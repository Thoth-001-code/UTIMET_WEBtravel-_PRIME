using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("DanhGiaTour")]
public class DanhGiaTour
{
    [Key]
    public int MaDanhGia { get; set; }

    public int MaDatTour { get; set; }
    public int MaKhachHang { get; set; }

    public int SoSao { get; set; } // 1-5
    public string? NoiDung { get; set; }
    public DateTime NgayDanhGia { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual DatTour DatTour { get; set; } = null!;
    public virtual KhachHang KhachHang { get; set; } = null!;
}