using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class VaiTro
{
    [Key]
    public int MaVaiTro { get; set; }

    [Required(ErrorMessage = "Ten vai tro la bat buoc")]
    [StringLength(50)]
    public string TenVaiTro { get; set; } = string.Empty;

    [StringLength(200)]
    public string? MoTa { get; set; }

    public ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
}
