using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("DatTour")]
    public class DatTour
    {
        [Key]
        [Column("MaDatTour")]
        public int MaDatTour { get; set; }

        [Required]
        [Column("MaCodeDat")]
        [StringLength(20)]
        public string MaCodeDat { get; set; }

        [Column("MaKhachHang")]
        public int? MaKhachHang { get; set; }

        [Column("MaLich")]
        public int? MaLich { get; set; }

        [Column("MaTaiKhoan")]
        public int? MaTaiKhoan { get; set; }

        [Column("NgayDat")]
        public DateTime NgayDat { get; set; }

        [Column("SoLuongNguoi")]
        public int? SoLuongNguoi { get; set; }

        [Column("TongTien")]
        public decimal? TongTien { get; set; }

        [Column("TienDatCoc")]
        public decimal? TienDatCoc { get; set; }

        [Column("TienConLai")]
        public decimal? TienConLai { get; set; }

        [Column("PhiHuy")]
        public decimal? PhiHuy { get; set; }

        [Column("TrangThai")]
        [StringLength(20)]
        public string TrangThai { get; set; }

        [Column("GhiChu")]
        public string GhiChu { get; set; }

        [ForeignKey(nameof(MaKhachHang))]
        public virtual KhachHang KhachHang { get; set; }

        [ForeignKey(nameof(MaLich))]
        public virtual LichKhoiHanh LichKhoiHanh { get; set; }

        [ForeignKey(nameof(MaTaiKhoan))]
        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ICollection<NguoiDiTour> NguoiDiTours { get; set; } = new List<NguoiDiTour>();
        public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
        public virtual HoaDon HoaDon { get; set; }
        public virtual ICollection<LichSuDatTour> LichSuDatTours { get; set; } = new List<LichSuDatTour>();
        public virtual DanhGiaTour DanhGiaTour { get; set; }
    }
}