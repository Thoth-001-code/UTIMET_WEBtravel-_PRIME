using TravelCompany.API.DTOs.Payment;

namespace TravelCompany.API.DTOs.Invoice;

public class HoaDonResponseDto
{
    public int MaHoaDon { get; set; }
    public string MaCodeHoaDon { get; set; } = null!;
    public int MaDatTour { get; set; }
    public DateTime NgayXuat { get; set; }
    public decimal TongTien { get; set; }
    public decimal DaThanhToan { get; set; }
    public decimal ConLai { get; set; }
    public string TrangThai { get; set; } = null!;
    public List<ThanhToanResponseDto> DanhSachThanhToan { get; set; } = new();
}