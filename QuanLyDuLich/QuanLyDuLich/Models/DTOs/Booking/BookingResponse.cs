using System;
using System.Collections.Generic;

namespace QuanLyDuLich.Models.DTOs.Booking
{
    public class BookingResponse
    {
        public int MaDatTour { get; set; }
        public string MaCodeDat { get; set; }
        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public int MaLich { get; set; }
        public DateTime NgayKhoiHanh { get; set; }
        public DateTime NgayDat { get; set; }
        public int? SoLuongNguoi { get; set; }
        public decimal? TongTien { get; set; }
        public decimal? TienDatCoc { get; set; }
        public decimal? TienConLai { get; set; }
        public decimal? PhiHuy { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }

    public class BookingDetailResponse : BookingResponse
    {
        public List<NguoiDiTourDto> DanhSachNguoiDi { get; set; } = new List<NguoiDiTourDto>();
        public List<ThanhToanDto> ThanhToans { get; set; } = new List<ThanhToanDto>();
        public HoaDonDto HoaDon { get; set; }
        public DanhGiaDto DanhGia { get; set; }
    }

    public class NguoiDiTourDto
    {
        public int MaNguoiDi { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string SoCCCD { get; set; }
        public string SoDienThoai { get; set; }
    }

    public class ThanhToanDto
    {
        public int MaThanhToan { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public decimal? SoTien { get; set; }
        public string LoaiThanhToan { get; set; }
        public string PhuongThuc { get; set; }
        public string TrangThai { get; set; }
    }

    public class HoaDonDto
    {
        public int MaHoaDon { get; set; }
        public string MaCodeHoaDon { get; set; }
        public DateTime NgayXuat { get; set; }
        public decimal? TongTien { get; set; }
        public decimal? DaThanhToan { get; set; }
        public decimal? ConLai { get; set; }
        public string TrangThai { get; set; }
    }

    public class DanhGiaDto
    {
        public int MaDanhGia { get; set; }
        public int? SoSao { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDanhGia { get; set; }
    }
}