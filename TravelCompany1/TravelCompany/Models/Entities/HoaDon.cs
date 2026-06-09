using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("HoaDon")]
public class HoaDon
{
    [Key]
    public int MaHoaDon { get; set; }

    [Required, MaxLength(50)]
    public string MaCodeHoaDon { get; set; } = null!;

    public int MaDatTour { get; set; }
    public DateTime NgayXuat { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TongTien { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DaThanhToan { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ConLai { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; } = "chua_thanh_toan";

    // Navigation
    public virtual DatTour DatTour { get; set; } = null!;
}