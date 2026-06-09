using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.API.Models.Entities;

[Table("KhachHang")]
public class KhachHang
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaKhachHang { get; set; }

    [Required]
    [MaxLength(100)]
    public string HoTen { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string SoDienThoai { get; set; } = null!;

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(255)]
    public string? MatKhau { get; set; }

    [MaxLength(10)]
    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }
    public string? DiaChi { get; set; }

    [MaxLength(50)]
    public string LoaiKhach { get; set; } = "moi";

    public DateTime NgayTao { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
    public virtual ICollection<DanhGiaTour> DanhGiaTours { get; set; } = new List<DanhGiaTour>();
}