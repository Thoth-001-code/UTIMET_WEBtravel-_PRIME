using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Destination;

public class DiemDenCreateDto
{
    [Required, MaxLength(150)]
    public string TenDiemDen { get; set; } = null!;

    [MaxLength(100)]
    public string? QuocGia { get; set; }

    [MaxLength(100)]
    public string? ThanhPho { get; set; }

    public string? MoTa { get; set; }
}