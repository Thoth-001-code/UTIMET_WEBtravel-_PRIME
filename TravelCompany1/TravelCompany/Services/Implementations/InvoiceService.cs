using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Invoice;
using TravelCompany.API.DTOs.Payment;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Services.Implementations;

public class InvoiceService : IInvoiceService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public InvoiceService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HoaDonResponseDto?> GetInvoiceByBookingAsync(int bookingId, int userId, string userRole)
    {
        var booking = await _context.DatTours
            .Include(b => b.HoaDon)
            .ThenInclude(h => h.DatTour).ThenInclude(d => d.ThanhToans)
            .FirstOrDefaultAsync(b => b.MaDatTour == bookingId);
        if (booking == null) return null;

        if (userRole == "customer" && booking.MaKhachHang != userId)
            throw new UnauthorizedAccessException("Không có quyền xem hóa đơn này");

        var hoaDon = booking.HoaDon;
        if (hoaDon == null) return null;

        var result = _mapper.Map<HoaDonResponseDto>(hoaDon);
        result.DanhSachThanhToan = _mapper.Map<List<ThanhToanResponseDto>>(booking.ThanhToans);
        return result;
    }
}