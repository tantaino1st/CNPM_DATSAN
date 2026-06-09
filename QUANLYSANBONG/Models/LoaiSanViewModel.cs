using System.Collections.Generic;

namespace QUANLYSANBONG.Models
{
    public class LoaiSanViewModel
    {
        public string? TenLoaiSan { get; set; }
        public List<SanBong> DanhSachSan { get; set; } = new();
        public List<LoaiSan> DanhSachLoaiSan { get; set; } = new();
        public List<BlogPost> BaiVietNoiBat { get; set; } = new();
        public string? Keyword { get; set; }
        public string? QuanHuyen { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
