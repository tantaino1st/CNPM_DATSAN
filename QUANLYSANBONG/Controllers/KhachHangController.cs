using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
public class KhachHangController : Controller
{
    private readonly ApplicationDbContext _context;

    public KhachHangController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchString, string nhomKhach)
    {
        ViewData["CurrentFilter"] = searchString;
        ViewData["CurrentNhom"] = nhomKhach;

        var khachHangs = _context.KhachHangs.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            khachHangs = khachHangs.Where(k => k.HoTen.Contains(searchString) || k.SoDienThoai.Contains(searchString));
        }

        if (!string.IsNullOrEmpty(nhomKhach))
        {
            khachHangs = khachHangs.Where(k => k.NhomKhach == nhomKhach);
        }

        return View(await khachHangs.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KhachHang khachHang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(khachHang);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm khách hàng thành công!";
            return RedirectToAction(nameof(Index));
        }
        return View(khachHang);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var khachHang = await _context.KhachHangs.FindAsync(id);
        if (khachHang == null) return NotFound();

        return View(khachHang);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, KhachHang khachHang)
    {
        if (id != khachHang.MaKH) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(khachHang);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật khách hàng thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(khachHang.MaKH)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(khachHang);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var khachHang = await _context.KhachHangs.FindAsync(id);
        if (khachHang != null)
        {
            var hasDatSan = await _context.DatSans.AnyAsync(x => x.MaKH == id);
            if (hasDatSan)
            {
                TempData["ErrorMessage"] = "Không thể xóa khách hàng này vì đã có lịch sử đặt sân!";
                return RedirectToAction(nameof(Index));
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa khách hàng thành công!";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool KhachHangExists(int id)
    {
        return _context.KhachHangs.Any(e => e.MaKH == id);
    }
}
