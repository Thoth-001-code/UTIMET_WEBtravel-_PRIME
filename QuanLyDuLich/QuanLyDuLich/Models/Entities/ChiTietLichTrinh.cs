using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("ChiTietLichTrinh")]
    public class ChiTietLichTrinh
    {
        [Key]
        [Column("MaChiTiet")]
        public int MaChiTiet { get; set; }

        [Column("MaTour")]
        public int? MaTour { get; set; }

        [Column("NgayThu")]
        public int? NgayThu { get; set; }

        [Column("TieuDe")]
        [StringLength(200)]
        public string TieuDe { get; set; }

        [Column("HoatDong")]
        public string HoatDong { get; set; }

        [Column("DiaDiem")]
        [StringLength(200)]
        public string DiaDiem { get; set; }

        [Column("ThongTinBuaAn")]
        public string ThongTinBuaAn { get; set; }

        [Column("ThongTinKhachSan")]
        public string ThongTinKhachSan { get; set; }

        [ForeignKey(nameof(MaTour))]
        public virtual Tour Tour { get; set; }
    }
}