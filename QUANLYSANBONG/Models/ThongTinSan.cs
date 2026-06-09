using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class ThongTinSan
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Tên sân không được để trống")]
    public string TenSan { get; set; } = string.Empty;
    public string? SoDienThoai { get; set; }
    public string? DiaChi { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? MoTa { get; set; }
    public string? LogoUrl { get; set; }
}
