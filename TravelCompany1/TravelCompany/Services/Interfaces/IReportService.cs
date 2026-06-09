using TravelCompany.API.DTOs.Report;

namespace TravelCompany.API.Services.Interfaces;

public interface IReportService
{
    Task<IEnumerable<DoanhThuTheoTourDto>> GetRevenueByTourAsync(DateTime? fromDate, DateTime? toDate);
    Task<IEnumerable<DoanhThuTheoThangDto>> GetRevenueByMonthAsync(int year);
    Task<IEnumerable<TopKhachHangDto>> GetTopCustomersAsync(int top);
    Task<IEnumerable<ThongKeTrangThaiBookingDto>> GetBookingStatusStatisticsAsync();
    Task<byte[]> ExportRevenueByTourExcelAsync(DateTime? fromDate, DateTime? toDate);
}