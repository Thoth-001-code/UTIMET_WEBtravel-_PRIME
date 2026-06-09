using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Payment;

public class ThanhToanCreateDto
{
    [Required]
    public int MaDatTour { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal SoTien { get; set; }

    [Required, MaxLength(50)]
    public string LoaiThanhToan { get; set; } = null!;

    [Required, MaxLength(50)]
    public string PhuongThuc { get; set; } = null!;

    public string? GhiChu { get; set; }
}