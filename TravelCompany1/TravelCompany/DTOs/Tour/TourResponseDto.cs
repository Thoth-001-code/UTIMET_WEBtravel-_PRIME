namespace TravelCompany.API.DTOs.Tour;

public class TourResponseDto
{
    public int MaTour { get; set; }
    public string MaCodeTour { get; set; } = null!;
    public string TenTour { get; set; } = null!;
    public int? MaDiemDen { get; set; }
    public string? TenDiemDen { get; set; } // để hiển thị tên điểm đến
    public string? MoTa { get; set; }
    public int SoNgay { get; set; }
    public decimal GiaCoBan { get; set; }
    public string TrangThai { get; set; } = null!;
    public DateTime NgayTao { get; set; }
    public List<LichKhoiHanhResponseDto> LichKhoiHanhs { get; set; } = new();
    public List<ChiTietLichTrinhResponseDto> ChiTietLichTrinhs { get; set; } = new();
    public List<AnhTourResponseDto> AnhTours { get; set; } = new();
}