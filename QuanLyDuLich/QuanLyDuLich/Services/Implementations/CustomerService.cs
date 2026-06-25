using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Models.DTOs.Booking;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.DTOs.Customer;
using QuanLyDuLich.Models.Entities;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CustomerResponse>> GetCustomersAsync(string search, int pageIndex, int pageSize)
        {
            var query = _context.KhachHangs.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(kh => kh.HoTen.Contains(search) || kh.Email.Contains(search));

            var total = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(kh => new CustomerResponse
                {
                    MaKhachHang = kh.MaKhachHang,
                    HoTen = kh.HoTen,
                    SoDienThoai = kh.SoDienThoai,
                    Email = kh.Email,
                    GioiTinh = kh.GioiTinh,
                    NgaySinh = kh.NgaySinh,
                    DiaChi = kh.DiaChi,
                    LoaiKhach = kh.LoaiKhach,
                    MaTaiKhoan = kh.MaTaiKhoan,
                    NgayTao = kh.NgayTao
                })
                .ToListAsync();

            return new PagedResult<CustomerResponse>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Items = items
            };
        }

        public async Task<CustomerDetailResponse> GetCustomerByIdAsync(int id)
        {
            var khachHang = await _context.KhachHangs
                .Include(kh => kh.DatTours)
                    .ThenInclude(dt => dt.LichKhoiHanh)
                .FirstOrDefaultAsync(kh => kh.MaKhachHang == id);

            if (khachHang == null) return null;

            return new CustomerDetailResponse
            {
                MaKhachHang = khachHang.MaKhachHang,
                HoTen = khachHang.HoTen,
                SoDienThoai = khachHang.SoDienThoai,
                Email = khachHang.Email,
                GioiTinh = khachHang.GioiTinh,
                NgaySinh = khachHang.NgaySinh,
                DiaChi = khachHang.DiaChi,
                LoaiKhach = khachHang.LoaiKhach,
                MaTaiKhoan = khachHang.MaTaiKhoan,
                NgayTao = khachHang.NgayTao,
                LichSuDatTour = khachHang.DatTours.Select(d => new BookingResponse
                {
                    MaDatTour = d.MaDatTour,
                    MaCodeDat = d.MaCodeDat,
                    MaKhachHang = d.MaKhachHang ?? 0,
                    TenKhachHang = khachHang.HoTen,
                    MaLich = d.MaLich ?? 0,
                    NgayKhoiHanh = d.LichKhoiHanh != null ? d.LichKhoiHanh.NgayKhoiHanh : DateTime.MinValue,
                    NgayDat = d.NgayDat,
                    SoLuongNguoi = d.SoLuongNguoi,
                    TongTien = d.TongTien,
                    TienDatCoc = d.TienDatCoc,
                    TienConLai = d.TienConLai,
                    PhiHuy = d.PhiHuy,
                    TrangThai = d.TrangThai,
                    GhiChu = d.GhiChu
                }).ToList()
            };
        }

        public async Task<CustomerResponse> UpdateCustomerAsync(int id, CustomerRequest request)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null) throw new Exception("Khách hàng không tồn tại.");

            khachHang.HoTen = request.HoTen;
            khachHang.SoDienThoai = request.SoDienThoai;
            khachHang.Email = request.Email;
            khachHang.GioiTinh = request.GioiTinh;
            khachHang.NgaySinh = request.NgaySinh;
            khachHang.DiaChi = request.DiaChi;
            khachHang.LoaiKhach = request.LoaiKhach;

            await _context.SaveChangesAsync();

            return new CustomerResponse
            {
                MaKhachHang = khachHang.MaKhachHang,
                HoTen = khachHang.HoTen,
                SoDienThoai = khachHang.SoDienThoai,
                Email = khachHang.Email,
                GioiTinh = khachHang.GioiTinh,
                NgaySinh = khachHang.NgaySinh,
                DiaChi = khachHang.DiaChi,
                LoaiKhach = khachHang.LoaiKhach,
                MaTaiKhoan = khachHang.MaTaiKhoan,
                NgayTao = khachHang.NgayTao
            };
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var khachHang = await _context.KhachHangs
                .Include(kh => kh.DatTours)
                .FirstOrDefaultAsync(kh => kh.MaKhachHang == id);

            if (khachHang == null) return false;

            if (khachHang.DatTours.Any(d => d.TrangThai != "da_huy"))
                throw new InvalidOperationException("Không thể xóa khách hàng vì có đặt tour đang hoạt động.");

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}