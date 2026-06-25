using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [Column("MaHoaDon")]
        public int MaHoaDon { get; set; }

        [Required]
        [Column("MaCodeHoaDon")]
        [StringLength(20)]
        public string MaCodeHoaDon { get; set; }

        [Column("MaDatTour")]
        public int? MaDatTour { get; set; }

        [Column("NgayXuat")]
        public DateTime NgayXuat { get; set; }

        [Column("TongTien")]
        public decimal? TongTien { get; set; }

        [Column("DaThanhToan")]
        public decimal? DaThanhToan { get; set; }

        [Column("ConLai")]
        public decimal? ConLai { get; set; }

        [Column("TrangThai")]
        [StringLength(20)]
        public string TrangThai { get; set; }

        [ForeignKey(nameof(MaDatTour))]
        public virtual DatTour DatTour { get; set; }

        public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
    }
}