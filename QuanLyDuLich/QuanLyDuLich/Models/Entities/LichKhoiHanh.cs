using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("LichKhoiHanh")]
    public class LichKhoiHanh
    {
        [Key]
        [Column("MaLich")]
        public int MaLich { get; set; }

        [Column("MaTour")]
        public int? MaTour { get; set; }

        [Required]
        [Column("NgayKhoiHanh")]
        public DateTime NgayKhoiHanh { get; set; }

        [Required]
        [Column("NgayKetThuc")]
        public DateTime NgayKetThuc { get; set; }

        [Column("SoChoToiDa")]
        public int? SoChoToiDa { get; set; }

        [Column("SoChoConLai")]
        public int? SoChoConLai { get; set; }

        [Column("GiaTour")]
        public decimal? GiaTour { get; set; }

        [Column("TrangThai")]
        [StringLength(20)]
        public string TrangThai { get; set; }

        [ForeignKey(nameof(MaTour))]
        public virtual Tour Tour { get; set; }

        public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
    }
}