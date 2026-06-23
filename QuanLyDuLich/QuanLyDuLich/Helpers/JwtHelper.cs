using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace QuanLyDuLich.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(int maTaiKhoan, string hoTen, string email, string vaiTro,
                                    int? maKhachHang = null, string maNhanVien = null, string loaiNhanVien = null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, maTaiKhoan.ToString()),
                new Claim(ClaimTypes.Name, hoTen),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, vaiTro),
                new Claim("MaTaiKhoan", maTaiKhoan.ToString()),
                new Claim("HoTen", hoTen),
                new Claim("Email", email),
                new Claim("VaiTro", vaiTro),
                new Claim("MaKhachHang", maKhachHang?.ToString() ?? ""),
                new Claim("MaNhanVien", maNhanVien ?? ""),
                new Claim("LoaiNhanVien", loaiNhanVien ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}