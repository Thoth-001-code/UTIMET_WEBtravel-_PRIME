using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("LichSuDatTour")]
    public class LichSuDatTour
    {
        [Key]
        [Column("MaLichSu")]
        public int MaLichSu { get; set; }

        [Column("MaDatTour")]
        public int? MaDatTour { get; set; }

        [Column("MaTaiKhoan")]
        public int? MaTaiKhoan { get; set; }

        [Column("HanhDong")]
        [StringLength(50)]
        public string HanhDong { get; set; }

        [Column("TrangThaiCu")]
        [StringLength(20)]
        public string TrangThaiCu { get; set; }

        [Column("TrangThaiMoi")]
        [StringLength(20)]
        public string TrangThaiMoi { get; set; }

        [Column("GhiChu")]
        public string GhiChu { get; set; }

        [Column("NgayTao")]
        public DateTime NgayTao { get; set; }

        [ForeignKey(nameof(MaDatTour))]
        public virtual DatTour DatTour { get; set; }

        [ForeignKey(nameof(MaTaiKhoan))]
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}