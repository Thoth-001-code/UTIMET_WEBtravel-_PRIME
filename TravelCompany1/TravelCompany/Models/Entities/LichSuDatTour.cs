using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("LichSuDatTour")]
public class LichSuDatTour
{
    [Key]
    public int MaLichSu { get; set; }

    public int MaDatTour { get; set; }
    public int? MaTaiKhoan { get; set; }

    [Required, MaxLength(100)]
    public string HanhDong { get; set; } = null!;

    [MaxLength(50)]
    public string? TrangThaiCu { get; set; }

    [MaxLength(50)]
    public string? TrangThaiMoi { get; set; }

    public string? GhiChu { get; set; }

    public DateTime NgayTao { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual DatTour DatTour { get; set; } = null!;
    public virtual TaiKhoan? TaiKhoan { get; set; }
}