using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Booking;

public class BookingCreateDto
{
    [Required]
    public int MaLich { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int SoLuongNguoi { get; set; }

    [Required]
    public List<NguoiDiTourDto> DanhSachNguoiDi { get; set; } = new();

    public string? GhiChu { get; set; }

    // Dành cho nhân viên tạo booking cho khách (optional)
    public int? MaKhachHang { get; set; }
} 