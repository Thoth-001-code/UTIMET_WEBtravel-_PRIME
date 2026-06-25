using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Helpers;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.DTOs.Staff;
using QuanLyDuLich.Models.Entities;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Services
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _context;

        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<StaffResponse>> GetStaffsAsync(string roleFilter, int pageIndex, int pageSize)
        {
            var query = _context.TaiKhoans
                .Where(tk => tk.VaiTro != "khach_hang");

            if (!string.IsNullOrEmpty(roleFilter))
                query = query.Where(tk => tk.VaiTro == roleFilter);

            var total = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(tk => new StaffResponse
                {
                    MaTaiKhoan = tk.MaTaiKhoan,
                    HoTen = tk.HoTen,
                    Email = tk.Email,
                    VaiTro = tk.VaiTro,
                    TrangThai = tk.TrangThai,
                    MaNhanVien = tk.MaNhanVien,
                    LoaiNhanVien = tk.LoaiNhanVien,
                    NgayTao = tk.NgayTao
                })
                .ToListAsync();

            return new PagedResult<StaffResponse>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Items = items
            };
        }

        public async Task<StaffResponse> CreateStaffAsync(StaffRequest request)
        {
            if (await _context.TaiKhoans.AnyAsync(tk => tk.Email == request.Email))
                throw new InvalidOperationException("Email đã tồn tại.");

            string vaiTro = (request.LoaiNhanVien == "quan_ly") ? "quan_ly" : "nhan_vien";

            var taiKhoan = new TaiKhoan
            {
                HoTen = request.HoTen,
                Email = request.Email,
                MatKhau = PasswordHelper.HashPassword(request.MatKhau),
                VaiTro = vaiTro,
                TrangThai = "hoat_dong",
                MaNhanVien = request.MaNhanVien,
                LoaiNhanVien = request.LoaiNhanVien,
                NgayTao = DateTime.Now
            };

            _context.TaiKhoans.Add(taiKhoan);
            await _context.SaveChangesAsync();

            return new StaffResponse
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                HoTen = taiKhoan.HoTen,
                Email = taiKhoan.Email,
                VaiTro = taiKhoan.VaiTro,
                TrangThai = taiKhoan.TrangThai,
                MaNhanVien = taiKhoan.MaNhanVien,
                LoaiNhanVien = taiKhoan.LoaiNhanVien,
                NgayTao = taiKhoan.NgayTao
            };
        }

        public async Task<StaffResponse> ToggleLockStaffAsync(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null) throw new Exception("Không tìm thấy tài khoản.");
            if (taiKhoan.VaiTro == "khach_hang")
                throw new InvalidOperationException("Chỉ khóa/mở khóa tài khoản nhân viên.");

            taiKhoan.TrangThai = taiKhoan.TrangThai == "hoat_dong" ? "khoa" : "hoat_dong";
            await _context.SaveChangesAsync();

            return new StaffResponse
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                HoTen = taiKhoan.HoTen,
                Email = taiKhoan.Email,
                VaiTro = taiKhoan.VaiTro,
                TrangThai = taiKhoan.TrangThai,
                MaNhanVien = taiKhoan.MaNhanVien,
                LoaiNhanVien = taiKhoan.LoaiNhanVien,
                NgayTao = taiKhoan.NgayTao
            };
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null) return false;
            if (taiKhoan.VaiTro == "khach_hang")
                throw new InvalidOperationException("Không thể xóa tài khoản khách hàng qua đây.");
            if (taiKhoan.VaiTro == "quan_tri")
                throw new InvalidOperationException("Không thể xóa tài khoản Admin.");

            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}