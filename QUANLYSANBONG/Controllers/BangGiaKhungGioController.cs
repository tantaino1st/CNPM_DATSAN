using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly")]
public class BangGiaKhungGioController : Controller
{
    private readonly ApplicationDbContext _context;

    public BangGiaKhungGioController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.BangGiaKhungGios.OrderBy(x => x.NgayTrongTuan).ThenBy(x => x.GioBatDau).ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BangGiaKhungGio bangGia)
    {
        if (ModelState.IsValid)
        {
            // Validate time
            if (bangGia.GioKetThuc <= bangGia.GioBatDau)
            {
                ModelState.AddModelError("GioKetThuc", "Giờ kết thúc phải lớn hơn giờ bắt đầu.");
                return View(bangGia);
            }

            _context.Add(bangGia);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm bảng giá thành công!";
            return RedirectToAction(nameof(Index));
        }
        return View(bangGia);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var bangGia = await _context.BangGiaKhungGios.FindAsync(id);
        if (bangGia == null) return NotFound();

        return View(bangGia);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BangGiaKhungGio bangGia)
    {
        if (id != bangGia.MaBangGia) return NotFound();

        if (ModelState.IsValid)
        {
            if (bangGia.GioKetThuc <= bangGia.GioBatDau)
            {
                ModelState.AddModelError("GioKetThuc", "Giờ kết thúc phải lớn hơn giờ bắt đầu.");
                return View(bangGia);
            }

            try
            {
                _context.Update(bangGia);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật bảng giá thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BangGiaExists(bangGia.MaBangGia)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(bangGia);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var bangGia = await _context.BangGiaKhungGios.FindAsync(id);
        if (bangGia != null)
        {
            var hasDatSan = await _context.DatSans.AnyAsync(x => x.MaBangGia == id);
            if (hasDatSan)
            {
                TempData["ErrorMessage"] = "Không thể xóa bảng giá này vì đã được sử dụng để đặt sân!";
                return RedirectToAction(nameof(Index));
            }

            _context.BangGiaKhungGios.Remove(bangGia);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa bảng giá thành công!";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool BangGiaExists(int id)
    {
        return _context.BangGiaKhungGios.Any(e => e.MaBangGia == id);
    }
}
