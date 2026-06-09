using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class GioHoatDong
{
    [Key]
    public int Id { get; set; }
    public string Thu { get; set; } = string.Empty;
    public bool DangHoatDong { get; set; } = true;
    public TimeSpan GioMoCua { get; set; } = new TimeSpan(6, 0, 0);
    public TimeSpan GioDongCua { get; set; } = new TimeSpan(22, 0, 0);
}
