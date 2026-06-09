using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly")]
public class AdminBlogController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public AdminBlogController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var posts = await _context.BlogPosts
            .Include(b => b.Category)
            .OrderByDescending(b => b.NgayDang)
            .ToListAsync();
        return View(posts);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await _context.BlogCategories.ToListAsync(), "Id", "TenDanhMuc");
        return View(new BlogPost());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogPost post, IFormFile? uploadAnh)
    {
        if (ModelState.IsValid)
        {
            if (uploadAnh != null)
            {
                string folder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "blogs");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadAnh.FileName);
                string path = Path.Combine(folder, fileName);
                
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await uploadAnh.CopyToAsync(stream);
                }
                post.AnhDaiDien = "/uploads/blogs/" + fileName;
            }
            else
            {
                post.AnhDaiDien = "/images/default-blog.jpg";
            }

            post.NgayDang = DateTime.Now;
            _context.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categories = new SelectList(await _context.BlogCategories.ToListAsync(), "Id", "TenDanhMuc", post.BlogCategoryId);
        return View(post);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null) return NotFound();

        ViewBag.Categories = new SelectList(await _context.BlogCategories.ToListAsync(), "Id", "TenDanhMuc", post.BlogCategoryId);
        return View(post);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BlogPost post, IFormFile? uploadAnh)
    {
        if (id != post.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var current = await _context.BlogPosts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (current == null) return NotFound();

                if (uploadAnh != null)
                {
                    string folder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "blogs");
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadAnh.FileName);
                    string path = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await uploadAnh.CopyToAsync(stream);
                    }
                    post.AnhDaiDien = "/uploads/blogs/" + fileName;
                }
                else
                {
                    post.AnhDaiDien = current.AnhDaiDien;
                }

                _context.Update(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BlogPosts.Any(e => e.Id == post.Id)) return NotFound();
                else throw;
            }
        }

        ViewBag.Categories = new SelectList(await _context.BlogCategories.ToListAsync(), "Id", "TenDanhMuc", post.BlogCategoryId);
        return View(post);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.BlogPosts.Include(b => b.Category).FirstOrDefaultAsync(x => x.Id == id);
        if (post == null) return NotFound();
        return View(post);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post != null)
        {
            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
