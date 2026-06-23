using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("Tour")]
    public class Tour
    {
        [Key]
        [Column("MaTour")]
        public int MaTour { get; set; }

        [Required]
        [Column("MaCodeTour")]
        [StringLength(20)]
        public string MaCodeTour { get; set; }

        [Required]
        [Column("TenTour")]
        [StringLength(200)]
        public string TenTour { get; set; }

        [Column("HinhAnh")]
        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Column("MaDiemDen")]
        public int? MaDiemDen { get; set; }

        [Column("MoTa")]
        public string MoTa { get; set; }

        [Column("SoNgay")]
        public int? SoNgay { get; set; }

        [Column("GiaCoBan")]
        public decimal? GiaCoBan { get; set; }

        [Column("TrangThai")]
        [StringLength(20)]
        public string TrangThai { get; set; }

        [Column("NgayTao")]
        public DateTime NgayTao { get; set; }

        [ForeignKey(nameof(MaDiemDen))]
        public virtual DiemDen DiemDen { get; set; }

        public virtual ICollection<LichKhoiHanh> LichKhoiHanhs { get; set; } = new List<LichKhoiHanh>();
        public virtual ICollection<ChiTietLichTrinh> ChiTietLichTrinhs { get; set; } = new List<ChiTietLichTrinh>();
    }
}