using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Models;
using QUANLYSANBONG.Filters;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Nhan vien", "Ke toan")]
public class CaiDatController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public CaiDatController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var username = HttpContext.Session.GetString("Username");
        var viewModel = new CaiDatViewModel
        {
            ThongTinSan = await _context.ThongTinSans.FirstOrDefaultAsync() ?? new ThongTinSan { TenSan = "Sân bóng của tôi" },
            DanhSachGioHoatDong = await _context.GioHoatDongs.OrderBy(x => x.Id).ToListAsync(),
            DanhSachThongBao = await _context.CaiDatThongBaos.ToListAsync(),
            NguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.TenDangNhap == username) ?? new NguoiDung()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateThongTinSan(ThongTinSan model, IFormFile? uploadLogo)
    {
        var current = await _context.ThongTinSans.FirstOrDefaultAsync();
        if (current == null) 
        { 
            _context.ThongTinSans.Add(model); 
            current = model; 
        }
        else
        {
            current.TenSan = model.TenSan;
            current.SoDienThoai = model.SoDienThoai;
            current.DiaChi = model.DiaChi;
            current.Email = model.Email;
            current.Website = model.Website;
            current.MoTa = model.MoTa;
        }

        if (uploadLogo != null)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            
            string fileName = "logo_san" + Path.GetExtension(uploadLogo.FileName);
            string path = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(path, FileMode.Create)) 
            { 
                await uploadLogo.CopyToAsync(stream); 
            }
            current.LogoUrl = "/images/" + fileName;
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Cập nhật thông tin sân thành công!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateGioHoatDong(List<GioHoatDong> danhSachGio)
    {
        if (danhSachGio != null)
        {
            foreach (var item in danhSachGio)
            {
                var current = await _context.GioHoatDongs.FindAsync(item.Id);
                if (current != null)
                {
                    current.DangHoatDong = item.DangHoatDong;
                    current.GioMoCua = item.GioMoCua;
                    current.GioDongCua = item.GioDongCua;
                }
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật giờ hoạt động thành công!";
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateThongBao(List<CaiDatThongBao> danhSachThongBao)
    {
        if (danhSachThongBao != null)
        {
            foreach (var item in danhSachThongBao)
            {
                var current = await _context.CaiDatThongBaos.FindAsync(item.Id);
                if (current != null) current.DangBat = item.DangBat;
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật cài đặt thông báo thành công!";
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTaiKhoan(string HoTen, string SoDienThoai, string? CurrentPassword, string? NewPassword, string? ConfirmPassword)
    {
        var username = HttpContext.Session.GetString("Username");
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(x => x.TenDangNhap == username);
        
        if (user == null) return NotFound();

        user.HoTen = HoTen;
        user.SoDienThoai = SoDienThoai;

        // Đổi mật khẩu nếu có nhập
        if (!string.IsNullOrEmpty(CurrentPassword) && !string.IsNullOrEmpty(NewPassword))
        {
            if (user.MatKhau != CurrentPassword)
            {
                TempData["Error"] = "Mật khẩu hiện tại không đúng!";
                return RedirectToAction("Index");
            }
            if (NewPassword != ConfirmPassword)
            {
                TempData["Error"] = "Mật khẩu xác nhận không khớp!";
                return RedirectToAction("Index");
            }
            user.MatKhau = NewPassword;
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Cập nhật thông tin tài khoản thành công!";
        return RedirectToAction("Index");
    }
}
