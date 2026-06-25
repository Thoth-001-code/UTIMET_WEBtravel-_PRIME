using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.Staff
{
    public class StaffRequest
    {
        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiNhanVien { get; set; } // quan_ly, nhan_vien
    }
}