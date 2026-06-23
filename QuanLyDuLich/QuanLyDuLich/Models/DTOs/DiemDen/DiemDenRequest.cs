using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.DiemDen
{
    public class DiemDenRequest
    {
        [Required]
        [StringLength(200)]
        public string TenDiemDen { get; set; }

        [StringLength(100)]
        public string QuocGia { get; set; }

        [StringLength(100)]
        public string ThanhPho { get; set; }

        public string MoTa { get; set; }
        public IFormFile? HinhAnhFile { get; set; }
    }
}