using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [Column("MaTaiKhoan")]
        public int MaTaiKhoan { get; set; }

        [Required]
        [Column("HoTen")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [Column("Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Column("MatKhau")]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [Column("VaiTro")]
        [StringLength(20)]
        public string VaiTro { get; set; }

        [Required]
        [Column("TrangThai")]
        [StringLength(20)]
        public string TrangThai { get; set; }

        [Column("MaNhanVien")]
        [StringLength(20)]
        public string? MaNhanVien { get; set; } // nullable

        [Column("LoaiNhanVien")]
        [StringLength(50)]
        public string? LoaiNhanVien { get; set; } // nullable

        [Column("NgayTao")]
        public DateTime NgayTao { get; set; }

        // Navigation properties
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
        public virtual ICollection<LichSuDatTour> LichSuDatTours { get; set; } = new List<LichSuDatTour>();
    }
}