using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Auth;
using TravelCompany.API.Helpers;
using TravelCompany.API.Models.Entities;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.TaiKhoans
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.TrangThai == "hoat_dong");
        if (user == null) return null;

        // Sử dụng đúng tên lớp BCrypt.Net.BCrypt
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.MatKhau);
        if (!isPasswordValid) return null;

        var token = JwtHelper.GenerateToken(user, _configuration);
        var userDto = _mapper.Map<UserResponseDto>(user);

        return new LoginResponseDto
        {
            Token = token,
            User = userDto
        };
    }

    public async Task<UserResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Kiểm tra email đã tồn tại
        var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
        if (existingUser != null)
            throw new Exception("Email đã được đăng ký");

        var user = _mapper.Map<TaiKhoan>(registerDto);
        user.MatKhau = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        user.NgayTao = DateTime.UtcNow;
        user.TrangThai = "hoat_dong";

        _context.TaiKhoans.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
    {
        var user = await _context.TaiKhoans.FindAsync(userId);
        if (user == null) return false;

        bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.MatKhau);
        if (!isOldPasswordValid) return false;

        user.MatKhau = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
        _context.TaiKhoans.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int userId)
    {
        var user = await _context.TaiKhoans.FindAsync(userId);
        return user == null ? null : _mapper.Map<UserResponseDto>(user);
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _context.TaiKhoans.ToListAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<bool> LockUserAsync(int userId, bool isLock)
    {
        var user = await _context.TaiKhoans.FindAsync(userId);
        if (user == null) return false;

        user.TrangThai = isLock ? "khoa" : "hoat_dong";
        _context.TaiKhoans.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}