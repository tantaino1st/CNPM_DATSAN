using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class ChiTietDichVu
{
    [Key]
    public int MaCTDV { get; set; }

    [Required]
    public int MaDatSan { get; set; }

    [ForeignKey(nameof(MaDatSan))]
    public DatSan? DatSan { get; set; }

    [Required]
    public int MaDichVu { get; set; }

    [ForeignKey(nameof(MaDichVu))]
    public DichVu? DichVu { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "So luong phai lon hon 0")]
    public int SoLuong { get; set; } = 1;

    [Range(0, double.MaxValue, ErrorMessage = "Don gia phai lon hon hoac bang 0")]
    public decimal DonGia { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ThanhTien { get; set; }
}
