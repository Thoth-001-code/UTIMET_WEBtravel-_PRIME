using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Helpers;
using QuanLyDuLich.Models.DTOs.Auth;
using QuanLyDuLich.Models.Entities;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Services.Servicess
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            // Tìm tài khoản theo email
            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(tk => tk.Email == request.Email);

            if (taiKhoan == null || !PasswordHelper.VerifyPassword(request.MatKhau, taiKhoan.MatKhau))
                throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng.");

            if (taiKhoan.TrangThai == "khoa")
                throw new UnauthorizedAccessException("Tài khoản đã bị khóa.");

            // Lấy MaKhachHang nếu là khách hàng
            int? maKhachHang = null;
            if (taiKhoan.VaiTro == "khach_hang")
            {
                var khachHang = await _context.KhachHangs
                    .FirstOrDefaultAsync(kh => kh.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                maKhachHang = khachHang?.MaKhachHang;
            }

            // Tạo token
            var token = _jwtHelper.GenerateToken(
                taiKhoan.MaTaiKhoan,
                taiKhoan.HoTen,
                taiKhoan.Email,
                taiKhoan.VaiTro,
                maKhachHang,
                taiKhoan.MaNhanVien,
                taiKhoan.LoaiNhanVien
            );

            return new AuthResponse
            {
                Token = token,
                User = new UserInfo
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    HoTen = taiKhoan.HoTen,
                    Email = taiKhoan.Email,
                    VaiTro = taiKhoan.VaiTro,
                    TrangThai = taiKhoan.TrangThai,
                    MaKhachHang = maKhachHang,
                    MaNhanVien = taiKhoan.MaNhanVien,
                    LoaiNhanVien = taiKhoan.LoaiNhanVien
                }
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Kiểm tra email đã tồn tại
            if (await _context.TaiKhoans.AnyAsync(tk => tk.Email == request.Email))
                throw new InvalidOperationException("Email đã được sử dụng.");

            // Tạo tài khoản và khách hàng
            var taiKhoan = new TaiKhoan
            {
                HoTen = request.HoTen,
                Email = request.Email,
                MatKhau = PasswordHelper.HashPassword(request.MatKhau),
                VaiTro = "khach_hang",
                TrangThai = "hoat_dong",
                NgayTao = DateTime.Now
            };

            var khachHang = new KhachHang
            {
                HoTen = request.HoTen,
                SoDienThoai = request.SoDienThoai,
                Email = request.Email,
                NgayTao = DateTime.Now,
                TaiKhoan = taiKhoan,
                DiaChi = "",
                LoaiKhach = "thuong",
                GioiTinh = "",
                NgaySinh = null
            };

            _context.TaiKhoans.Add(taiKhoan);
            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            // Tạo token
            var token = _jwtHelper.GenerateToken(
                taiKhoan.MaTaiKhoan,
                taiKhoan.HoTen,
                taiKhoan.Email,
                taiKhoan.VaiTro,
                khachHang.MaKhachHang
            );

            return new AuthResponse
            {
                Token = token,
                User = new UserInfo
                {
                    MaTaiKhoan = taiKhoan.MaTaiKhoan,
                    HoTen = taiKhoan.HoTen,
                    Email = taiKhoan.Email,
                    VaiTro = taiKhoan.VaiTro,
                    TrangThai = taiKhoan.TrangThai,
                    MaKhachHang = khachHang.MaKhachHang
                }
            };
        }

        public async Task<UserInfo> GetCurrentUserAsync(int maTaiKhoan)
        {
            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(tk => tk.MaTaiKhoan == maTaiKhoan);

            if (taiKhoan == null) return null;

            int? maKhachHang = null;
            if (taiKhoan.VaiTro == "khach_hang")
            {
                var kh = await _context.KhachHangs
                    .FirstOrDefaultAsync(k => k.MaTaiKhoan == maTaiKhoan);
                maKhachHang = kh?.MaKhachHang;
            }

            return new UserInfo
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                HoTen = taiKhoan.HoTen,
                Email = taiKhoan.Email,
                VaiTro = taiKhoan.VaiTro,
                TrangThai = taiKhoan.TrangThai,
                MaKhachHang = maKhachHang,
                MaNhanVien = taiKhoan.MaNhanVien,
                LoaiNhanVien = taiKhoan.LoaiNhanVien
            };
        }
    }
}