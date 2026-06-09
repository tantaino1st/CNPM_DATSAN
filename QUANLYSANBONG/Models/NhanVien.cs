using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class NhanVien
{
    [Key]
    public int MaNV { get; set; }

    [Required(ErrorMessage = "Ho ten la bat buoc")]
    [StringLength(100)]
    public string HoTen { get; set; } = string.Empty;

    [StringLength(15)]
    public string? SoDienThoai { get; set; }

    [EmailAddress(ErrorMessage = "Email khong dung dinh dang")]
    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? ChucVu { get; set; }

    public int? MaNguoiDung { get; set; }

    [ForeignKey(nameof(MaNguoiDung))]
    public NguoiDung? NguoiDung { get; set; }
}
