namespace QUANLYSANBONG.Models;

public class DatSanViewModel
{
    public int MaSan { get; set; }
    public string TenSan { get; set; } = string.Empty;
    public string DiaChi { get; set; } = string.Empty;
    public string HinhAnh { get; set; } = string.Empty;
    public decimal GiaMacDinh { get; set; }
    public List<SanCon> DanhSachSanCon { get; set; } = new List<SanCon>();

    public int MaSanCon { get; set; }
    public DateTime NgayDat { get; set; } = DateTime.Today;
    public TimeSpan GioBatDau { get; set; }
    public double ThoiLuong { get; set; }
    public decimal TongTien { get; set; }

    public string HoTen { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? GhiChu { get; set; }
}
