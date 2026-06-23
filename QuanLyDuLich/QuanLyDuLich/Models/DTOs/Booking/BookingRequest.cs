using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.Booking
{
    public class BookingRequest
    {
        [Required]
        public int MaLich { get; set; }

        [Required]
        public int MaKhachHang { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SoLuongNguoi { get; set; }

        public string GhiChu { get; set; }

        public List<NguoiDiTourRequest> DanhSachNguoiDi { get; set; } = new List<NguoiDiTourRequest>();
    }

    public class NguoiDiTourRequest
    {
        [Required]
        public string HoTen { get; set; }

        public string GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string SoCCCD { get; set; }

        public string SoDienThoai { get; set; }
    }
}