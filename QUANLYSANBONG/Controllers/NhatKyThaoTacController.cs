using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin")]
public class NhatKyThaoTacController : Controller
{
    private readonly ApplicationDbContext _context;
    public NhatKyThaoTacController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? search, string? actionType)
    {
        ViewData["Title"] = "Nhật ký hệ thống";
        var query = _context.NhatKyThaoTacs.Include(x => x.NguoiDung).AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.HanhDong.Contains(search) || 
                                    (x.NoiDung ?? "").Contains(search) || 
                                    x.NguoiDung!.TenDangNhap.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(actionType))
        {
            query = query.Where(x => x.HanhDong.Contains(actionType));
        }

        return View(await query.OrderByDescending(x => x.ThoiGian).Take(100).ToListAsync());
    }
}
