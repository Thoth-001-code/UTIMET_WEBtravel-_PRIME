using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Auth;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<UserInfo> GetCurrentUserAsync(int maTaiKhoan);
    }
}