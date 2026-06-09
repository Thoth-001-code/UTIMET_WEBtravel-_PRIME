using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("LichKhoiHanh")]
public class LichKhoiHanh
{
    [Key]
    public int MaLich { get; set; }

    public int MaTour { get; set; }

    public DateTime NgayKhoiHanh { get; set; }
    public DateTime NgayKetThuc { get; set; }

    public int SoChoToiDa { get; set; }
    public int SoChoConLai { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal GiaTour { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; } = "con_cho";

    // Navigation
    public virtual Tour Tour { get; set; } = null!;
    public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
}