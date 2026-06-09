using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin")]
public class NguoiDungController : Controller
{
    private readonly ApplicationDbContext _context;

    public NguoiDungController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var nguoiDungs = await _context.NguoiDungs.Include(n => n.VaiTro).ToListAsync();
        return View(nguoiDungs);
    }

    public IActionResult Create()
    {
        ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "TenVaiTro");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NguoiDung nguoiDung)
    {
        if (ModelState.IsValid)
        {
            if (await _context.NguoiDungs.AnyAsync(n => n.TenDangNhap == nguoiDung.TenDangNhap))
            {
                ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại.");
                ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
                return View(nguoiDung);
            }

            _context.Add(nguoiDung);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm tài khoản thành công!";
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
        return View(nguoiDung);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var nguoiDung = await _context.NguoiDungs.FindAsync(id);
        if (nguoiDung == null) return NotFound();

        ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
        return View(nguoiDung);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NguoiDung nguoiDung)
    {
        if (id != nguoiDung.MaNguoiDung) return NotFound();

        if (ModelState.IsValid)
        {
            if (await _context.NguoiDungs.AnyAsync(n => n.TenDangNhap == nguoiDung.TenDangNhap && n.MaNguoiDung != id))
            {
                ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại.");
                ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
                return View(nguoiDung);
            }

            try
            {
                _context.Update(nguoiDung);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật tài khoản thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NguoiDungExists(nguoiDung.MaNguoiDung)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaVaiTro"] = new SelectList(_context.VaiTros, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
        return View(nguoiDung);
    }

    private bool NguoiDungExists(int id)
    {
        return _context.NguoiDungs.Any(e => e.MaNguoiDung == id);
    }
}
