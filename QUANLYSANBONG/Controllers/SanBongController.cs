using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

public class SanBongController : Controller
{
    private readonly ApplicationDbContext _context;

    public SanBongController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> TimSan(string? keyword, int? loaiSanId, string? tinhThanh, string? sort, int page = 1)
    {
        int pageSize = 20;
        var query = _context.SanBongs.Include(s => s.LoaiSan).AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(s => s.TenSan.Contains(keyword) || s.DiaChi!.Contains(keyword));
        }

        if (loaiSanId.HasValue)
        {
            query = query.Where(s => s.MaLoaiSan == loaiSanId.Value);
        }

        if (!string.IsNullOrEmpty(tinhThanh))
        {
            query = query.Where(s => s.TinhThanh == tinhThanh);
        }

        switch (sort)
        {
            case "price_asc":
                query = query.OrderBy(s => s.GiaMacDinh);
                break;
            case "price_desc":
                query = query.OrderByDescending(s => s.GiaMacDinh);
                break;
            case "name_az":
                query = query.OrderBy(s => s.TenSan);
                break;
            default:
                query = query.OrderByDescending(s => s.MaSan);
                break;
        }

        int totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        var danhSachSan = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        var viewModel = new TimSanViewModel
        {
            DanhSachSan = danhSachSan,
            DanhSachLoaiSan = await _context.LoaiSans.ToListAsync(),
            Keyword = keyword,
            LoaiSanId = loaiSanId,
            TinhThanh = tinhThanh,
            Sort = sort,
            CurrentPage = page,
            TotalPages = totalPages,
            TotalItems = totalItems
        };

        return View(viewModel);
    }

    [SessionAuthorize("Admin", "Quan ly")]
    public async Task<IActionResult> Index(string searchString, int? maLoaiSan, string trangThai)
    {
        ViewData["CurrentFilter"] = searchString;
        ViewData["CurrentTrangThai"] = trangThai;
        ViewData["CurrentLoaiSan"] = maLoaiSan;

        var sanBongs = _context.SanBongs.Include(s => s.LoaiSan).AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            sanBongs = sanBongs.Where(s => s.TenSan.Contains(searchString));
        }

        if (maLoaiSan.HasValue)
        {
            sanBongs = sanBongs.Where(s => s.MaLoaiSan == maLoaiSan.Value);
        }

        if (!string.IsNullOrEmpty(trangThai))
        {
            sanBongs = sanBongs.Where(s => s.TrangThai == trangThai);
        }

        ViewData["MaLoaiSan"] = new SelectList(_context.LoaiSans, "MaLoaiSan", "TenLoaiSan", maLoaiSan);
        
        return View(await sanBongs.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var sanBong = await _context.SanBongs
            .Include(s => s.LoaiSan)
            .Include(s => s.SanCons)
            .FirstOrDefaultAsync(m => m.MaSan == id);

        if (sanBong == null) return NotFound();

        return View(sanBong);
    }

    public IActionResult Create()
    {
        ViewData["MaLoaiSan"] = new SelectList(_context.LoaiSans, "MaLoaiSan", "TenLoaiSan");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SanBong sanBong)
    {
        if (ModelState.IsValid)
        {
            _context.Add(sanBong);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm sân bóng thành công!";
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaLoaiSan"] = new SelectList(_context.LoaiSans, "MaLoaiSan", "TenLoaiSan", sanBong.MaLoaiSan);
        return View(sanBong);
    }

    [SessionAuthorize("Admin", "Quan ly")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var sanBong = await _context.SanBongs.FindAsync(id);
        if (sanBong == null) return NotFound();

        ViewData["MaLoaiSan"] = new SelectList(_context.LoaiSans, "MaLoaiSan", "TenLoaiSan", sanBong.MaLoaiSan);
        return View(sanBong);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [SessionAuthorize("Admin", "Quan ly")]
    public async Task<IActionResult> Edit(int id, SanBong sanBong)
    {
        if (id != sanBong.MaSan) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(sanBong);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật sân bóng thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanBongExists(sanBong.MaSan)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaLoaiSan"] = new SelectList(_context.LoaiSans, "MaLoaiSan", "TenLoaiSan", sanBong.MaLoaiSan);
        return View(sanBong);
    }

    [HttpPost]
    [SessionAuthorize("Admin", "Quan ly")]
    public async Task<IActionResult> Delete(int id)
    {
        var sanBong = await _context.SanBongs.FindAsync(id);
        if (sanBong != null)
        {
            var hasDatSan = await _context.DatSans.AnyAsync(x => x.MaSan == id);
            if (hasDatSan)
            {
                TempData["ErrorMessage"] = "Không thể xóa sân này vì đã có lịch đặt sân!";
                return RedirectToAction(nameof(Index));
            }

            _context.SanBongs.Remove(sanBong);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa sân bóng thành công!";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool SanBongExists(int id)
    {
        return _context.SanBongs.Any(e => e.MaSan == id);
    }
}
