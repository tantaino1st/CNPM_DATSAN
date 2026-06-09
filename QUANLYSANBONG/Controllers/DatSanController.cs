using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
public class DatSanController : Controller
{
    private readonly ApplicationDbContext _context;

    public DatSanController(ApplicationDbContext context)
    {
        _context = context;
    }

    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string? trangThai)
    {
        var datSans = _context.DatSans
            .Include(d => d.KhachHang)
            .Include(d => d.SanBong)
            .AsQueryable();

        if (fromDate.HasValue)
        {
            datSans = datSans.Where(d => d.NgayDat.Date >= fromDate.Value.Date);
            ViewData["FromDate"] = fromDate.Value.ToString("yyyy-MM-dd");
        }

        if (toDate.HasValue)
        {
            datSans = datSans.Where(d => d.NgayDat.Date <= toDate.Value.Date);
            ViewData["ToDate"] = toDate.Value.ToString("yyyy-MM-dd");
        }

        if (!string.IsNullOrEmpty(trangThai))
        {
            datSans = datSans.Where(d => d.TrangThai == trangThai);
            ViewData["CurrentTrangThai"] = trangThai;
        }

        return View(await datSans.OrderByDescending(d => d.NgayDat).ThenByDescending(d => d.MaDatSan).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> TaoNhanh(int MaSan, string HoTen, string SoDienThoai, DateTime NgayDat, string GioBatDau, string GioKetThuc, decimal TienCoc, string? GhiChu, string TrangThai)
    {
        try
        {
            TimeSpan start = TimeSpan.Parse(GioBatDau);
            TimeSpan end = TimeSpan.Parse(GioKetThuc);

            bool isOverlapping = await _context.DatSans.AnyAsync(x => 
                x.MaSan == MaSan && 
                x.NgayDat.Date == NgayDat.Date && 
                x.TrangThai != "Da huy" &&
                ((start >= x.GioBatDau && start < x.GioKetThuc) || 
                 (end > x.GioBatDau && end <= x.GioKetThuc) || 
                 (start <= x.GioBatDau && end >= x.GioKetThuc)));

            if (isOverlapping)
            {
                TempData["Error"] = "Sân đã có lịch đặt trong khoảng thời gian này!";
                return RedirectToAction("Index", new { ngay = NgayDat.ToString("yyyy-MM-dd"), maSan = MaSan });
            }

            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.SoDienThoai == SoDienThoai);
            if (khachHang == null)
            {
                khachHang = new KhachHang { HoTen = HoTen, SoDienThoai = SoDienThoai };
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
            }

            var datSan = new DatSan
            {
                MaSan = MaSan,
                MaKH = khachHang.MaKH,
                NgayDat = NgayDat,
                GioBatDau = start,
                GioKetThuc = end,
                ThoiLuong = (end - start).TotalHours,
                TienCoc = TienCoc,
                TrangThai = TrangThai,
                GhiChu = GhiChu,
                NgayTao = DateTime.Now
            };

            _context.DatSans.Add(datSan);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đặt sân thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Lỗi: " + ex.Message;
        }

        return RedirectToAction("Index", new { ngay = NgayDat.ToString("yyyy-MM-dd"), maSan = MaSan });
    }

    // --- CLIENT ACTIONS ---

    [AllowAnonymous]
    public async Task<IActionResult> CreateClient(int? maSan)
    {
        if (maSan == null)
        {
            return RedirectToAction("TimSan", "SanBong");
        }

        var sanBong = await _context.SanBongs
            .Include(s => s.SanCons)
            .FirstOrDefaultAsync(s => s.MaSan == maSan);

        if (sanBong == null) return NotFound();

        var viewModel = new DatSanViewModel
        {
            MaSan = sanBong.MaSan,
            TenSan = sanBong.TenSan,
            DiaChi = sanBong.DiaChi ?? "",
            HinhAnh = sanBong.HinhAnh ?? "",
            GiaMacDinh = sanBong.GiaMacDinh,
            DanhSachSanCon = sanBong.SanCons.Where(c => c.TrangThai == "Hoat dong").ToList(),
            NgayDat = DateTime.Today,
            ThoiLuong = 1
        };

        // Pre-fill user info if logged in
        var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
        if (!string.IsNullOrEmpty(tenDangNhap))
        {
            var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(k => k.SoDienThoai == tenDangNhap || k.GhiChu == tenDangNhap);
            if (khachHang != null)
            {
                viewModel.HoTen = khachHang.HoTen;
                viewModel.SoDienThoai = khachHang.SoDienThoai;
                viewModel.Email = khachHang.GhiChu;
            }
        }

        return View("CreateClient", viewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateClient(DatSanViewModel model)
    {
        var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
        if (maNguoiDung == null)
        {
            TempData["Error"] = "Vui lòng đăng nhập trước khi đặt sân.";
            return RedirectToAction("CreateClient", new { maSan = model.MaSan });
        }

        if (string.IsNullOrEmpty(model.HoTen) || string.IsNullOrEmpty(model.SoDienThoai))
        {
            ModelState.AddModelError("", "Vui lòng nhập đầy đủ họ tên và số điện thoại.");
        }

        if (model.MaSanCon <= 0)
        {
            ModelState.AddModelError("", "Vui lòng chọn sân.");
        }

        if (ModelState.IsValid)
        {
            var isConflict = await _context.DatSans.AnyAsync(x => 
                x.MaSan == model.MaSan && 
                x.NgayDat.Date == model.NgayDat.Date && 
                x.TrangThai != "Da huy" &&
                ((model.GioBatDau >= x.GioBatDau && model.GioBatDau < x.GioKetThuc) ||
                 (model.GioBatDau.Add(TimeSpan.FromHours(model.ThoiLuong)) > x.GioBatDau && model.GioBatDau.Add(TimeSpan.FromHours(model.ThoiLuong)) <= x.GioKetThuc) ||
                 (model.GioBatDau <= x.GioBatDau && model.GioBatDau.Add(TimeSpan.FromHours(model.ThoiLuong)) >= x.GioKetThuc)));

            if (isConflict)
            {
                ModelState.AddModelError("", "Sân này đã được đặt trong khung giờ này. Vui lòng chọn khung giờ hoặc sân khác!");
            }
            else
            {
                var khachHang = await _context.KhachHangs.FirstOrDefaultAsync(k => k.SoDienThoai == model.SoDienThoai);
                if (khachHang == null)
                {
                    khachHang = new KhachHang
                    {
                        HoTen = model.HoTen,
                        SoDienThoai = model.SoDienThoai,
                        GhiChu = model.Email,
                        NhomKhach = "Khach moi"
                    };
                    _context.KhachHangs.Add(khachHang);
                    await _context.SaveChangesAsync();
                }

                // Tự động tính giá
                var dayOfWeek = model.NgayDat.DayOfWeek;
                string groupName = (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday) 
                    ? "Thu 7 - Chu nhat" : "Thu 2 - Thu 6";

                var priceList = await _context.BangGiaKhungGios
                    .Where(x => x.NgayTrongTuan == groupName && model.GioBatDau >= x.GioBatDau && model.GioBatDau < x.GioKetThuc)
                    .FirstOrDefaultAsync();

                decimal totalAmount = 0;
                var sanBongSelect = await _context.SanBongs.FindAsync(model.MaSan);
                decimal giaMacDinh = sanBongSelect?.GiaMacDinh ?? 0;
                
                if (priceList != null)
                {
                    totalAmount = (priceList.DonGia + priceList.PhuThuCaoDiem) * (decimal)model.ThoiLuong;
                }
                else
                {
                    totalAmount = giaMacDinh * (decimal)model.ThoiLuong;
                }

                var datSan = new DatSan
                {
                    MaKH = khachHang.MaKH,
                    MaSan = model.MaSan,
                    MaSanCon = model.MaSanCon,
                    MaBangGia = priceList?.MaBangGia,
                    NgayDat = model.NgayDat,
                    GioBatDau = model.GioBatDau,
                    GioKetThuc = model.GioBatDau.Add(TimeSpan.FromHours(model.ThoiLuong)),
                    ThoiLuong = model.ThoiLuong,
                    TongTien = totalAmount,
                    TienCoc = 0,
                    TrangThai = "Cho xac nhan",
                    GhiChu = model.GhiChu,
                    NgayTao = DateTime.Now
                };

                _context.DatSans.Add(datSan);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Success), new { id = datSan.MaDatSan });
            }
        }
        
        var sanBong = await _context.SanBongs.Include(s => s.SanCons).FirstOrDefaultAsync(s => s.MaSan == model.MaSan);
        if (sanBong != null)
        {
            model.DanhSachSanCon = sanBong.SanCons.Where(c => c.TrangThai == "Hoat dong").ToList();
        }
        
        return View("CreateClient", model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Success(int id)
    {
        var datSan = await _context.DatSans
            .Include(d => d.SanBong)
            .Include(d => d.SanCon)
            .Include(d => d.KhachHang)
            .FirstOrDefaultAsync(d => d.MaDatSan == id);
            
        if (datSan == null) return NotFound();
        return View(datSan);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailableTimes(int maSanCon, DateTime ngayDat)
    {
        var bookedTimes = await _context.DatSans
            .Where(d => d.MaSanCon == maSanCon && d.NgayDat.Date == ngayDat.Date && d.TrangThai != "Da huy")
            .Select(d => new { d.GioBatDau, d.GioKetThuc })
            .ToListAsync();

        return Json(bookedTimes);
    }

    // --- ADMIN ACTIONS ---

    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public IActionResult Create()
    {
        ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "HoTen");
        ViewData["MaSan"] = new SelectList(_context.SanBongs.Where(s => s.TrangThai == "Hoat dong"), "MaSan", "TenSan");
        return View(new DatSan { NgayDat = DateTime.Today, TrangThai = "Cho xac nhan" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public async Task<IActionResult> Create(DatSan datSan)
    {
        if (ModelState.IsValid)
        {
            // Kiểm tra trùng lịch
            bool isOverlapping = await _context.DatSans.AnyAsync(x => 
                x.MaSan == datSan.MaSan && 
                x.NgayDat.Date == datSan.NgayDat.Date && 
                x.TrangThai != "Da huy" &&
                ((datSan.GioBatDau >= x.GioBatDau && datSan.GioBatDau < x.GioKetThuc) || 
                 (datSan.GioKetThuc > x.GioBatDau && datSan.GioKetThuc <= x.GioKetThuc) || 
                 (datSan.GioBatDau <= x.GioBatDau && datSan.GioKetThuc >= x.GioKetThuc)));

            if (isOverlapping)
            {
                ModelState.AddModelError("", "Sân đã có lịch đặt trong khoảng thời gian này!");
                ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "HoTen", datSan.MaKH);
                ViewData["MaSan"] = new SelectList(_context.SanBongs.Where(s => s.TrangThai == "Hoat dong"), "MaSan", "TenSan", datSan.MaSan);
                return View(datSan);
            }

            // Tự động tính giá
            var dayOfWeek = datSan.NgayDat.DayOfWeek;
            string groupName = (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday) 
                ? "Thu 7 - Chu nhat" : "Thu 2 - Thu 6";

            var priceList = await _context.BangGiaKhungGios
                .Where(x => x.NgayTrongTuan == groupName && datSan.GioBatDau >= x.GioBatDau && datSan.GioBatDau < x.GioKetThuc)
                .FirstOrDefaultAsync();

            decimal totalAmount = 0;
            var sanBongSelect = await _context.SanBongs.FindAsync(datSan.MaSan);
            decimal giaMacDinh = sanBongSelect?.GiaMacDinh ?? 0;
            double duration = (datSan.GioKetThuc - datSan.GioBatDau).TotalHours;

            if (priceList != null)
            {
                totalAmount = (priceList.DonGia + priceList.PhuThuCaoDiem) * (decimal)duration;
            }
            else
            {
                totalAmount = giaMacDinh * (decimal)duration;
            }

            datSan.MaBangGia = priceList?.MaBangGia;
            datSan.TongTien = totalAmount;
            datSan.ThoiLuong = duration;
            datSan.NgayTao = DateTime.Now;
            
            _context.Add(datSan);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm lịch đặt sân thành công!";
            return RedirectToAction(nameof(Index));
        }
        
        ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "HoTen", datSan.MaKH);
        ViewData["MaSan"] = new SelectList(_context.SanBongs.Where(s => s.TrangThai == "Hoat dong"), "MaSan", "TenSan", datSan.MaSan);
        return View(datSan);
    }

    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var datSan = await _context.DatSans.FindAsync(id);
        if (datSan == null) return NotFound();

        ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "HoTen", datSan.MaKH);
        ViewData["MaSan"] = new SelectList(_context.SanBongs.Where(s => s.TrangThai == "Hoat dong"), "MaSan", "TenSan", datSan.MaSan);
        return View(datSan);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public async Task<IActionResult> Edit(int id, DatSan datSan)
    {
        if (id != datSan.MaDatSan) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(datSan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật đặt sân thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatSanExists(datSan.MaDatSan)) return NotFound();
                else throw;
            }
        }
        
        ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "HoTen", datSan.MaKH);
        ViewData["MaSan"] = new SelectList(_context.SanBongs.Where(s => s.TrangThai == "Hoat dong"), "MaSan", "TenSan", datSan.MaSan);
        return View(datSan);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public async Task<IActionResult> XacNhan(int id)
    {
        var datSan = await _context.DatSans.FindAsync(id);
        if (datSan != null)
        {
            datSan.TrangThai = "Da xac nhan";
            _context.Update(datSan);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Đã duyệt đơn đặt sân thành công!";
        }
        return RedirectToAction("Dashboard", "Admin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public async Task<IActionResult> HuyDon(int id)
    {
        var datSan = await _context.DatSans.FindAsync(id);
        if (datSan != null)
        {
            datSan.TrangThai = "Da huy";
            _context.Update(datSan);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Đã hủy đơn đặt sân!";
        }
        return RedirectToAction("Dashboard", "Admin");
    }

    [SessionAuthorize("Admin", "Quan ly", "Nhan vien")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var datSan = await _context.DatSans
            .Include(d => d.BangGiaKhungGio)
            .Include(d => d.KhachHang)
            .Include(d => d.SanBong)
            .Include(d => d.SanCon)
            .FirstOrDefaultAsync(m => m.MaDatSan == id);
            
        if (datSan == null) return NotFound();

        return View(datSan);
    }

    private bool DatSanExists(int id)
    {
        return _context.DatSans.Any(e => e.MaDatSan == id);
    }
}
