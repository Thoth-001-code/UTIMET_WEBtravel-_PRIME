using System;
using System.Collections.Generic;
using QuanLyDuLich.Models.DTOs.Schedule;

namespace QuanLyDuLich.Models.DTOs.Tour
{
    public class TourResponse
    {
        public int MaTour { get; set; }
        public string MaCodeTour { get; set; }
        public string TenTour { get; set; }
        public int? MaDiemDen { get; set; }
        public string TenDiemDen { get; set; }
        public string MoTa { get; set; }
        public int? SoNgay { get; set; }
        public decimal? GiaCoBan { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public string HinhAnh { get; set; }
    }

    public class TourDetailResponse : TourResponse
    {
        public List<ScheduleResponse> LichKhoiHanhs { get; set; } = new List<ScheduleResponse>();
        public List<ChiTietLichTrinhDto> ChiTietLichTrinhs { get; set; } = new List<ChiTietLichTrinhDto>();
    }

    public class ChiTietLichTrinhDto
    {
        public int MaChiTiet { get; set; }
        public int? NgayThu { get; set; }
        public string TieuDe { get; set; }
        public string HoatDong { get; set; }
        public string DiaDiem { get; set; }
        public string ThongTinBuaAn { get; set; }
        public string ThongTinKhachSan { get; set; }
    }
}