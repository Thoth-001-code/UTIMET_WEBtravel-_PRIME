using System.ComponentModel.DataAnnotations;
namespace TravelCompany.API.DTOs.Customer;

public class CustomerRegisterDto
{
    [Required, MaxLength(100)]
    public string HoTen { get; set; } = null!;
    [Required, MaxLength(100), EmailAddress]
    public string Email { get; set; } = null!;
    [Required, MaxLength(20)]
    public string SoDienThoai { get; set; } = null!;
    [Required, MinLength(6)]
    public string MatKhau { get; set; } = null!;
    public string? GioiTinh { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string? DiaChi { get; set; }
}