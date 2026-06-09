using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Booking;

public class BookingStatusUpdateDto
{
    [Required]
    public int MaDatTour { get; set; }

    [Required, MaxLength(50)]
    public string TrangThaiMoi { get; set; } = null!;

    public string? GhiChu { get; set; }
}