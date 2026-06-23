using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuLich.Models.DTOs.Customer;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "quan_tri,quan_ly,nhan_vien")]
        public async Task<IActionResult> GetCustomers([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _customerService.GetCustomersAsync(search, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly,nhan_vien")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly,nhan_vien")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerRequest request)
        {
            try
            {
                var result = await _customerService.UpdateCustomerAsync(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "quan_tri,quan_ly,nhan_vien")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var success = await _customerService.DeleteCustomerAsync(id);
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