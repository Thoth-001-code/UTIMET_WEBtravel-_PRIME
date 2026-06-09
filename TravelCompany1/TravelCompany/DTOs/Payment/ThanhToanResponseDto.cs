namespace TravelCompany.API.DTOs.Payment;

public class ThanhToanResponseDto
{
    public int MaThanhToan { get; set; }
    public int MaDatTour { get; set; }
    public DateTime NgayThanhToan { get; set; }
    public decimal SoTien { get; set; }
    public string LoaiThanhToan { get; set; } = null!;
    public string PhuongThuc { get; set; } = null!;
    public string TrangThai { get; set; } = null!;
    public string? GhiChu { get; set; }
}