using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

public class BlogController : Controller
{
    private readonly ApplicationDbContext _context;
    private const int PageSize = 6;

    public BlogController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? keyword, int? categoryId, int page = 1)
    {
        var query = _context.BlogPosts
            .Include(b => b.Category)
            .Where(b => b.TrangThai == true)
            .AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(b => b.TieuDe.Contains(keyword) || b.MoTaNgan.Contains(keyword));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(b => b.BlogCategoryId == categoryId.Value);
        }

        int totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
        page = Math.Max(1, Math.Min(page, totalPages > 0 ? totalPages : 1));

        var posts = await query
            .OrderByDescending(b => b.NgayDang)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        var viewModel = new BlogIndexViewModel
        {
            BaiViet = posts,
            DanhMuc = await _context.BlogCategories.Where(c => c.TrangThai).ToListAsync(),
            BaiVietNoiBat = await _context.BlogPosts.Where(b => b.TrangThai && b.NoiBat).Take(5).ToListAsync(),
            Keyword = keyword,
            CategoryId = categoryId,
            CurrentPage = page,
            TotalPages = totalPages
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var post = await _context.BlogPosts
            .Include(b => b.Category)
            .Include(b => b.NguoiTao)
            .FirstOrDefaultAsync(b => b.Id == id && b.TrangThai);

        if (post == null) return NotFound();

        // Tăng lượt xem
        post.LuotXem++;
        _context.Update(post);
        await _context.SaveChangesAsync();

        var relatedPosts = await _context.BlogPosts
            .Where(b => b.TrangThai && b.BlogCategoryId == post.BlogCategoryId && b.Id != id)
            .Take(3)
            .ToListAsync();

        var viewModel = new BlogDetailViewModel
        {
            BaiViet = post,
            BaiVietLienQuan = relatedPosts
        };

        return View(viewModel);
    }
}
