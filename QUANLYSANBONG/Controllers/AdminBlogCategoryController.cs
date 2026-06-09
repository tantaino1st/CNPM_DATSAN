using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly")]
public class AdminBlogCategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminBlogCategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.BlogCategories.ToListAsync());
    }

    public IActionResult Create()
    {
        return View(new BlogCategory());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogCategory category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _context.BlogCategories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BlogCategory category)
    {
        if (id != category.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.BlogCategories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.BlogCategories.FindAsync(id);
        if (category != null)
        {
            _context.BlogCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
