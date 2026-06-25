using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuLich.Models.DTOs.Schedule;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Authorize(Roles = "quan_tri, quan_ly")]
        public async Task<IActionResult> GetAllSchedules([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _scheduleService.GetAllSchedulesAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("by-tour/{tourId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSchedulesByTour(int tourId)
        {
            var result = await _scheduleService.GetSchedulesByTourAsync(tourId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleRequest request)
        {
            try
            {
                var result = await _scheduleService.CreateScheduleAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleRequest request)
        {
            try
            {
                var result = await _scheduleService.UpdateScheduleAsync(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                var success = await _scheduleService.DeleteScheduleAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}