using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MatKhau { get; set; }
    }
}