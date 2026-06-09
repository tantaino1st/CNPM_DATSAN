using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class BangGiaKhungGio
{
    [Key]
    public int MaBangGia { get; set; }

    [Required(ErrorMessage = "Ngay trong tuan la bat buoc")]
    [StringLength(30)]
    public string NgayTrongTuan { get; set; } = "Thu 2";

    [Required(ErrorMessage = "Gio bat dau la bat buoc")]
    public TimeSpan GioBatDau { get; set; }

    [Required(ErrorMessage = "Gio ket thuc la bat buoc")]
    public TimeSpan GioKetThuc { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Don gia phai lon hon hoac bang 0")]
    public decimal DonGia { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Phu thu phai lon hon hoac bang 0")]
    public decimal PhuThuCaoDiem { get; set; }

    public ICollection<DatSan> DatSans { get; set; } = new List<DatSan>();
}
