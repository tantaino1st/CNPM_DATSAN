using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUANLYSANBONG.Models;

public class BlogPost
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
    [StringLength(255)]
    public string TieuDe { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mô tả ngắn là bắt buộc")]
    [StringLength(500)]
    public string MoTaNgan { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nội dung là bắt buộc")]
    public string NoiDung { get; set; } = string.Empty;

    [StringLength(255)]
    public string? AnhDaiDien { get; set; }

    [Required]
    public int BlogCategoryId { get; set; }

    [ForeignKey(nameof(BlogCategoryId))]
    public BlogCategory? Category { get; set; }

    public DateTime NgayDang { get; set; } = DateTime.Now;

    [StringLength(50)]
    public string ThoiGianDoc { get; set; } = "5 phút đọc";

    public int LuotXem { get; set; } = 0;

    public bool NoiBat { get; set; } = false;

    public bool TrangThai { get; set; } = true;

    public int? NguoiTaoId { get; set; }

    [ForeignKey(nameof(NguoiTaoId))]
    public NguoiDung? NguoiTao { get; set; }
}
