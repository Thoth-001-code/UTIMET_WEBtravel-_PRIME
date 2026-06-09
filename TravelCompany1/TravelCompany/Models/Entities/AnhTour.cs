using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("AnhTour")]
public class AnhTour
{
    [Key]
    public int MaAnh { get; set; }

    public int MaTour { get; set; }

    [Required]
    public string DuongDanAnh { get; set; } = null!;

    public string? MoTaAnh { get; set; }

    public int ThuTu { get; set; } = 0;

    // Navigation
    public virtual Tour Tour { get; set; } = null!;
}
