using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("TaiKhoan")]
public class TaiKhoan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaTaiKhoan { get; set; }

    [Required]
    [MaxLength(100)]
    public string HoTen { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [MaxLength(20)]
    public string? SoDienThoai { get; set; }

    [Required]
    public string MatKhau { get; set; } = null!;

    [MaxLength(50)]
    public string VaiTro { get; set; } = "nhan_vien";

    [MaxLength(50)]
    public string TrangThai { get; set; } = "hoat_dong";

    public DateTime NgayTao { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
    public virtual ICollection<LichSuDatTour> LichSuDatTours { get; set; } = new List<LichSuDatTour>();
}