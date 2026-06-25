using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.Schedule
{
    public class ScheduleRequest
    {
        [Required]
        public int MaTour { get; set; }

        [Required]
        public DateTime NgayKhoiHanh { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        public int? SoChoToiDa { get; set; }

        public decimal? GiaTour { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; } = "sap_khoi_hanh";
    }
}