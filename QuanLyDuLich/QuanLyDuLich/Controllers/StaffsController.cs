using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuLich.Models.DTOs.Staff;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "quan_tri")]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffs([FromQuery] string role = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _staffService.GetStaffsAsync(role, page, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] StaffRequest request)
        {
            try
            {
                var result = await _staffService.CreateStaffAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/lock")]
        public async Task<IActionResult> ToggleLock(int id)
        {
            try
            {
                var result = await _staffService.ToggleLockStaffAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            try
            {
                var success = await _staffService.DeleteStaffAsync(id);
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