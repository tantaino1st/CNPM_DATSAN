using System.ComponentModel.DataAnnotations;

namespace QUANLYSANBONG.Models;

public class CaiDatThongBao
{
    [Key]
    public int Id { get; set; }
    public string TenThongBao { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;
    public bool DangBat { get; set; } = true;
    public string Icon { get; set; } = "fas fa-bell";
}
