using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin")]
public class NhanVienController : Controller
{
    private readonly ApplicationDbContext _context;

    public NhanVienController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var nhanViens = await _context.NhanViens.Include(n => n.NguoiDung).ToListAsync();
        return View(nhanViens);
    }

    public IActionResult Create()
    {
        ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs.Where(n => !_context.NhanViens.Any(nv => nv.MaNguoiDung == n.MaNguoiDung)), "MaNguoiDung", "TenDangNhap");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NhanVien nhanVien)
    {
        if (ModelState.IsValid)
        {
            _context.Add(nhanVien);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs.Where(n => !_context.NhanViens.Any(nv => nv.MaNguoiDung == n.MaNguoiDung)), "MaNguoiDung", "TenDangNhap", nhanVien.MaNguoiDung);
        return View(nhanVien);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var nhanVien = await _context.NhanViens.FindAsync(id);
        if (nhanVien == null) return NotFound();

        var availableUsers = _context.NguoiDungs.Where(n => !_context.NhanViens.Any(nv => nv.MaNguoiDung == n.MaNguoiDung && nv.MaNV != id)).ToList();
        
        ViewData["MaNguoiDung"] = new SelectList(availableUsers, "MaNguoiDung", "TenDangNhap", nhanVien.MaNguoiDung);
        return View(nhanVien);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NhanVien nhanVien)
    {
        if (id != nhanVien.MaNV) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(nhanVien);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật nhân viên thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(nhanVien.MaNV)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        var availableUsers = _context.NguoiDungs.Where(n => !_context.NhanViens.Any(nv => nv.MaNguoiDung == n.MaNguoiDung && nv.MaNV != id)).ToList();
        ViewData["MaNguoiDung"] = new SelectList(availableUsers, "MaNguoiDung", "TenDangNhap", nhanVien.MaNguoiDung);
        return View(nhanVien);
    }

    private bool NhanVienExists(int id)
    {
        return _context.NhanViens.Any(e => e.MaNV == id);
    }
}
