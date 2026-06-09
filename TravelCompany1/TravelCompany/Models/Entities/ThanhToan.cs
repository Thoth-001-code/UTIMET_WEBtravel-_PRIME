using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("ThanhToan")]
public class ThanhToan
{
    [Key]
    public int MaThanhToan { get; set; }

    public int MaDatTour { get; set; }
    public DateTime NgayThanhToan { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal SoTien { get; set; }

    [MaxLength(50)]
    public string LoaiThanhToan { get; set; } = null!;  // dat_coc, thanh_toan_day_du, thanh_toan_con_lai, hoan_tien

    [MaxLength(50)]
    public string PhuongThuc { get; set; } = null!;      // tien_mat, chuyen_khoan, the, vi_dien_tu

    [MaxLength(50)]
    public string TrangThai { get; set; } = "thanh_cong";

    public string? GhiChu { get; set; }

    // Navigation
    public virtual DatTour DatTour { get; set; } = null!;
}