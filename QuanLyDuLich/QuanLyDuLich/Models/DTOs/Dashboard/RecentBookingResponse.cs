using System;

namespace QuanLyDuLich.Models.DTOs.Dashboard
{
    public class RecentBookingResponse
    {
        public int MaDatTour { get; set; }
        public string MaCodeDat { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal? TongTien { get; set; }
        public string TrangThai { get; set; }
    }
}   