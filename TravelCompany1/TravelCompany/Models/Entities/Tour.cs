using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("Tour")]
public class Tour
{
    [Key]
    public int MaTour { get; set; }

    [Required, MaxLength(50)]
    public string MaCodeTour { get; set; } = null!;

    [Required, MaxLength(150)]
    public string TenTour { get; set; } = null!;

    public int? MaDiemDen { get; set; }

    public string? MoTa { get; set; }

    public int SoNgay { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal GiaCoBan { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; } = "dang_mo";

    public DateTime NgayTao { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual DiemDen? DiemDen { get; set; }
    public virtual ICollection<LichKhoiHanh> LichKhoiHanhs { get; set; } = new List<LichKhoiHanh>();
    public virtual ICollection<ChiTietLichTrinh> ChiTietLichTrinhs { get; set; } = new List<ChiTietLichTrinh>();
    public virtual ICollection<AnhTour> AnhTours { get; set; } = new List<AnhTour>();
}