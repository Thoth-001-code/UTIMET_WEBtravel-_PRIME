using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.Customer
{
    public class CustomerRequest
    {
        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Phone]
        public string SoDienThoai { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(20)]
        public string LoaiKhach { get; set; }
    }
}