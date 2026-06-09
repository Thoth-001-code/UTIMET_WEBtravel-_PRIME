using TravelCompany.API.DTOs.Booking;

namespace TravelCompany.API.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(int userId, string userRole, BookingCreateDto dto);
    Task<IEnumerable<BookingResponseDto>> GetMyBookingsAsync(int customerId);
    Task<BookingResponseDto?> GetBookingByIdAsync(int bookingId, int userId, string userRole);
    Task<bool> CancelBookingAsync(int bookingId, int userId, string userRole, string? lyDo);
    Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(int page, int pageSize, string? trangThai);
    Task<bool> ConfirmBookingAsync(int bookingId, int staffId);
    Task<bool> UpdateBookingStatusAsync(int bookingId, int staffId, string newStatus, string? note);
}