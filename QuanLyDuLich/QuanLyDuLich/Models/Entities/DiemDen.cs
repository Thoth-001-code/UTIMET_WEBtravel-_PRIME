using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDuLich.Models.Entities
{
    [Table("DiemDen")]
    public class DiemDen
    {
        [Key]
        [Column("MaDiemDen")]
        public int MaDiemDen { get; set; }

        [Required]
        [Column("TenDiemDen")]
        [StringLength(200)]
        public string TenDiemDen { get; set; }

        [Column("HinhAnh")]
        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Column("QuocGia")]
        [StringLength(100)]
        public string QuocGia { get; set; }

        [Column("ThanhPho")]
        [StringLength(100)]
        public string ThanhPho { get; set; }

        [Column("MoTa")]
        public string MoTa { get; set; }

        public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}