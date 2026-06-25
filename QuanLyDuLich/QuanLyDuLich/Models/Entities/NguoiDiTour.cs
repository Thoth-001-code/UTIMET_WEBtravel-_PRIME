using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("NguoiDiTour")]
    public class NguoiDiTour
    {
        [Key]
        [Column("MaNguoiDi")]
        public int MaNguoiDi { get; set; }

        [Column("MaDatTour")]
        public int? MaDatTour { get; set; }

        [Required]
        [Column("HoTen")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Column("GioiTinh")]
        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        [Column("SoCCCD")]
        [StringLength(20)]
        public string SoCCCD { get; set; }

        [Column("SoDienThoai")]
        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [ForeignKey(nameof(MaDatTour))]
        public virtual DatTour DatTour { get; set; }
    }
}