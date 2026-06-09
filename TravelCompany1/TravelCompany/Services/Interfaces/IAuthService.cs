using TravelCompany.API.DTOs.Auth;

namespace TravelCompany.API.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
    Task<UserResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
    Task<UserResponseDto?> GetUserByIdAsync(int userId);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<bool> LockUserAsync(int userId, bool isLock);
}