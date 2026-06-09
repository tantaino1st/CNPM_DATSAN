using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class LoaiSan
{
    [Key]
    public int MaLoaiSan { get; set; }

    [Required(ErrorMessage = "Ten loai san la bat buoc")]
    [StringLength(50)]
    public string TenLoaiSan { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Icon { get; set; }

    [StringLength(200)]
    public string? MoTa { get; set; }

    public ICollection<SanBong> SanBongs { get; set; } = new List<SanBong>();
}
