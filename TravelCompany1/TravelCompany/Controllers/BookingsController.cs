using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelCompany.API.DTOs.Booking;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    private (int userId, string role) GetUserInfo()
    {
        // Thử lấy từ claim "sub" hoặc "nameid"
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        var roleClaim = User.FindFirst(ClaimTypes.Role);
        if (userIdClaim == null || roleClaim == null)
            throw new UnauthorizedAccessException("Invalid token");

        int userId = int.Parse(userIdClaim.Value);
        string role = roleClaim.Value;
        return (userId, role);
    }

    [HttpPost]
    [Authorize(Roles = "customer,nhan_vien,quan_ly,ke_toan")]
    public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
    {
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _bookingService.CreateBookingAsync(userId, role, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my-bookings")]
    [Authorize(Roles = "customer")]
    public async Task<IActionResult> GetMyBookings()
    {
        var (userId, role) = GetUserInfo();
        var result = await _bookingService.GetMyBookingsAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetBookingById(int id)
    {
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _bookingService.GetBookingByIdAsync(id, userId, role);
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

    [HttpPut("{id}/cancel")]
    [Authorize(Roles = "customer,nhan_vien,quan_ly,ke_toan")]
    public async Task<IActionResult> CancelBooking(int id, [FromBody] BookingCancelDto? cancelDto)
    {
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _bookingService.CancelBookingAsync(id, userId, role, cancelDto?.LyDoHuy);
            if (!result) return NotFound();
            return Ok(new { message = "Hủy đơn thành công" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles = "nhan_vien,quan_ly,ke_toan")]
    public async Task<IActionResult> GetAllBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? trangThai = null)
    {
        var result = await _bookingService.GetAllBookingsAsync(page, pageSize, trangThai);
        return Ok(result);
    }

    [HttpPut("{id}/confirm")]
    [Authorize(Roles = "nhan_vien,quan_ly")]
    public async Task<IActionResult> ConfirmBooking(int id)
    {
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _bookingService.ConfirmBookingAsync(id, userId);
            if (!result) return NotFound();
            return Ok(new { message = "Xác nhận đơn thành công" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "nhan_vien,quan_ly")]
    public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] BookingStatusUpdateDto dto)
    {
        if (dto.MaDatTour != id) return BadRequest("ID không khớp");
        try
        {
            var (userId, role) = GetUserInfo();
            var result = await _bookingService.UpdateBookingStatusAsync(id, userId, dto.TrangThaiMoi, dto.GhiChu);
            if (!result) return NotFound();
            return Ok(new { message = "Cập nhật trạng thái thành công" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}