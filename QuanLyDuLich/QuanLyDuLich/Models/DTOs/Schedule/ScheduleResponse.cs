using System;

namespace QuanLyDuLich.Models.DTOs.Schedule
{
    public class ScheduleResponse
    {
        public int MaLich { get; set; }
        public int MaTour { get; set; }
        public string TenTour { get; set; }
        public DateTime NgayKhoiHanh { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int? SoChoToiDa { get; set; }
        public int? SoChoConLai { get; set; }
        public decimal? GiaTour { get; set; }
        public string TrangThai { get; set; }
    }
}