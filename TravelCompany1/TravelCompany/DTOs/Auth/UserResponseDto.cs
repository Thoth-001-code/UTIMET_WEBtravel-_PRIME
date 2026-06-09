namespace TravelCompany.API.DTOs.Auth;

public class UserResponseDto
{
    public int MaTaiKhoan { get; set; }
    public string HoTen { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string SoDienThoai { get; set; } = null!;
    public string VaiTro { get; set; } = null!;
    public string TrangThai { get; set; } = null!;
    public DateTime NgayTao { get; set; }
}