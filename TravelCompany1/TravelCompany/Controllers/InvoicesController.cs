using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    private (int userId, string role) GetUserInfo()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        var roleClaim = User.FindFirst(ClaimTypes.Role);
        if (userIdClaim == null || roleClaim == null)
            throw new UnauthorizedAccessException("Invalid token");
        return (int.Parse(userIdClaim.Value), roleClaim.Value);
    }

    [HttpGet("booking/{bookingId}")]
    public async Task<IActionResult> GetInvoiceByBooking(int bookingId)
    {
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _invoiceService.GetInvoiceByBookingAsync(bookingId, userId, role);
            if (result == null) return NotFound();
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