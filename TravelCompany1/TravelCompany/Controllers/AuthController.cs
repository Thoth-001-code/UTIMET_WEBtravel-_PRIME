using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.API.DTOs.Auth;
using TravelCompany.API.Services.Interfaces;
using System.Security.Claims;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        // Validate thủ công (có thể dùng FluentValidation)
        var validator = new Validators.Auth.LoginDtoValidator();
        var validationResult = await validator.ValidateAsync(loginDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var result = await _authService.LoginAsync(loginDto);
        if (result == null)
            return Unauthorized("Sai email hoặc mật khẩu hoặc tài khoản bị khóa");

        return Ok(result);
    }

    [HttpPost("register")]
    [Authorize(Roles = "quan_tri")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var validator = new Validators.Auth.RegisterDtoValidator();
        var validationResult = await validator.ValidateAsync(registerDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try
        {
            var newUser = await _authService.RegisterAsync(registerDto);
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var userId = int.Parse(userIdClaim);
        var user = await _authService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var userId = int.Parse(userIdClaim);
        var result = await _authService.ChangePasswordAsync(userId, changePasswordDto);
        if (!result)
            return BadRequest("Mật khẩu cũ không đúng");

        return Ok(new { message = "Đổi mật khẩu thành công" });
    }

    [HttpGet("users")]
    [Authorize(Roles = "quan_tri")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _authService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPut("users/{id}/lock")]
    [Authorize(Roles = "quan_tri")]
    public async Task<IActionResult> LockUser(int id, [FromQuery] bool isLock)
    {
        var result = await _authService.LockUserAsync(id, isLock);
        if (!result)
            return NotFound();
        return Ok(new { message = isLock ? "Đã khóa tài khoản" : "Đã mở khóa tài khoản" });
    }
}