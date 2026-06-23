using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Helpers;
using QuanLyDuLich.Models.DTOs.Booking;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.Entities;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly CodeGenerator _codeGenerator;

        public BookingService(ApplicationDbContext context, CodeGenerator codeGenerator)
        {
            _context = context;
            _codeGenerator = codeGenerator;
        }

        public async Task<PagedResult<BookingResponse>> GetBookingsAsync(int? maKhachHang, string role, int pageIndex, int pageSize)
        {
            var query = _context.DatTours
                .Include(d => d.KhachHang)
                .Include(d => d.LichKhoiHanh)
                .AsQueryable();

            if (role == "khach_hang" && maKhachHang.HasValue)
                query = query.Where(d => d.MaKhachHang == maKhachHang.Value);

            var total = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new BookingResponse
                {
                    MaDatTour = d.MaDatTour,
                    MaCodeDat = d.MaCodeDat,
                    MaKhachHang = d.MaKhachHang ?? 0,
                    TenKhachHang = d.KhachHang.HoTen,
                    MaLich = d.MaLich ?? 0,
                    NgayKhoiHanh = d.LichKhoiHanh.NgayKhoiHanh,
                    NgayDat = d.NgayDat,
                    SoLuongNguoi = d.SoLuongNguoi,
                    TongTien = d.TongTien,
                    TienDatCoc = d.TienDatCoc,
                    TienConLai = d.TienConLai,
                    PhiHuy = d.PhiHuy,
                    TrangThai = d.TrangThai,
                    GhiChu = d.GhiChu
                })
                .ToListAsync();

            return new PagedResult<BookingResponse>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Items = items
            };
        }

        public async Task<BookingDetailResponse> GetBookingByIdAsync(int id)
        {
            var booking = await _context.DatTours
                .Include(d => d.KhachHang)
                .Include(d => d.LichKhoiHanh)
                .Include(d => d.NguoiDiTours)
                .Include(d => d.ThanhToans)
                .Include(d => d.HoaDon)
                .Include(d => d.DanhGiaTour)
                .FirstOrDefaultAsync(d => d.MaDatTour == id);

            if (booking == null) return null;

            return new BookingDetailResponse
            {
                MaDatTour = booking.MaDatTour,
                MaCodeDat = booking.MaCodeDat,
                MaKhachHang = booking.MaKhachHang ?? 0,
                TenKhachHang = booking.KhachHang?.HoTen,
                MaLich = booking.MaLich ?? 0,
                NgayKhoiHanh = booking.LichKhoiHanh?.NgayKhoiHanh ?? DateTime.MinValue,
                NgayDat = booking.NgayDat,
                SoLuongNguoi = booking.SoLuongNguoi,
                TongTien = booking.TongTien,
                TienDatCoc = booking.TienDatCoc,
                TienConLai = booking.TienConLai,
                PhiHuy = booking.PhiHuy,
                TrangThai = booking.TrangThai,
                GhiChu = booking.GhiChu,
                DanhSachNguoiDi = booking.NguoiDiTours.Select(n => new NguoiDiTourDto
                {
                    MaNguoiDi = n.MaNguoiDi,
                    HoTen = n.HoTen,
                    GioiTinh = n.GioiTinh,
                    NgaySinh = n.NgaySinh,
                    SoCCCD = n.SoCCCD,
                    SoDienThoai = n.SoDienThoai
                }).ToList(),
                ThanhToans = booking.ThanhToans.Select(t => new ThanhToanDto
                {
                    MaThanhToan = t.MaThanhToan,
                    NgayThanhToan = t.NgayThanhToan,
                    SoTien = t.SoTien,
                    LoaiThanhToan = t.LoaiThanhToan,
                    PhuongThuc = t.PhuongThuc,
                    TrangThai = t.TrangThai
                }).ToList(),
                HoaDon = booking.HoaDon == null ? null : new HoaDonDto
                {
                    MaHoaDon = booking.HoaDon.MaHoaDon,
                    MaCodeHoaDon = booking.HoaDon.MaCodeHoaDon,
                    NgayXuat = booking.HoaDon.NgayXuat,
                    TongTien = booking.HoaDon.TongTien,
                    DaThanhToan = booking.HoaDon.DaThanhToan,
                    ConLai = booking.HoaDon.ConLai,
                    TrangThai = booking.HoaDon.TrangThai
                },
                DanhGia = booking.DanhGiaTour == null ? null : new DanhGiaDto
                {
                    MaDanhGia = booking.DanhGiaTour.MaDanhGia,
                    SoSao = booking.DanhGiaTour.SoSao,
                    NoiDung = booking.DanhGiaTour.NoiDung,
                    NgayDanhGia = booking.DanhGiaTour.NgayDanhGia
                }
            };
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingRequest request, int maTaiKhoan)
        {
            var schedule = await _context.LichKhoiHanhs.FindAsync(request.MaLich);
            if (schedule == null) throw new Exception("Lịch khởi hành không tồn tại.");
            if (schedule.SoChoConLai < request.SoLuongNguoi)
                throw new InvalidOperationException("Không đủ chỗ trống.");

            var khachHang = await _context.KhachHangs.FindAsync(request.MaKhachHang);
            if (khachHang == null) throw new Exception("Khách hàng không tồn tại.");

            decimal giaTour = schedule.GiaTour ?? 0;
            decimal tongTien = giaTour * request.SoLuongNguoi;
            decimal tienCoc = tongTien * 0.3m;
            decimal tienConLai = tongTien - tienCoc;

            string maCode = _codeGenerator.GenerateBookingCode();

            var booking = new DatTour
            {
                MaCodeDat = maCode,
                MaKhachHang = request.MaKhachHang,
                MaLich = request.MaLich,
                MaTaiKhoan = maTaiKhoan,
                NgayDat = DateTime.Now,
                SoLuongNguoi = request.SoLuongNguoi,
                TongTien = tongTien,
                TienDatCoc = tienCoc,
                TienConLai = tienConLai,
                TrangThai = "cho_xac_nhan",
                GhiChu = request.GhiChu
            };

            schedule.SoChoConLai -= request.SoLuongNguoi;

            _context.DatTours.Add(booking);
            await _context.SaveChangesAsync();

            foreach (var nguoi in request.DanhSachNguoiDi)
            {
                _context.NguoiDiTours.Add(new NguoiDiTour
                {
                    MaDatTour = booking.MaDatTour,
                    HoTen = nguoi.HoTen,
                    GioiTinh = nguoi.GioiTinh,
                    NgaySinh = nguoi.NgaySinh,
                    SoCCCD = nguoi.SoCCCD,
                    SoDienThoai = nguoi.SoDienThoai
                });
            }

            _context.LichSuDatTours.Add(new LichSuDatTour
            {
                MaDatTour = booking.MaDatTour,
                MaTaiKhoan = maTaiKhoan,
                HanhDong = "tao",
                TrangThaiMoi = "cho_xac_nhan",
                NgayTao = DateTime.Now,
                GhiChu = "Tạo đặt tour mới"
            });

            await _context.SaveChangesAsync();

            var hoaDon = new HoaDon
            {
                MaCodeHoaDon = _codeGenerator.GenerateInvoiceCode(),
                MaDatTour = booking.MaDatTour,
                NgayXuat = DateTime.Now,
                TongTien = tongTien,
                DaThanhToan = 0,
                ConLai = tongTien,
                TrangThai = "chua_thanh_toan"
            };
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            return new BookingResponse
            {
                MaDatTour = booking.MaDatTour,
                MaCodeDat = booking.MaCodeDat,
                MaKhachHang = booking.MaKhachHang ?? 0,
                TenKhachHang = khachHang.HoTen,
                MaLich = booking.MaLich ?? 0,
                NgayDat = booking.NgayDat,
                SoLuongNguoi = booking.SoLuongNguoi,
                TongTien = booking.TongTien,
                TienDatCoc = booking.TienDatCoc,
                TienConLai = booking.TienConLai,
                PhiHuy = booking.PhiHuy,
                TrangThai = booking.TrangThai,
                GhiChu = booking.GhiChu
            };
        }

        public async Task<BookingResponse> ConfirmBookingAsync(int id)
        {
            var booking = await _context.DatTours
                .Include(d => d.KhachHang)
                .Include(d => d.LichKhoiHanh)
                .FirstOrDefaultAsync(d => d.MaDatTour == id);

            if (booking == null) throw new Exception("Không tìm thấy đặt tour.");
            if (booking.TrangThai != "cho_xac_nhan")
                throw new InvalidOperationException("Chỉ có thể xác nhận đặt tour đang chờ.");

            booking.TrangThai = "da_xac_nhan";
            await _context.SaveChangesAsync();

            return new BookingResponse
            {
                MaDatTour = booking.MaDatTour,
                MaCodeDat = booking.MaCodeDat,
                MaKhachHang = booking.MaKhachHang ?? 0,
                TenKhachHang = booking.KhachHang?.HoTen,
                MaLich = booking.MaLich ?? 0,
                NgayDat = booking.NgayDat,
                SoLuongNguoi = booking.SoLuongNguoi,
                TongTien = booking.TongTien,
                TienDatCoc = booking.TienDatCoc,
                TienConLai = booking.TienConLai,
                PhiHuy = booking.PhiHuy,
                TrangThai = booking.TrangThai,
                GhiChu = booking.GhiChu
            };
        }

        public async Task<BookingResponse> CancelBookingAsync(int id)
        {
            var booking = await _context.DatTours
                .Include(d => d.KhachHang)
                .Include(d => d.LichKhoiHanh)
                .FirstOrDefaultAsync(d => d.MaDatTour == id);

            if (booking == null) throw new Exception("Không tìm thấy đặt tour.");
            if (booking.TrangThai == "da_huy")
                throw new InvalidOperationException("Đặt tour đã bị hủy trước đó.");

            decimal phiHuy = (booking.TongTien ?? 0) * 0.1m;
            booking.PhiHuy = phiHuy;
            booking.TrangThai = "da_huy";

            var schedule = await _context.LichKhoiHanhs.FindAsync(booking.MaLich);
            if (schedule != null)
                schedule.SoChoConLai += booking.SoLuongNguoi;

            await _context.SaveChangesAsync();

            return new BookingResponse
            {
                MaDatTour = booking.MaDatTour,
                MaCodeDat = booking.MaCodeDat,
                MaKhachHang = booking.MaKhachHang ?? 0,
                TenKhachHang = booking.KhachHang?.HoTen,
                MaLich = booking.MaLich ?? 0,
                NgayDat = booking.NgayDat,
                SoLuongNguoi = booking.SoLuongNguoi,
                TongTien = booking.TongTien,
                TienDatCoc = booking.TienDatCoc,
                TienConLai = booking.TienConLai,
                PhiHuy = booking.PhiHuy,
                TrangThai = booking.TrangThai,
                GhiChu = booking.GhiChu
            };
        }

        public async Task<BookingResponse> UpdateBookingStatusAsync(int id, string newStatus)
        {
            var booking = await _context.DatTours
                .Include(d => d.KhachHang)
                .FirstOrDefaultAsync(d => d.MaDatTour == id);

            if (booking == null) throw new Exception("Không tìm thấy đặt tour.");

            var validStatus = new[] { "cho_xac_nhan", "da_xac_nhan", "da_huy", "hoan_thanh" };
            if (!validStatus.Contains(newStatus))
                throw new InvalidOperationException("Trạng thái không hợp lệ.");

            booking.TrangThai = newStatus;
            await _context.SaveChangesAsync();

            return new BookingResponse
            {
                MaDatTour = booking.MaDatTour,
                MaCodeDat = booking.MaCodeDat,
                MaKhachHang = booking.MaKhachHang ?? 0,
                TenKhachHang = booking.KhachHang?.HoTen,
                MaLich = booking.MaLich ?? 0,
                NgayDat = booking.NgayDat,
                SoLuongNguoi = booking.SoLuongNguoi,
                TongTien = booking.TongTien,
                TienDatCoc = booking.TienDatCoc,
                TienConLai = booking.TienConLai,
                PhiHuy = booking.PhiHuy,
                TrangThai = booking.TrangThai,
                GhiChu = booking.GhiChu
            };
        }
    }
}