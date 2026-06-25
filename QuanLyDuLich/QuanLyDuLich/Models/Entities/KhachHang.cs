using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [Column("MaKhachHang")]
        public int MaKhachHang { get; set; }

        [Required]
        [Column("HoTen")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Column("SoDienThoai")]
        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [Column("Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("GioiTinh")]
        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        [Column("DiaChi")]
        [StringLength(200)]
        public string? DiaChi { get; set; }

        [Column("LoaiKhach")]
        [StringLength(20)]
        public string? LoaiKhach { get; set; }

        [Column("MaTaiKhoan")]
        public int? MaTaiKhoan { get; set; }

        [Column("NgayTao")]
        public DateTime NgayTao { get; set; }

        [ForeignKey(nameof(MaTaiKhoan))]
        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
        public virtual ICollection<DanhGiaTour> DanhGiaTours { get; set; } = new List<DanhGiaTour>();
    }
}