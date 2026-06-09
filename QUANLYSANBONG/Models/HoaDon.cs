using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class HoaDon
{
    [Key]
    public int MaHD { get; set; }

    [Required(ErrorMessage = "Phieu dat san la bat buoc")]
    public int MaDatSan { get; set; }

    [ForeignKey(nameof(MaDatSan))]
    public DatSan? DatSan { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TienSan { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TienDichVu { get; set; }

    [Range(0, double.MaxValue)]
    public decimal GiamGia { get; set; }

    [Range(0, double.MaxValue)]
    public decimal PhuThu { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TongTien { get; set; }

    [StringLength(30)]
    public string PhuongThucThanhToan { get; set; } = "Tien mat";

    [StringLength(30)]
    public string TrangThaiThanhToan { get; set; } = "Chua thanh toan";

    public DateTime? NgayThanhToan { get; set; }
}
