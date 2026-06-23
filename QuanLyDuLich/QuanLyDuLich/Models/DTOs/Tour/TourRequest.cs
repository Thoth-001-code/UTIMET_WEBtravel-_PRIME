using System.ComponentModel.DataAnnotations;

namespace QuanLyDuLich.Models.DTOs.Tour
{
    public class TourRequest
    {
        [Required]
        [StringLength(200)]
        public string TenTour { get; set; }

        public int? MaDiemDen { get; set; }

        public string MoTa { get; set; }

        public int? SoNgay { get; set; }

        public decimal? GiaCoBan { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; } = "hien_an";
        public IFormFile? HinhAnhFile { get; set; } // Thêm dòng này

    }
}