using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class DatSan
{
    [Key]
    public int MaDatSan { get; set; }

    [Required(ErrorMessage = "Khach hang la bat buoc")]
    public int MaKH { get; set; }

    [ForeignKey(nameof(MaKH))]
    public KhachHang? KhachHang { get; set; }

    [Required(ErrorMessage = "San bong la bat buoc")]
    public int MaSan { get; set; }

    [ForeignKey(nameof(MaSan))]
    public SanBong? SanBong { get; set; }

    public int? MaBangGia { get; set; }

    [ForeignKey(nameof(MaBangGia))]
    public BangGiaKhungGio? BangGiaKhungGio { get; set; }

    public int? MaSanCon { get; set; }

    [ForeignKey(nameof(MaSanCon))]
    public SanCon? SanCon { get; set; }

    [DataType(DataType.Date)]
    public DateTime NgayDat { get; set; } = DateTime.Today;

    public TimeSpan GioBatDau { get; set; }
    public TimeSpan GioKetThuc { get; set; }
    public double ThoiLuong { get; set; }

    public decimal TongTien { get; set; }

    public DateTime NgayTao { get; set; } = DateTime.Now;

    [StringLength(30)]
    public string TrangThai { get; set; } = "Cho xac nhan";

    [Range(0, double.MaxValue, ErrorMessage = "Tien coc phai lon hon hoac bang 0")]
    public decimal TienCoc { get; set; }

    [StringLength(500)]
    public string? GhiChu { get; set; }

    public ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();
    public HoaDon? HoaDon { get; set; }
}
