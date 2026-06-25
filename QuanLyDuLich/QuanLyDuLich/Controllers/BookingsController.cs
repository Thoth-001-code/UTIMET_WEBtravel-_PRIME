using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuLich.Models.DTOs.Booking;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var maKhachHang = int.TryParse(User.FindFirst("MaKhachHang")?.Value, out int kh) ? kh : (int?)null;

            var result = await _bookingService.GetBookingsAsync(maKhachHang, role, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);
            if (result == null) return NotFound();

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var maKhachHang = User.FindFirst("MaKhachHang")?.Value;
            if (role != "quan_tri" && role != "quan_ly" && role != "nhan_vien")
            {
                if (maKhachHang == null || result.MaKhachHang != int.Parse(maKhachHang))
                    return Forbid();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            try
            {
                var maTaiKhoan = int.Parse(User.FindFirst("MaTaiKhoan")?.Value ?? "0");
                var result = await _bookingService.CreateBookingAsync(request, maTaiKhoan);
                return CreatedAtAction(nameof(GetBooking), new { id = result.MaDatTour }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/confirm")]
        [Authorize(Roles = "quan_tri,quan_ly,nhan_vien")]
        public async Task<IActionResult> ConfirmBooking(int id)
        {
            try
            {
                var result = await _bookingService.ConfirmBookingAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var maKhachHang = User.FindFirst("MaKhachHang")?.Value;

                if (role == "khach_hang")
                {
                    var booking = await _bookingService.GetBookingByIdAsync(id);
                    if (booking == null) return NotFound();
                    if (booking.MaKhachHang != int.Parse(maKhachHang))
                        return Forbid();
                }

                var result = await _bookingService.CancelBookingAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "quan_tri,quan_ly,nhan_vien")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] string status)
        {
            try
            {
                var result = await _bookingService.UpdateBookingStatusAsync(id, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}