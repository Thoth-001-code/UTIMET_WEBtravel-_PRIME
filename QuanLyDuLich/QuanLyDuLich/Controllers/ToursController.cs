using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuLich.Models.DTOs.Tour;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        // GET: api/tours (công khai)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTours([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _tourService.GetToursAsync(search, page, pageSize);
            return Ok(result);
        }

        // GET: api/tours/{id} (công khai)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTour(int id)
        {
            var result = await _tourService.GetTourByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/tours (chỉ Admin/Manager)
        [HttpPost]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> CreateTour([FromForm] TourRequest request)
        {
            var result = await _tourService.CreateTourAsync(request);
            return CreatedAtAction(nameof(GetTour), new { id = result.MaTour }, result);
        }

        // PUT: api/tours/{id} (chỉ Admin/Manager)
        [HttpPut("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> UpdateTour(int id, [FromForm] TourRequest request)
        {
            try
            {
                var result = await _tourService.UpdateTourAsync(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/tours/{id} (chỉ Admin/Manager)
        [HttpDelete("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            try
            {
                var success = await _tourService.DeleteTourAsync(id);
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