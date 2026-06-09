namespace TravelCompany.API.DTOs.Auth;

public class RegisterDto
{
    public string HoTen { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string SoDienThoai { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string VaiTro { get; set; } = "nhan_vien"; // quan_tri, quan_ly, nhan_vien, ke_toan
}