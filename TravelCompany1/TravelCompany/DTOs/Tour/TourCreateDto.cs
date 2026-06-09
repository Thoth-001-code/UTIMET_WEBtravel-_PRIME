using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Tour;

public class TourCreateDto
{
    [Required, MaxLength(50)]
    public string MaCodeTour { get; set; } = null!;

    [Required, MaxLength(150)]
    public string TenTour { get; set; } = null!;

    public int? MaDiemDen { get; set; }

    public string? MoTa { get; set; }

    [Required]
    public int SoNgay { get; set; }

    [Required, Range(0, double.MaxValue)]
    public decimal GiaCoBan { get; set; }

    [MaxLength(50)]
    public string? TrangThai { get; set; } // dang_mo, tam_ngung, da_huy
}