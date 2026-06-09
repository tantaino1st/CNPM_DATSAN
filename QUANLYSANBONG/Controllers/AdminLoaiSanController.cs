using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly")]
public class AdminLoaiSanController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminLoaiSanController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.LoaiSans.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LoaiSan loaiSan)
    {
        if (ModelState.IsValid)
        {
            _context.Add(loaiSan);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm loại sân thành công!";
            return RedirectToAction(nameof(Index));
        }
        return View(loaiSan);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var loaiSan = await _context.LoaiSans.FindAsync(id);
        if (loaiSan == null) return NotFound();

        return View(loaiSan);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, LoaiSan loaiSan)
    {
        if (id != loaiSan.MaLoaiSan) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(loaiSan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật loại sân thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiSanExists(loaiSan.MaLoaiSan)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(loaiSan);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var loaiSan = await _context.LoaiSans.FirstOrDefaultAsync(m => m.MaLoaiSan == id);
        if (loaiSan == null) return NotFound();

        return View(loaiSan);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var loaiSan = await _context.LoaiSans.FindAsync(id);
        if (loaiSan != null)
        {
            // Kiểm tra xem có sân bóng nào đang dùng loại này không
            var hasSanBong = await _context.SanBongs.AnyAsync(x => x.MaLoaiSan == id);
            if (hasSanBong)
            {
                TempData["ErrorMessage"] = "Không thể xóa loại sân này vì đang có sân bóng sử dụng!";
                return RedirectToAction(nameof(Index));
            }

            _context.LoaiSans.Remove(loaiSan);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa loại sân thành công!";
        }
        
        return RedirectToAction(nameof(Index));
    }

    private bool LoaiSanExists(int id)
    {
        return _context.LoaiSans.Any(e => e.MaLoaiSan == id);
    }
}
