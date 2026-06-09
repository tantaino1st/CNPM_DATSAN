namespace QUANLYSANBONG.Models;

public class TimSanViewModel
{
    public List<SanBong> DanhSachSan { get; set; } = new List<SanBong>();
    public List<LoaiSan> DanhSachLoaiSan { get; set; } = new List<LoaiSan>();
    
    public string? Keyword { get; set; }
    public int? LoaiSanId { get; set; }
    public string? TinhThanh { get; set; }
    public string? Sort { get; set; }
    
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}
