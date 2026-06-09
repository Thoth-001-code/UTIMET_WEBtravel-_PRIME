using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "ke_toan,quan_tri")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("revenue-by-tour")]
    public async Task<IActionResult> GetRevenueByTour([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        var result = await _reportService.GetRevenueByTourAsync(fromDate, toDate);
        return Ok(result);
    }

    [HttpGet("revenue-by-month")]
    public async Task<IActionResult> GetRevenueByMonth([FromQuery] int year)
    {
        var result = await _reportService.GetRevenueByMonthAsync(year);
        return Ok(result);
    }

    [HttpGet("top-customers")]
    public async Task<IActionResult> GetTopCustomers([FromQuery] int top = 10)
    {
        var result = await _reportService.GetTopCustomersAsync(top);
        return Ok(result);
    }

    [HttpGet("booking-status")]
    public async Task<IActionResult> GetBookingStatusStatistics()
    {
        var result = await _reportService.GetBookingStatusStatisticsAsync();
        return Ok(result);
    }

    [HttpGet("export-excel/revenue-by-tour")]
    public async Task<IActionResult> ExportRevenueByTourExcel([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        var fileBytes = await _reportService.ExportRevenueByTourExcelAsync(fromDate, toDate);
        var fileName = $"DoanhThuTheoTour_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}