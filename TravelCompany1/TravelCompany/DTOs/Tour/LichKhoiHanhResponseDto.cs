namespace TravelCompany.API.DTOs.Tour;

public class LichKhoiHanhResponseDto
{
    public int MaLich { get; set; }
    public int MaTour { get; set; }
    public DateTime NgayKhoiHanh { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public int SoChoToiDa { get; set; }
    public int SoChoConLai { get; set; }
    public decimal GiaTour { get; set; }
    public string TrangThai { get; set; } = null!;
}