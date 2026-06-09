namespace TravelCompany.API.DTOs.Tour;

public class ChiTietLichTrinhUpdateDto
{
    public int MaChiTiet { get; set; }
    public int MaTour { get; set; }
    public int? NgayThu { get; set; }
    public string? TieuDe { get; set; }
    public string? HoatDong { get; set; }
    public string? DiaDiem { get; set; }
    public string? ThongTinBuaAn { get; set; }
    public string? ThongTinKhachSan { get; set; }
}