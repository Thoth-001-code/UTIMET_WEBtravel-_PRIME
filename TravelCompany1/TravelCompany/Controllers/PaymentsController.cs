using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelCompany.API.DTOs.Payment;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    private (int userId, string role) GetUserInfo()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        var roleClaim = User.FindFirst(ClaimTypes.Role);
        if (userIdClaim == null || roleClaim == null)
            throw new UnauthorizedAccessException("Invalid token");
        return (int.Parse(userIdClaim.Value), roleClaim.Value);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment([FromBody] ThanhToanCreateDto dto)
    {
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _paymentService.ProcessPaymentAsync(userId, role, dto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("booking/{bookingId}")]
    public async Task<IActionResult> GetPaymentsByBooking(int bookingId)
    {
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _paymentService.GetPaymentsByBookingAsync(bookingId, userId, role);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}