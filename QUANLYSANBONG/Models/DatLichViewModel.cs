using System;
using System.Collections.Generic;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Models
{
    public class DatLichViewModel
    {
        public DateTime NgayHienTai { get; set; }
        public int? SelectedMaSan { get; set; }
        
        public List<SanBong> DanhSachTatCaSan { get; set; } = new();
        public List<SanBong> DanhSachSanHienThi { get; set; } = new();
        public List<DatSan> DanhSachDatSan { get; set; } = new();
        public List<GioHoatDong> GioHoatDongs { get; set; } = new();
        
        public int SoDaDat { get; set; }
        public int SoTrong { get; set; }
        public int SoDangChoi { get; set; }
        public double TiLeLapDay { get; set; }
        
        // Cấu hình Timeline
        public int GioBatDauHeThong { get; set; } = 6;
        public int GioKetThucHeThong { get; set; } = 22;
    }
}
