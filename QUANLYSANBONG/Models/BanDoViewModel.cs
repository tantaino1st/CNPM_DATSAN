using System.Collections.Generic;

namespace QUANLYSANBONG.Models
{
    public class BanDoViewModel
    {
        public List<SanBong> DanhSachSan { get; set; } = new();
        public string? Keyword { get; set; }
        public string? LoaiSan { get; set; }
    }
}
