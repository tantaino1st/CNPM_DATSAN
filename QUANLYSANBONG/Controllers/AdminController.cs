using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Nhan vien", "Ke toan")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Dashboard");
    }

    public async Task<IActionResult> Dashboard()
    {
        var today = DateTime.Today;
        
        // Cơ bản
        var tongSan = await _context.SanBongs.CountAsync();
        var dangHoatDong = await _context.SanBongs.CountAsync(x => x.TrangThai == "Hoat dong");
        
        var donDatHomNay = await _context.DatSans
            .CountAsync(x => x.NgayDat.Date == today);
            
        var doanhThuHomNay = await _context.HoaDons
            .Include(x => x.DatSan)
            .Where(x => x.TrangThaiThanhToan == "Da thanh toan" && x.DatSan!.NgayDat.Date == today)
            .SumAsync(x => x.TongTien);

        var khachMoiThangNay = await _context.KhachHangs
            .CountAsync(); // Trong thực tế nên có cột NgayTao

        // Lịch đặt gần nhất
        var dsDatSanGanDay = await _context.DatSans
            .Include(x => x.KhachHang)
            .Include(x => x.SanBong)
            .OrderByDescending(x => x.MaDatSan)
            .Take(5)
            .ToListAsync();

        var viewModel = new DashboardViewModel
        {
            DonDatHomNay = donDatHomNay,
            DoanhThuHomNay = doanhThuHomNay,
            SoSanHoatDong = dangHoatDong,
            TongSoSan = tongSan,
            KhachMoiThangNay = khachMoiThangNay,
            
            // Dữ liệu mẫu cho biểu đồ doanh thu (7 ngày gần nhất)
            DoanhThuTheoNgay = new List<DoanhThuNgayViewModel>
            {
                new DoanhThuNgayViewModel { Ngay = today.AddDays(-3).ToString("dd/MM"), DoanhThu = 4500000 },
                new DoanhThuNgayViewModel { Ngay = today.AddDays(-2).ToString("dd/MM"), DoanhThu = 3800000 },
                new DoanhThuNgayViewModel { Ngay = today.AddDays(-1).ToString("dd/MM"), DoanhThu = 5200000 },
                new DoanhThuNgayViewModel { Ngay = today.ToString("dd/MM"), DoanhThu = doanhThuHomNay > 0 ? doanhThuHomNay : 600000 }
            },
            
            // Dữ liệu mẫu cho loại sân
            ThongKeLoaiSan = new List<LoaiSanThongKeViewModel>
            {
                new LoaiSanThongKeViewModel { TenLoai = "Bóng đá", SoLuong = 12 },
                new LoaiSanThongKeViewModel { TenLoai = "Cầu lông", SoLuong = 8 },
                new LoaiSanThongKeViewModel { TenLoai = "Tennis", SoLuong = 4 },
                new LoaiSanThongKeViewModel { TenLoai = "Pickleball", SoLuong = 6 },
                new LoaiSanThongKeViewModel { TenLoai = "Bóng rổ", SoLuong = 3 }
            },

            LichDatGanNhat = dsDatSanGanDay.Select(x => new LichDatGanNhatViewModel
            {
                MaDatSan = x.MaDatSan,
                TenKhachHang = x.KhachHang?.HoTen ?? "Khách vãng lai",
                SoDienThoai = x.KhachHang?.SoDienThoai ?? "",
                TenSan = x.SanBong?.TenSan ?? "N/A",
                ThoiGian = $"{x.GioBatDau:hh\\:mm} - {x.GioKetThuc:hh\\:mm}",
                Gia = x.TongTien,
                TrangThai = x.TrangThai
            }).ToList()
        };

        return View(viewModel);
    }
}
