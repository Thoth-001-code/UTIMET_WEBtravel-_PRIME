using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Booking;

public class NguoiDiTourDto
{
    [Required, MaxLength(100)]
    public string HoTen { get; set; } = null!;

    [MaxLength(10)]
    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    [MaxLength(50)]
    public string? SoCCCD { get; set; }

    [MaxLength(20)]
    public string? SoDienThoai { get; set; }
}