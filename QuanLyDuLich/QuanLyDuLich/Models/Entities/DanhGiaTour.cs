using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("DanhGiaTour")]
    public class DanhGiaTour
    {
        [Key]
        [Column("MaDanhGia")]
        public int MaDanhGia { get; set; }

        [Column("MaDatTour")]
        public int? MaDatTour { get; set; }

        [Column("MaKhachHang")]
        public int? MaKhachHang { get; set; }

        [Column("SoSao")]
        public int? SoSao { get; set; }

        [Column("NoiDung")]
        public string NoiDung { get; set; }

        [Column("NgayDanhGia")]
        public DateTime NgayDanhGia { get; set; }

        [ForeignKey(nameof(MaDatTour))]
        public virtual DatTour DatTour { get; set; }

        [ForeignKey(nameof(MaKhachHang))]
        public virtual KhachHang KhachHang { get; set; }
    }
}