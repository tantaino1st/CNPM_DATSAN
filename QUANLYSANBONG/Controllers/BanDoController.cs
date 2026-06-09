using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QUANLYSANBONG.Controllers
{
    public class BanDoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BanDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dsSan = await _context.SanBongs
                .Include(s => s.LoaiSan)
                .Where(s => s.TrangThai == "Hoat dong" && s.Latitude != null && s.Longitude != null)
                .ToListAsync();

            var viewModel = new BanDoViewModel
            {
                DanhSachSan = dsSan
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetSanBong()
        {
            var dsSan = await _context.SanBongs
                .Include(s => s.LoaiSan)
                .Where(s => s.TrangThai == "Hoat dong" && s.Latitude != null && s.Longitude != null)
                .Select(s => new
                {
                    maSan = s.MaSan,
                    tenSan = s.TenSan,
                    diaChi = s.DiaChi,
                    tinhThanh = s.TinhThanh,
                    loaiSan = s.LoaiSan != null ? s.LoaiSan.TenLoaiSan : "Khác",
                    hinhAnh = s.HinhAnh,
                    lat = s.Latitude,
                    lng = s.Longitude,
                    soSanCon = s.SoLuongSanCon,
                    gia = s.GiaMacDinh
                })
                .ToListAsync();

            return Json(dsSan);
        }
    }
}
