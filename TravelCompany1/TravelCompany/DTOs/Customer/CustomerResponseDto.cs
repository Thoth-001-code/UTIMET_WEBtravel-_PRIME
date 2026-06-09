namespace TravelCompany.API.DTOs.Customer;

public class CustomerResponseDto
{
    public int MaKhachHang { get; set; }
    public string HoTen { get; set; } = null!;
    public string SoDienThoai { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? GioiTinh { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string? DiaChi { get; set; }
    public string LoaiKhach { get; set; } = null!;
    public DateTime NgayTao { get; set; }
}