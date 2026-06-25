using System;

namespace QuanLyDuLich.Models.DTOs.Staff
{
    public class StaffResponse
    {
        public int MaTaiKhoan { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
        public string MaNhanVien { get; set; }
        public string LoaiNhanVien { get; set; }
        public DateTime NgayTao { get; set; }
    }
}