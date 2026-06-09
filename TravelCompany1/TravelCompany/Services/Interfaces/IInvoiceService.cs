using TravelCompany.API.DTOs.Invoice;

namespace TravelCompany.API.Services.Interfaces;

public interface IInvoiceService
{
    Task<HoaDonResponseDto?> GetInvoiceByBookingAsync(int bookingId, int userId, string userRole);
}