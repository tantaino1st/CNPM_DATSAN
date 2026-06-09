using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Ke toan")]
public class BaoCaoController : Controller
{
    private readonly ApplicationDbContext _context;

    public BaoCaoController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
    {
        if (!fromDate.HasValue) fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        if (!toDate.HasValue) toDate = DateTime.Now;

        ViewData["FromDate"] = fromDate.Value.ToString("yyyy-MM-dd");
        ViewData["ToDate"] = toDate.Value.ToString("yyyy-MM-dd");

        var query = _context.HoaDons
            .Include(h => h.DatSan)
            .Where(h => h.TrangThaiThanhToan == "Da thanh toan" && 
                        h.NgayThanhToan >= fromDate.Value.Date && 
                        h.NgayThanhToan <= toDate.Value.Date.AddDays(1).AddTicks(-1));

        var tongDoanhThu = await query.SumAsync(h => h.TongTien);
        var tongTienSan = await query.SumAsync(h => h.TienSan);
        var tongTienDichVu = await query.SumAsync(h => h.TienDichVu);
        var tongHoaDon = await query.CountAsync();

        ViewBag.TongDoanhThu = tongDoanhThu;
        ViewBag.TongTienSan = tongTienSan;
        ViewBag.TongTienDichVu = tongTienDichVu;
        ViewBag.TongHoaDon = tongHoaDon;

        // Thống kê theo ngày
        var doanhThuTheoNgay = await query
            .GroupBy(h => h.NgayThanhToan!.Value.Date)
            .Select(g => new {
                Ngay = g.Key,
                DoanhThu = g.Sum(h => h.TongTien)
            })
            .OrderBy(x => x.Ngay)
            .ToListAsync();

        ViewBag.DoanhThuTheoNgay = doanhThuTheoNgay;

        // Thống kê lượt đặt sân
        var dsDatSan = await _context.DatSans
            .Where(d => d.TrangThai == "Da hoan thanh" && 
                        d.NgayDat >= fromDate.Value.Date && 
                        d.NgayDat <= toDate.Value.Date)
            .Include(d => d.SanBong)
            .GroupBy(d => d.SanBong!.TenSan)
            .Select(g => new {
                TenSan = g.Key,
                LuotDat = g.Count()
            })
            .OrderByDescending(x => x.LuotDat)
            .ToListAsync();

        ViewBag.LuotDatTheoSan = dsDatSan;

        return View();
    }
}
