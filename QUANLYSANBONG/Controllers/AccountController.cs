using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string PhoneOrEmail, string Password, string? returnUrl = null)
    {
        PhoneOrEmail = PhoneOrEmail?.Trim() ?? "";
        var tenDangNhapToLogin = PhoneOrEmail;

        if (PhoneOrEmail.Contains("@"))
        {
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(k => k.GhiChu == PhoneOrEmail);
            if (khachHang != null)
            {
                tenDangNhapToLogin = khachHang.SoDienThoai;
            }
        }

        var user = await _context.NguoiDungs
            .Include(x => x.VaiTro)
            .FirstOrDefaultAsync(x => x.TenDangNhap == tenDangNhapToLogin && x.MatKhau == Password);

        if (user == null)
        {
            TempData["Error"] = "Số điện thoại hoặc mật khẩu không đúng.";
            return Redirect(returnUrl ?? "/");
        }

        if (!user.TrangThai)
        {
            TempData["Error"] = "Tài khoản đã bị khóa.";
            return Redirect(returnUrl ?? "/");
        }

        HttpContext.Session.SetInt32("MaNguoiDung", user.MaNguoiDung);
        HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
        HttpContext.Session.SetString("VaiTro", user.VaiTro?.TenVaiTro ?? string.Empty);

        _context.NhatKyThaoTacs.Add(new NhatKyThaoTac
        {
            MaNguoiDung = user.MaNguoiDung,
            HanhDong = "Dang nhap",
            NoiDung = $"Nguoi dung {user.TenDangNhap} dang nhap he thong",
            ThoiGian = DateTime.Now
        });
        await _context.SaveChangesAsync();

        TempData["Success"] = "Đăng nhập thành công!";

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        if (user.VaiTro?.TenVaiTro == "Khach hang")
        {
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Admin");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string Phone, string Password, string FullName, string? Email, string? returnUrl = null)
    {
        Phone = Phone?.Trim() ?? "";
        var exists = await _context.NguoiDungs.AnyAsync(x => x.TenDangNhap == Phone);
        if (exists)
        {
            TempData["Error"] = "Số điện thoại này đã được đăng ký.";
            return Redirect(returnUrl ?? "/");
        }

        var newUser = new NguoiDung
        {
            TenDangNhap = Phone,
            MatKhau = Password,
            MaVaiTro = 5, 
            TrangThai = true
        };

        _context.NguoiDungs.Add(newUser);
        await _context.SaveChangesAsync();

        var newKhachHang = new KhachHang
        {
            HoTen = FullName,
            SoDienThoai = Phone,
            GhiChu = Email,
            NhomKhach = "Khach moi"
        };
        _context.KhachHangs.Add(newKhachHang);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
        return Redirect(returnUrl ?? "/");
    }

    [SessionAuthorize]
    public async Task<IActionResult> Profile()
    {
        var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
        if (string.IsNullOrEmpty(tenDangNhap)) return RedirectToAction("Index", "Home");

        var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(k => k.SoDienThoai == tenDangNhap);
        if (khachHang == null)
        {
            // If not found by phone, maybe it's a staff user?
            var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(n => n.SoDienThoai == tenDangNhap);
            if (nhanVien != null)
            {
                ViewBag.UserType = "NhanVien";
                return View("ProfileNhanVien", nhanVien);
            }
            return NotFound("Không tìm thấy thông tin hồ sơ.");
        }

        ViewBag.UserType = "KhachHang";
        return View(khachHang);
    }

    [SessionAuthorize]
    public async Task<IActionResult> BookingHistory()
    {
        var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
        if (string.IsNullOrEmpty(tenDangNhap)) return RedirectToAction("Index", "Home");

        var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(k => k.SoDienThoai == tenDangNhap);
        if (khachHang == null)
        {
             return View(new List<DatSan>());
        }

        var history = await _context.DatSans
            .Include(d => d.SanBong)
            .Include(d => d.SanCon)
            .Where(d => d.MaKH == khachHang.MaKH)
            .OrderByDescending(d => d.NgayDat)
            .ThenByDescending(d => d.NgayTao)
            .ToListAsync();

        return View(history);
    }

    [SessionAuthorize]
    public async Task<IActionResult> BookingDetails(int id)
    {
        var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
        var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(k => k.SoDienThoai == tenDangNhap);
        
        if (khachHang == null) return Unauthorized();

        var booking = await _context.DatSans
            .Include(d => d.SanBong)
            .Include(d => d.SanCon)
            .Include(d => d.ChiTietDichVus)
                .ThenInclude(c => c.DichVu)
            .Include(d => d.HoaDon)
            .FirstOrDefaultAsync(d => d.MaDatSan == id && d.MaKH == khachHang.MaKH);

        if (booking == null) return NotFound("Không tìm thấy đơn đặt sân hoặc bạn không có quyền xem đơn này.");

        return View(booking);
    }
}
