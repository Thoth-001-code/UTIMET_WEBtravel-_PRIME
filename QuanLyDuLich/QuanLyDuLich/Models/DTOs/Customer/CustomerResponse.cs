using System;
using System.Collections.Generic;
using QuanLyDuLich.Models.DTOs.Booking;

namespace QuanLyDuLich.Models.DTOs.Customer
{
    public class CustomerResponse
    {
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string LoaiKhach { get; set; }
        public int? MaTaiKhoan { get; set; }
        public DateTime NgayTao { get; set; }
    }

    public class CustomerDetailResponse : CustomerResponse
    {
        public List<BookingResponse> LichSuDatTour { get; set; } = new List<BookingResponse>();
    }
}