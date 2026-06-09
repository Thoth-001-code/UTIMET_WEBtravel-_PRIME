using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("DiemDen")]
public class DiemDen
{
    [Key]
    public int MaDiemDen { get; set; }

    [Required, MaxLength(150)]
    public string TenDiemDen { get; set; } = null!;

    [MaxLength(100)]
    public string? QuocGia { get; set; }

    [MaxLength(100)]
    public string? ThanhPho { get; set; }

    public string? MoTa { get; set; }

    // Navigation
    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}