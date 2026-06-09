using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class NguoiDung
{
    [Key]
    public int MaNguoiDung { get; set; }

    [Required(ErrorMessage = "Ten dang nhap la bat buoc")]
    [StringLength(50)]
    public string TenDangNhap { get; set; } = string.Empty;

    [StringLength(100)]
    public string? HoTen { get; set; }

    [StringLength(15)]
    public string? SoDienThoai { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Mat khau la bat buoc")]
    [StringLength(100)]
    public string MatKhau { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vai tro la bat buoc")]
    public int MaVaiTro { get; set; }

    [ForeignKey(nameof(MaVaiTro))]
    public VaiTro? VaiTro { get; set; }

    public bool TrangThai { get; set; } = true;

    public NhanVien? NhanVien { get; set; }
    public ICollection<NhatKyThaoTac> NhatKyThaoTacs { get; set; } = new List<NhatKyThaoTac>();
}
