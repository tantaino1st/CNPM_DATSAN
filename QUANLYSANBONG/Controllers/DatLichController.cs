using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Models;
using QUANLYSANBONG.Filters;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
public class DatLichController : Controller
{
    private readonly ApplicationDbContext _context;

    public DatLichController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(DateTime? ngay, int? maSan)
    {
        DateTime selectedDate = ngay ?? DateTime.Today;
        
        // 1. Lấy toàn bộ danh sách sân để làm bộ lọc
        var tatCaSan = await _context.SanBongs
            .Include(x => x.LoaiSan)
            .Where(x => x.TrangThai == "Hoat dong")
            .ToListAsync();

        if (!tatCaSan.Any())
        {
            return View(new DatLichViewModel { NgayHienTai = selectedDate });
        }

        // 2. Xác định danh sách sân hiển thị
        List<SanBong> dsSanHienThi;
        if (maSan.HasValue && maSan > 0)
        {
            // Chọn 1 sân cụ thể
            dsSanHienThi = tatCaSan.Where(x => x.MaSan == maSan).ToList();
        }
        else if (maSan == 0)
        {
            // Chọn "Tất cả sân" -> Giới hạn tối đa 4 sân để không vỡ giao diện
            dsSanHienThi = tatCaSan.Take(4).ToList();
        }
        else
        {
            // Mặc định lấy sân đầu tiên
            dsSanHienThi = tatCaSan.Take(1).ToList();
            maSan = dsSanHienThi.FirstOrDefault()?.MaSan;
        }

        var dsMaSanHienThi = dsSanHienThi.Select(s => s.MaSan).ToList();

        // 3. Lấy lịch đặt của các sân đang hiển thị
        var dsDatSan = await _context.DatSans
            .Include(x => x.KhachHang)
            .Where(x => x.NgayDat.Date == selectedDate.Date && 
                   dsMaSanHienThi.Contains(x.MaSan) &&
                   x.TrangThai != "Da huy")
            .ToListAsync();
            
        var gioHoatDongs = await _context.GioHoatDongs.ToListAsync();
        
        // 4. Tính toán thống kê dựa trên các sân đang hiển thị
        int tongKhungGio = dsSanHienThi.Count * 16;
        int soDaDat = dsDatSan.Count(x => x.TrangThai == "Da dat" || x.TrangThai == "Cho xac nhan" || x.TrangThai == "Da xac nhan");
        int soDangChoi = dsDatSan.Count(x => (x.TrangThai == "Da xac nhan" || x.TrangThai == "Da dat") && 
                                            DateTime.Now.Date == selectedDate.Date && 
                                            DateTime.Now.TimeOfDay >= x.GioBatDau && 
                                            DateTime.Now.TimeOfDay <= x.GioKetThuc);
        
        int soTrong = tongKhungGio - soDaDat;

        var viewModel = new DatLichViewModel
        {
            NgayHienTai = selectedDate,
            SelectedMaSan = maSan,
            DanhSachTatCaSan = tatCaSan,
            DanhSachSanHienThi = dsSanHienThi,
            DanhSachDatSan = dsDatSan,
            GioHoatDongs = gioHoatDongs,
            SoDaDat = soDaDat,
            SoDangChoi = soDangChoi,
            SoTrong = Math.Max(0, soTrong),
            TiLeLapDay = tongKhungGio > 0 ? Math.Round((double)soDaDat * 100 / tongKhungGio, 1) : 0
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> TaoNhanh(int MaSan, string HoTen, string SoDienThoai, DateTime NgayDat, string GioBatDau, string GioKetThuc, decimal TienCoc, string? GhiChu, string TrangThai)
    {
        try
        {
            TimeSpan start = TimeSpan.Parse(GioBatDau);
            TimeSpan end = TimeSpan.Parse(GioKetThuc);

            bool isOverlapping = await _context.DatSans.AnyAsync(x => 
                x.MaSan == MaSan && 
                x.NgayDat.Date == NgayDat.Date && 
                x.TrangThai != "Da huy" &&
                ((start >= x.GioBatDau && start < x.GioKetThuc) || 
                 (end > x.GioBatDau && end <= x.GioKetThuc) || 
                 (start <= x.GioBatDau && end >= x.GioKetThuc)));

            if (isOverlapping)
            {
                TempData["Error"] = "Sân đã có lịch đặt trong khoảng thời gian này!";
                return RedirectToAction("Index", new { ngay = NgayDat.ToString("yyyy-MM-dd"), maSan = MaSan });
            }

            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.SoDienThoai == SoDienThoai);
            if (khachHang == null)
            {
                khachHang = new KhachHang { HoTen = HoTen, SoDienThoai = SoDienThoai };
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
            }

            // Tự động tính giá
            var dayOfWeek = NgayDat.DayOfWeek;
            string groupName = (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday) 
                ? "Thu 7 - Chu nhat" : "Thu 2 - Thu 6";

            var priceList = await _context.BangGiaKhungGios
                .Where(x => x.NgayTrongTuan == groupName && start >= x.GioBatDau && start < x.GioKetThuc)
                .FirstOrDefaultAsync();

            decimal totalAmount = 0;
            var sanBong = await _context.SanBongs.FindAsync(MaSan);
            decimal giaMacDinh = sanBong?.GiaMacDinh ?? 0;
            double duration = (end - start).TotalHours;

            if (priceList != null)
            {
                totalAmount = (priceList.DonGia + priceList.PhuThuCaoDiem) * (decimal)duration;
            }
            else
            {
                totalAmount = giaMacDinh * (decimal)duration;
            }

            var datSan = new DatSan
            {
                MaSan = MaSan,
                MaKH = khachHang.MaKH,
                MaBangGia = priceList?.MaBangGia,
                NgayDat = NgayDat,
                GioBatDau = start,
                GioKetThuc = end,
                ThoiLuong = duration,
                TienCoc = TienCoc,
                TongTien = totalAmount,
                TrangThai = TrangThai,
                GhiChu = GhiChu,
                NgayTao = DateTime.Now
            };

            _context.DatSans.Add(datSan);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đặt sân thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Lỗi: " + ex.Message;
        }

        return RedirectToAction("Index", new { ngay = NgayDat.ToString("yyyy-MM-dd"), maSan = MaSan });
    }
}
