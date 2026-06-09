using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Data;
using QUANLYSANBONG.Filters;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Controllers;

[SessionAuthorize("Admin", "Quan ly", "Ke toan", "Nhan vien")]
public class HoaDonController : Controller
{
    private readonly ApplicationDbContext _context;

    public HoaDonController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string trangThai)
    {
        var hoaDons = _context.HoaDons
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.KhachHang)
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.SanBong)
            .AsQueryable();

        if (fromDate.HasValue)
        {
            hoaDons = hoaDons.Where(h => h.NgayThanhToan >= fromDate.Value.Date);
            ViewData["FromDate"] = fromDate.Value.ToString("yyyy-MM-dd");
        }

        if (toDate.HasValue)
        {
            hoaDons = hoaDons.Where(h => h.NgayThanhToan <= toDate.Value.Date.AddDays(1).AddTicks(-1));
            ViewData["ToDate"] = toDate.Value.ToString("yyyy-MM-dd");
        }

        if (!string.IsNullOrEmpty(trangThai))
        {
            hoaDons = hoaDons.Where(h => h.TrangThaiThanhToan == trangThai);
            ViewData["CurrentTrangThai"] = trangThai;
        }

        return View(await hoaDons.OrderByDescending(h => h.NgayThanhToan).ToListAsync());
    }

    public async Task<IActionResult> Create(int? maDatSan)
    {
        if (maDatSan == null) return NotFound();

        var datSan = await _context.DatSans
            .Include(d => d.BangGiaKhungGio)
            .FirstOrDefaultAsync(m => m.MaDatSan == maDatSan);

        if (datSan == null) return NotFound();

        // Check if invoice already exists
        var existingHoaDon = await _context.HoaDons.FirstOrDefaultAsync(h => h.MaDatSan == maDatSan);
        if (existingHoaDon != null)
        {
            return RedirectToAction(nameof(Edit), new { id = existingHoaDon.MaHD });
        }

        decimal tienSan = 0;
        if (datSan.BangGiaKhungGio != null)
        {
            tienSan = datSan.BangGiaKhungGio.DonGia + datSan.BangGiaKhungGio.PhuThuCaoDiem;
        }
        else
        {
            tienSan = datSan.TongTien;
        }

        var hoaDon = new HoaDon
        {
            MaDatSan = datSan.MaDatSan,
            NgayThanhToan = DateTime.Now,
            TienSan = tienSan,
            TienDichVu = 0,
            GiamGia = 0,
            PhuThu = 0,
            TongTien = tienSan - datSan.TienCoc,
            TrangThaiThanhToan = "Chua thanh toan",
            PhuongThucThanhToan = "Tien mat"
        };

        ViewData["DatSanInfo"] = $"Sân: {datSan.MaSan} - Ngày: {datSan.NgayDat:dd/MM/yyyy} - Khách: {datSan.MaKH}";
        return View(hoaDon);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HoaDon hoaDon)
    {
        if (ModelState.IsValid)
        {
            var datSan = await _context.DatSans.FindAsync(hoaDon.MaDatSan);
            if (datSan != null)
            {
                hoaDon.TongTien = hoaDon.TienSan + hoaDon.TienDichVu + hoaDon.PhuThu - hoaDon.GiamGia - datSan.TienCoc;
            }

            _context.Add(hoaDon);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Lập hóa đơn thành công!";
            return RedirectToAction(nameof(Edit), new { id = hoaDon.MaHD });
        }
        return View(hoaDon);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var hoaDon = await _context.HoaDons
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.SanBong)
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.KhachHang)
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.BangGiaKhungGio)
            .FirstOrDefaultAsync(m => m.MaHD == id);
            
        if (hoaDon == null) return NotFound();

        var chiTietDichVus = await _context.ChiTietDichVus
            .Include(c => c.DichVu)
            .Where(c => c.MaDatSan == hoaDon.MaDatSan)
            .ToListAsync();

        ViewBag.ChiTietDichVus = chiTietDichVus;
        ViewData["DichVus"] = new SelectList(_context.DichVus, "MaDichVu", "TenDichVu");
        
        return View(hoaDon);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, HoaDon hoaDon)
    {
        if (id != hoaDon.MaHD) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var datSan = await _context.DatSans.FindAsync(hoaDon.MaDatSan);
                decimal tienCoc = datSan?.TienCoc ?? 0;

                hoaDon.TongTien = hoaDon.TienSan + hoaDon.TienDichVu + hoaDon.PhuThu - hoaDon.GiamGia - tienCoc;

                if (hoaDon.TrangThaiThanhToan == "Da thanh toan" && datSan != null)
                {
                    datSan.TrangThai = "Da hoan thanh";
                    _context.Update(datSan);
                }

                _context.Update(hoaDon);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật hóa đơn thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonExists(hoaDon.MaHD)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(hoaDon);
    }

    [HttpPost]
    public async Task<IActionResult> AddDichVu(int maHD, int maDichVu, int soLuong)
    {
        var dichVu = await _context.DichVus.FindAsync(maDichVu);
        var hoaDon = await _context.HoaDons.FindAsync(maHD);

        if (dichVu != null && hoaDon != null && soLuong > 0)
        {
            var chiTiet = await _context.ChiTietDichVus
                .FirstOrDefaultAsync(c => c.MaDatSan == hoaDon.MaDatSan && c.MaDichVu == maDichVu);

            if (chiTiet != null)
            {
                chiTiet.SoLuong += soLuong;
                chiTiet.ThanhTien = chiTiet.SoLuong * chiTiet.DonGia;
                _context.Update(chiTiet);
            }
            else
            {
                chiTiet = new ChiTietDichVu
                {
                    MaDatSan = hoaDon.MaDatSan,
                    MaDichVu = maDichVu,
                    SoLuong = soLuong,
                    DonGia = dichVu.DonGia,
                    ThanhTien = soLuong * dichVu.DonGia
                };
                _context.Add(chiTiet);
            }

            // Update Ton Kho
            dichVu.TonKho -= soLuong;
            _context.Update(dichVu);

            await _context.SaveChangesAsync();

            // Recalculate TienDichVu in HoaDon
            hoaDon.TienDichVu = await _context.ChiTietDichVus.Where(c => c.MaDatSan == hoaDon.MaDatSan).SumAsync(c => c.ThanhTien);
            
            var datSan = await _context.DatSans.FindAsync(hoaDon.MaDatSan);
            decimal tienCoc = datSan?.TienCoc ?? 0;
            hoaDon.TongTien = hoaDon.TienSan + hoaDon.TienDichVu + hoaDon.PhuThu - hoaDon.GiamGia - tienCoc;
            
            _context.Update(hoaDon);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Thêm dịch vụ thành công!";
        }

        return RedirectToAction(nameof(Edit), new { id = maHD });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveDichVu(int maHD, int maDichVu)
    {
        var hoaDon = await _context.HoaDons.FindAsync(maHD);
        if (hoaDon == null) return NotFound();

        var chiTiet = await _context.ChiTietDichVus
            .FirstOrDefaultAsync(c => c.MaDatSan == hoaDon.MaDatSan && c.MaDichVu == maDichVu);

        if (chiTiet != null)
        {
            var dichVu = await _context.DichVus.FindAsync(maDichVu);
            if (dichVu != null)
            {
                dichVu.TonKho += chiTiet.SoLuong;
                _context.Update(dichVu);
            }

            _context.ChiTietDichVus.Remove(chiTiet);
            await _context.SaveChangesAsync();

            // Recalculate TienDichVu in HoaDon
            hoaDon.TienDichVu = await _context.ChiTietDichVus.Where(c => c.MaDatSan == hoaDon.MaDatSan).SumAsync(c => c.ThanhTien);
            var datSan = await _context.DatSans.FindAsync(hoaDon.MaDatSan);
            decimal tienCoc = datSan?.TienCoc ?? 0;
            hoaDon.TongTien = hoaDon.TienSan + hoaDon.TienDichVu + hoaDon.PhuThu - hoaDon.GiamGia - tienCoc;
            
            _context.Update(hoaDon);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Xóa dịch vụ thành công!";
        }

        return RedirectToAction(nameof(Edit), new { id = maHD });
    }

    public async Task<IActionResult> Print(int? id)
    {
        if (id == null) return NotFound();

        var hoaDon = await _context.HoaDons
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.SanBong)
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.KhachHang)
            .Include(h => h.DatSan)
                .ThenInclude(d => d!.BangGiaKhungGio)
            .FirstOrDefaultAsync(m => m.MaHD == id);
            
        if (hoaDon == null) return NotFound();

        var chiTietDichVus = await _context.ChiTietDichVus
            .Include(c => c.DichVu)
            .Where(c => c.MaDatSan == hoaDon.MaDatSan)
            .ToListAsync();

        ViewBag.ChiTietDichVus = chiTietDichVus;
        return View(hoaDon);
    }

    private bool HoaDonExists(int id)
    {
        return _context.HoaDons.Any(e => e.MaHD == id);
    }
}
