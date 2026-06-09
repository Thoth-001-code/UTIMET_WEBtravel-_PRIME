namespace TravelCompany.API.DTOs.Report;

public class DoanhThuTheoTourDto
{
    public int MaTour { get; set; }
    public string TenTour { get; set; } = null!;
    public int SoLuongBooking { get; set; }
    public int TongSoKhach { get; set; }
    public decimal TongDoanhThu { get; set; }
}