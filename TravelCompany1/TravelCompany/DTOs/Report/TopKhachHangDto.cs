namespace TravelCompany.API.DTOs.Report;

public class TopKhachHangDto
{
    public int MaKhachHang { get; set; }
    public string HoTen { get; set; } = null!;
    public string SoDienThoai { get; set; } = null!;
    public string? Email { get; set; }
    public int SoLuongBooking { get; set; }
    public decimal TongChiTieu { get; set; }
}