using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class DichVu
{
    [Key]
    public int MaDichVu { get; set; }

    [Required(ErrorMessage = "Ten dich vu la bat buoc")]
    [StringLength(100)]
    public string TenDichVu { get; set; } = string.Empty;

    [Required(ErrorMessage = "Don vi tinh la bat buoc")]
    [StringLength(30)]
    public string DonViTinh { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Don gia phai lon hon hoac bang 0")]
    public decimal DonGia { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Ton kho phai lon hon hoac bang 0")]
    public int? TonKho { get; set; }

    public ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();
}
