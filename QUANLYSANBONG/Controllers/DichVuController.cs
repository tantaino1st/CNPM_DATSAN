using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
public class DichVuController : Controller
{
    private readonly ApplicationDbContext _context;

    public DichVuController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        ViewData["CurrentFilter"] = searchString;

        var dichVus = _context.DichVus.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            dichVus = dichVus.Where(d => d.TenDichVu.Contains(searchString));
        }

        return View(await dichVus.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DichVu dichVu)
    {
        if (ModelState.IsValid)
        {
            _context.Add(dichVu);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm dịch vụ thành công!";
            return RedirectToAction(nameof(Index));
        }
        return View(dichVu);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var dichVu = await _context.DichVus.FindAsync(id);
        if (dichVu == null) return NotFound();

        return View(dichVu);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DichVu dichVu)
    {
        if (id != dichVu.MaDichVu) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(dichVu);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật dịch vụ thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DichVuExists(dichVu.MaDichVu)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(dichVu);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var dichVu = await _context.DichVus.FindAsync(id);
        if (dichVu != null)
        {
            var hasChiTiet = await _context.ChiTietDichVus.AnyAsync(x => x.MaDichVu == id);
            if (hasChiTiet)
            {
                TempData["ErrorMessage"] = "Không thể xóa dịch vụ này vì đã được sử dụng trong hóa đơn!";
                return RedirectToAction(nameof(Index));
            }

            _context.DichVus.Remove(dichVu);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa dịch vụ thành công!";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool DichVuExists(int id)
    {
        return _context.DichVus.Any(e => e.MaDichVu == id);
    }
}
