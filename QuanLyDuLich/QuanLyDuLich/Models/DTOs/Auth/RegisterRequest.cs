using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.Auth
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string SoDienThoai { get; set; }

        [Required]
        [MinLength(6)]
        public string MatKhau { get; set; }

    }
}