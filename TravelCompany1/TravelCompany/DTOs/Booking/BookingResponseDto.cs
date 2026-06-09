namespace TravelCompany.API.DTOs.Booking;

public class BookingResponseDto
{
    public int MaDatTour { get; set; }
    public string MaCodeDat { get; set; } = null!;
    public int MaKhachHang { get; set; }
    public string TenKhachHang { get; set; } = null!;
    public int MaLich { get; set; }
    public string TenTour { get; set; } = null!;
    public DateTime NgayKhoiHanh { get; set; }
    public DateTime NgayDat { get; set; }
    public int SoLuongNguoi { get; set; }
    public decimal TongTien { get; set; }
    public decimal TienDatCoc { get; set; }
    public decimal TienConLai { get; set; }
    public decimal PhiHuy { get; set; }
    public string TrangThai { get; set; } = null!;
    public string? GhiChu { get; set; }
    public List<NguoiDiTourDto> DanhSachNguoiDi { get; set; } = new();
}