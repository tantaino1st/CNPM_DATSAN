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

        // Lấy doanh thu 7 ngày gần nhất
        var sevenDaysAgo = today.AddDays(-6);
        var doanhThu7Ngay = await _context.HoaDons
            .Where(h => h.TrangThaiThanhToan == "Da thanh toan" && h.NgayThanhToan >= sevenDaysAgo)
            .GroupBy(h => h.NgayThanhToan!.Value.Date)
            .Select(g => new DoanhThuNgayViewModel
            {
                Ngay = g.Key.ToString("dd/MM"),
                DoanhThu = g.Sum(h => h.TongTien)
            })
            .ToListAsync();

        // Đảm bảo đủ 7 ngày (điền 0 nếu không có dữ liệu)
        var listDoanhThu = new List<DoanhThuNgayViewModel>();
        for (int i = 6; i >= 0; i--)
        {
            var date = today.AddDays(-i);
            var dateStr = date.ToString("dd/MM");
            var found = doanhThu7Ngay.FirstOrDefault(x => x.Ngay == dateStr);
            listDoanhThu.Add(found ?? new DoanhThuNgayViewModel { Ngay = dateStr, DoanhThu = 0 });
        }

        // Thống kê theo loại sân
        var thongKeLoaiSan = await _context.SanBongs
            .Include(s => s.LoaiSan)
            .GroupBy(s => s.LoaiSan!.TenLoaiSan)
            .Select(g => new LoaiSanThongKeViewModel
            {
                TenLoai = g.Key ?? "Chưa phân loại",
                SoLuong = g.Count()
            })
            .ToListAsync();

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
            DoanhThuTheoNgay = listDoanhThu,
            ThongKeLoaiSan = thongKeLoaiSan,
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

    [HttpGet]
    public async Task<IActionResult> GetLatestNotifications()
    {
        var latestBookings = await _context.DatSans
            .Include(d => d.KhachHang)
            .OrderByDescending(d => d.NgayTao)
            .Take(3)
            .Select(d => new {
                Type = "Booking",
                Title = "Đơn đặt sân mới",
                Content = $"Khách {d.KhachHang!.HoTen} vừa đặt sân vào {d.NgayDat:dd/MM}",
                Time = d.NgayTao,
                Icon = "fa-calendar-check",
                Class = "bg-primary"
            })
            .ToListAsync();

        var latestPayments = await _context.HoaDons
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.KhachHang)
            .OrderByDescending(h => h.NgayThanhToan)
            .Take(3)
            .Select(h => new {
                Type = "Payment",
                Title = "Thanh toán thành công",
                Content = $"Hóa đơn #{h.MaHD:D5} - {h.TongTien:N0}đ",
                Time = h.NgayThanhToan ?? DateTime.Now,
                Icon = "fa-money-bill-wave",
                Class = "bg-success"
            })
            .ToListAsync();

        var allNotifications = latestBookings.Cast<object>()
            .Concat(latestPayments.Cast<object>())
            .OrderByDescending(x => (DateTime)x.GetType().GetProperty("Time")!.GetValue(x)!)
            .Take(5)
            .ToList();

        return Json(allNotifications);
    }
}
