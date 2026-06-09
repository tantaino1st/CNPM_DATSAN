using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class SanCon
{
    [Key]
    public int MaSanCon { get; set; }

    [Required]
    public int MaSan { get; set; }

    [ForeignKey(nameof(MaSan))]
    public SanBong? SanBong { get; set; }

    [Required]
    [StringLength(100)]
    public string TenSanCon { get; set; } = string.Empty;

    public int SucChua { get; set; } = 5;

    [StringLength(30)]
    public string TrangThai { get; set; } = "Hoat dong";
}
