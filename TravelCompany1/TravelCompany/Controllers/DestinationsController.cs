using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.API.DTOs.Destination;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DestinationsController : ControllerBase
{
    private readonly IDestinationService _destinationService;

    public DestinationsController(IDestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _destinationService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _destinationService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> Create([FromBody] DiemDenCreateDto dto)
    {
        var result = await _destinationService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.MaDiemDen }, result);
    }

    [HttpPut]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> Update([FromBody] DiemDenUpdateDto dto)
    {
        var result = await _destinationService.UpdateAsync(dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _destinationService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}