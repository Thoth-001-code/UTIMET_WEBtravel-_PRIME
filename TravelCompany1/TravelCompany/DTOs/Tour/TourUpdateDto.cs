namespace TravelCompany.API.DTOs.Tour;

public class TourUpdateDto
{
    public int MaTour { get; set; }
    public string? MaCodeTour { get; set; }
    public string? TenTour { get; set; }
    public int? MaDiemDen { get; set; }
    public string? MoTa { get; set; }
    public int? SoNgay { get; set; }
    public decimal? GiaCoBan { get; set; }
    public string? TrangThai { get; set; }
}