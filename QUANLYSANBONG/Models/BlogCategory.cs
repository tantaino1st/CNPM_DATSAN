using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class BlogCategory
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
    [StringLength(100)]
    public string TenDanhMuc { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(500)]
    public string? MoTa { get; set; }

    public bool TrangThai { get; set; } = true;

    public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}
