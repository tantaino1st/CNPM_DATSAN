using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class SanBong
{
    [Key]
    public int MaSan { get; set; }

    [Required(ErrorMessage = "Ten san la bat buoc")]
    [StringLength(100)]
    public string TenSan { get; set; } = string.Empty;

    [StringLength(200)]
    public string? DiaChi { get; set; }

    [StringLength(100)]
    public string? TinhThanh { get; set; }

    public int SoLuongSanCon { get; set; } = 1;

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    [Required(ErrorMessage = "Loai san la bat buoc")]
    public int MaLoaiSan { get; set; }

    [ForeignKey(nameof(MaLoaiSan))]
    public LoaiSan? LoaiSan { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Gia mac dinh phai lon hon hoac bang 0")]
    public decimal GiaMacDinh { get; set; }

    [StringLength(30)]
    public string TrangThai { get; set; } = "Hoat dong";

    [StringLength(1000)]
    public string? MoTa { get; set; }

    [StringLength(15)]
    public string? SoDienThoai { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public ICollection<DatSan> DatSans { get; set; } = new List<DatSan>();
    public ICollection<SanCon> SanCons { get; set; } = new List<SanCon>();
}
