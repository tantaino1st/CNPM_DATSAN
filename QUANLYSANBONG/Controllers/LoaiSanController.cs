using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QUANLYSANBONG.Controllers
{
    public class LoaiSanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 6;

        public LoaiSanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? loai, string? keyword, string? quan, int page = 1)
        {
            var query = _context.SanBongs
                .Include(s => s.LoaiSan)
                .Where(s => s.TrangThai == "Hoat dong")
                .AsQueryable();

            if (!string.IsNullOrEmpty(loai))
            {
                query = query.Where(s => s.LoaiSan.TenLoaiSan == loai);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(s => s.TenSan.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(quan))
            {
                query = query.Where(s => s.DiaChi.Contains(quan));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            page = Math.Max(1, Math.Min(page, totalPages > 0 ? totalPages : 1));

            var dsSan = await query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var viewModel = new LoaiSanViewModel
            {
                TenLoaiSan = loai ?? "Tất cả sân",
                DanhSachSan = dsSan,
                DanhSachLoaiSan = await _context.LoaiSans.ToListAsync(),
                BaiVietNoiBat = await _context.BlogPosts.Where(b => b.TrangThai && b.NoiBat).Take(5).ToListAsync(),
                Keyword = keyword,
                QuanHuyen = quan,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View("Index", viewModel);
        }

        public Task<IActionResult> BongDa(string? keyword, string? quan, int page = 1) => Index("Bóng đá", keyword, quan, page);
        public Task<IActionResult> Pickleball(string? keyword, string? quan, int page = 1) => Index("Pickleball", keyword, quan, page);
        public Task<IActionResult> CauLong(string? keyword, string? quan, int page = 1) => Index("Cầu lông", keyword, quan, page);
        public Task<IActionResult> Tennis(string? keyword, string? quan, int page = 1) => Index("Quần vợt", keyword, quan, page);
        public Task<IActionResult> BongRo(string? keyword, string? quan, int page = 1) => Index("Bóng rổ", keyword, quan, page);
    }
}
