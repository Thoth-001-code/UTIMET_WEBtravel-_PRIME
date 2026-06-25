using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Booking;
using QuanLyDuLich.Models.DTOs.Common;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface IBookingService
    {
        Task<PagedResult<BookingResponse>> GetBookingsAsync(int? maKhachHang, string role, int pageIndex, int pageSize);
        Task<BookingDetailResponse> GetBookingByIdAsync(int id);
        Task<BookingResponse> CreateBookingAsync(BookingRequest request, int maTaiKhoan);
        Task<BookingResponse> ConfirmBookingAsync(int id);
        Task<BookingResponse> CancelBookingAsync(int id);
        Task<BookingResponse> UpdateBookingStatusAsync(int id, string newStatus);
    }
}