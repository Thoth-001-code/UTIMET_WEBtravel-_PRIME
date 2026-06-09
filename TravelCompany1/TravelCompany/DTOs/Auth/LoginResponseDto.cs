namespace TravelCompany.API.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public UserResponseDto User { get; set; } = null!;
}