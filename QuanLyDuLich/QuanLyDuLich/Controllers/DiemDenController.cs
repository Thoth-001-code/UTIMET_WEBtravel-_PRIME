using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuLich.Models.DTOs.DiemDen;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiemDenController : ControllerBase
    {
        private readonly IDiemDenService _service;

        public DiemDenController(IDiemDenService service)
        {
            _service = service;
        }

        // GET: api/diemden (công khai)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/diemden/{id} (công khai)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/diemden (chỉ Admin/Manager)
        [HttpPost]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> Create([FromForm] DiemDenRequest request)
        {
            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.MaDiemDen }, result);
        }

        // PUT: api/diemden/{id} (chỉ Admin/Manager)
        [HttpPut("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> Update(int id, [FromForm] DiemDenRequest request)
        {
            try
            {
                var result = await _service.UpdateAsync(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/diemden/{id} (chỉ Admin/Manager)
        [HttpDelete("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
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