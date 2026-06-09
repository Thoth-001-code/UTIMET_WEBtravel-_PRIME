using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.API.DTOs.Customer;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerAuthController : ControllerBase
{
    private readonly ICustomerAuthService _customerAuthService;

    public CustomerAuthController(ICustomerAuthService customerAuthService)
    {
        _customerAuthService = customerAuthService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CustomerRegisterDto registerDto)
    {
        try
        {
            var result = await _customerAuthService.RegisterAsync(registerDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CustomerLoginDto loginDto)
    {
        var result = await _customerAuthService.LoginAsync(loginDto);
        if (result == null)
            return Unauthorized("Sai email hoặc mật khẩu");
        return Ok(result);
    }

    [Authorize(Roles = "customer")]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentCustomer()
    {
        var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId")?.Value;
        if (string.IsNullOrEmpty(customerIdClaim))
            return Unauthorized();
        var customerId = int.Parse(customerIdClaim);
        var customer = await _customerAuthService.GetCustomerByIdAsync(customerId);
        if (customer == null)
            return NotFound();
        return Ok(customer);
    }
}