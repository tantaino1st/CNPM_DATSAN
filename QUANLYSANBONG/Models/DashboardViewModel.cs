using System;
using System.Collections.Generic;

namespace QUANLYSANBONG.Models
{
    public class DashboardViewModel
    {
        public int DonDatHomNay { get; set; }
        public decimal DoanhThuHomNay { get; set; }
        public int SoSanHoatDong { get; set; }
        public int TongSoSan { get; set; }
        public int KhachMoiThangNay { get; set; }

        public List<DoanhThuNgayViewModel> DoanhThuTheoNgay { get; set; } = new();
        public List<LoaiSanThongKeViewModel> ThongKeLoaiSan { get; set; } = new();
        public List<LichDatGanNhatViewModel> LichDatGanNhat { get; set; } = new();
    }

    public class DoanhThuNgayViewModel
    {
        public string Ngay { get; set; } = string.Empty;
        public decimal DoanhThu { get; set; }
    }

    public class LoaiSanThongKeViewModel
    {
        public string TenLoai { get; set; } = string.Empty;
        public int SoLuong { get; set; }
    }

    public class LichDatGanNhatViewModel
    {
        public int MaDatSan { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string TenSan { get; set; } = string.Empty;
        public string ThoiGian { get; set; } = string.Empty;
        public decimal Gia { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }
}
