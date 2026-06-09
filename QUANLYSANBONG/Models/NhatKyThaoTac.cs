using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class NhatKyThaoTac
{
    [Key]
    public int MaNhatKy { get; set; }

    public int? MaNguoiDung { get; set; }

    [ForeignKey(nameof(MaNguoiDung))]
    public NguoiDung? NguoiDung { get; set; }

    [Required]
    [StringLength(100)]
    public string HanhDong { get; set; } = string.Empty;

    public DateTime ThoiGian { get; set; } = DateTime.Now;

    [StringLength(1000)]
    public string? NoiDung { get; set; }
}
