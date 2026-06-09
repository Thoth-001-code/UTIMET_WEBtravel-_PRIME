using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Tour;

public class LichKhoiHanhCreateDto
{
    public int MaTour { get; set; }

    [Required]
    public DateTime NgayKhoiHanh { get; set; }

    [Required]
    public DateTime NgayKetThuc { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int SoChoToiDa { get; set; }

    [Required, Range(0, double.MaxValue)]
    public decimal GiaTour { get; set; }

    [MaxLength(50)]
    public string? TrangThai { get; set; } // con_cho, het_cho, da_khoi_hanh, da_huy
}