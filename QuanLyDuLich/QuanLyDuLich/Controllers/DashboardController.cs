using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "quan_tri,quan_ly")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var result = await _dashboardService.GetStatsAsync();
            return Ok(result);
        }

        [HttpGet("recent-bookings")]
        public async Task<IActionResult> GetRecentBookings([FromQuery] int count = 5)
        {
            var result = await _dashboardService.GetRecentBookingsAsync(count);
            return Ok(result);
        }
    }
}