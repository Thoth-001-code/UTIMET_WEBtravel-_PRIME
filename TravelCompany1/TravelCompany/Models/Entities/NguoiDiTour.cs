using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("NguoiDiTour")]
public class NguoiDiTour
{
    [Key]
    public int MaNguoiDi { get; set; }

    public int MaDatTour { get; set; }

    [Required, MaxLength(100)]
    public string HoTen { get; set; } = null!;

    [MaxLength(10)]
    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    [MaxLength(50)]
    public string? SoCCCD { get; set; }

    [MaxLength(20)]
    public string? SoDienThoai { get; set; }

    // Navigation
    public virtual DatTour DatTour { get; set; } = null!;
}