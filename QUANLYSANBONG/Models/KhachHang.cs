using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class KhachHang
{
    [Key]
    public int MaKH { get; set; }

    [Required(ErrorMessage = "Ho ten la bat buoc")]
    [StringLength(100)]
    public string HoTen { get; set; } = string.Empty;

    [Required(ErrorMessage = "So dien thoai la bat buoc")]
    [StringLength(15)]
    public string SoDienThoai { get; set; } = string.Empty;

    [StringLength(30)]
    public string NhomKhach { get; set; } = "Khach moi";

    [StringLength(500)]
    public string? GhiChu { get; set; }

    public ICollection<DatSan> DatSans { get; set; } = new List<DatSan>();
}
