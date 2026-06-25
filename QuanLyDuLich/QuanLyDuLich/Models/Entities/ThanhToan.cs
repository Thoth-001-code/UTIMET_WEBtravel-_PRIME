using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("ThanhToan")]
    public class ThanhToan
    {
        [Key]
        [Column("MaThanhToan")]
        public int MaThanhToan { get; set; }

        [Column("MaDatTour")]
        public int? MaDatTour { get; set; }

        [Column("NgayThanhToan")]
        public DateTime NgayThanhToan { get; set; }

        [Column("SoTien")]
        public decimal? SoTien { get; set; }

        [Column("LoaiThanhToan")]
        [StringLength(50)]
        public string LoaiThanhToan { get; set; }

        [Column("PhuongThuc")]
        [StringLength(50)]
        public string PhuongThuc { get; set; }

        [Column("TrangThai")]
        [StringLength(20)]
        public string TrangThai { get; set; }

        [Column("GhiChu")]
        public string GhiChu { get; set; }

        [Column("MaHoaDon")]
        public int? MaHoaDon { get; set; }

        [ForeignKey(nameof(MaDatTour))]
        public virtual DatTour DatTour { get; set; }

        [ForeignKey(nameof(MaHoaDon))]
        public virtual HoaDon HoaDon { get; set; }
    }
}