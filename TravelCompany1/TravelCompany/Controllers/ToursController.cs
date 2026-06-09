using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.API.DTOs.Tour;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToursController : ControllerBase
{
    private readonly ITourService _tourService;

    public ToursController(ITourService tourService)
    {
        _tourService = tourService;
    }

    // ===== TOUR =====
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllTours()
    {
        var result = await _tourService.GetAllToursAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTourById(int id)
    {
        var result = await _tourService.GetTourByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> CreateTour([FromBody] TourCreateDto dto)
    {
        var result = await _tourService.CreateTourAsync(dto);
        return CreatedAtAction(nameof(GetTourById), new { id = result.MaTour }, result);
    }

    [HttpPut]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> UpdateTour([FromBody] TourUpdateDto dto)
    {
        var result = await _tourService.UpdateTourAsync(dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> DeleteTour(int id)
    {
        var result = await _tourService.DeleteTourAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    // ===== LICH KHOI HANH =====
    [HttpPost("schedules")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> AddLichKhoiHanh([FromBody] LichKhoiHanhCreateDto dto)
    {
        var result = await _tourService.AddLichKhoiHanhAsync(dto);
        return Ok(result);
    }

    [HttpPut("schedules")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> UpdateLichKhoiHanh([FromBody] LichKhoiHanhUpdateDto dto)
    {
        var result = await _tourService.UpdateLichKhoiHanhAsync(dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("schedules/{maLich}")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> DeleteLichKhoiHanh(int maLich)
    {
        var result = await _tourService.DeleteLichKhoiHanhAsync(maLich);
        if (!result) return NotFound();
        return NoContent();
    }

    // ===== CHI TIET LICH TRINH =====
    [HttpPost("details")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> AddChiTietLichTrinh([FromBody] ChiTietLichTrinhCreateDto dto)
    {
        var result = await _tourService.AddChiTietLichTrinhAsync(dto);
        return Ok(result);
    }

    [HttpPut("details")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> UpdateChiTietLichTrinh([FromBody] ChiTietLichTrinhUpdateDto dto)
    {
        var result = await _tourService.UpdateChiTietLichTrinhAsync(dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("details/{maChiTiet}")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> DeleteChiTietLichTrinh(int maChiTiet)
    {
        var result = await _tourService.DeleteChiTietLichTrinhAsync(maChiTiet);
        if (!result) return NotFound();
        return NoContent();
    }

    // ===== ANH TOUR =====
    [HttpGet("{maTour}/images")]
    [AllowAnonymous]
    public async Task<IActionResult> GetImagesByTourId(int maTour)
    {
        var result = await _tourService.GetAnhTourByMaTourAsync(maTour);
        return Ok(result);
    }

    // Upload và lưu ảnh trực tiếp
    [HttpPost("images/upload-and-save")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> UploadAndSaveSingleImage(int maTour, IFormFile file, string? moTaAnh = null, int thuTu = 0)
    {
        var dto = new AnhTourUploadDto
        {
            MaTour = maTour,
            File = file,
            MoTaAnh = moTaAnh,
            ThuTu = thuTu
        };
        var result = await _tourService.UploadAndSaveImageAsync(dto);
        return Ok(result);
    }

    [HttpPost("images/upload-and-save-multiple")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> UploadAndSaveMultipleImages(int maTour, List<IFormFile> files)
    {
        var dto = new AnhTourMultipleUploadDto
        {
            MaTour = maTour,
            Files = files
        };
        var result = await _tourService.UploadAndSaveMultipleImagesAsync(dto);
        return Ok(result);
    }

    [HttpPut("images")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> UpdateImage([FromBody] AnhTourUpdateDto dto)
    {
        var result = await _tourService.UpdateAnhTourAsync(dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("images/{maAnh}")]
    [Authorize(Roles = "quan_ly,quan_tri")]
    public async Task<IActionResult> DeleteImage(int maAnh)
    {
        var result = await _tourService.DeleteAnhTourAsync(maAnh);
        if (!result) return NotFound();
        return NoContent();
    }
}