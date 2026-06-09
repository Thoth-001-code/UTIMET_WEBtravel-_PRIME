using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("ChiTietLichTrinh")]
public class ChiTietLichTrinh
{
    [Key]
    public int MaChiTiet { get; set; }

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

    // Navigation
    public virtual Tour Tour { get; set; } = null!;
}