using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("DatTour")]
public class DatTour
{
    [Key]
    public int MaDatTour { get; set; }

    [Required, MaxLength(50)]
    public string MaCodeDat { get; set; } = null!;

    public int MaKhachHang { get; set; }
    public int MaLich { get; set; }
    public int? MaTaiKhoan { get; set; }   // nhân viên xử lý

    public DateTime NgayDat { get; set; } = DateTime.UtcNow;
    public int SoLuongNguoi { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TongTien { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TienDatCoc { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TienConLai { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PhiHuy { get; set; }

    [MaxLength(50)]
    public string TrangThai { get; set; } = "cho_xac_nhan";

    public string? GhiChu { get; set; }

    // Navigation
    public virtual KhachHang KhachHang { get; set; } = null!;
    public virtual LichKhoiHanh LichKhoiHanh { get; set; } = null!;
    public virtual TaiKhoan? TaiKhoan { get; set; }
    public virtual ICollection<NguoiDiTour> NguoiDiTours { get; set; } = new List<NguoiDiTour>();
    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
    public virtual HoaDon? HoaDon { get; set; }
    public virtual ICollection<LichSuDatTour> LichSuDatTours { get; set; } = new List<LichSuDatTour>();
    public virtual ICollection<DanhGiaTour> DanhGiaTours { get; set; } = new List<DanhGiaTour>();
}