namespace QUANLYSANBONG.Models;

public class CaiDatViewModel
{
    public ThongTinSan ThongTinSan { get; set; } = new();
    public List<GioHoatDong> DanhSachGioHoatDong { get; set; } = new();
    public List<CaiDatThongBao> DanhSachThongBao { get; set; } = new();
    public NguoiDung NguoiDung { get; set; } = new();
}
