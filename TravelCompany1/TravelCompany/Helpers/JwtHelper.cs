using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TravelCompany.API.Models.Entities;

namespace TravelCompany.API.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(TaiKhoan user, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"] ?? "fallback_key_32_chars_long_123456";
        var key = Encoding.UTF8.GetBytes(jwtKey);
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var expiryMinutes = int.Parse(configuration["Jwt:ExpiryMinutes"] ?? "480");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.MaTaiKhoan.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.HoTen),
            new Claim(ClaimTypes.Role, user.VaiTro),
            new Claim("VaiTro", user.VaiTro)
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GenerateCustomerToken(KhachHang customer, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"] ?? "fallback_key_32_chars_long_123456";
        var key = Encoding.UTF8.GetBytes(jwtKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, customer.MaKhachHang.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, customer.Email ?? ""),
        new Claim(ClaimTypes.Role, "customer"),
        new Claim("CustomerId", customer.MaKhachHang.ToString())
    };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}