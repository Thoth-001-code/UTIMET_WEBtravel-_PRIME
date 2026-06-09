using TravelCompany.API.DTOs.Payment;

namespace TravelCompany.API.Services.Interfaces;

public interface IPaymentService
{
    Task<ThanhToanResponseDto> ProcessPaymentAsync(int userId, string userRole, ThanhToanCreateDto dto);
    Task<IEnumerable<ThanhToanResponseDto>> GetPaymentsByBookingAsync(int bookingId, int userId, string userRole);
}