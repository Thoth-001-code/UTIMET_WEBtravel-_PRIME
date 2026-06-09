using System.ComponentModel.DataAnnotations;

namespace TravelCompany.API.DTOs.Tour;

public class ChiTietLichTrinhCreateDto
{
    public int MaTour { get; set; }
    public int NgayThu { get; set; }

    [MaxLength(150)]
    public string? TieuDe { get; set; }

    public string? HoatDong { get; set; }

    [MaxLength(150)]
    public string? DiaDiem { get; set; }

    [MaxLength(255)]
    public string? ThongTinBuaAn { get; set; }

    [MaxLength(255)]
    public string? ThongTinKhachSan { get; set; }
}