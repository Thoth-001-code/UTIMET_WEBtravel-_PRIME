using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Payment;
using TravelCompany.API.Models.Entities;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PaymentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ThanhToanResponseDto> ProcessPaymentAsync(int userId, string userRole, ThanhToanCreateDto dto)
    {
        var booking = await _context.DatTours
            .Include(b => b.HoaDon)
            .FirstOrDefaultAsync(b => b.MaDatTour == dto.MaDatTour);
        if (booking == null)
            throw new Exception("Không tìm thấy đơn đặt tour");

        if (userRole == "customer" && booking.MaKhachHang != userId)
            throw new UnauthorizedAccessException("Không thể thanh toán đơn của khách khác");

        if (booking.TrangThai != "da_xac_nhan" && booking.TrangThai != "da_dat_coc" && booking.TrangThai != "da_thanh_toan")
            throw new Exception("Đơn hàng không ở trạng thái có thể thanh toán");

        var thanhToan = _mapper.Map<ThanhToan>(dto);
        thanhToan.NgayThanhToan = DateTime.UtcNow;
        thanhToan.TrangThai = "thanh_cong";
        _context.ThanhToans.Add(thanhToan);

        var hoaDon = booking.HoaDon;
        if (hoaDon == null)
        {
            hoaDon = new HoaDon
            {
                MaCodeHoaDon = Helpers.CodeGenerator.GenerateInvoiceCode(),
                MaDatTour = booking.MaDatTour,
                NgayXuat = DateTime.UtcNow,
                TongTien = booking.TongTien,
                DaThanhToan = 0,
                ConLai = booking.TongTien,
                TrangThai = "chua_thanh_toan"
            };
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();
        }

        hoaDon.DaThanhToan += dto.SoTien;
        hoaDon.ConLai = hoaDon.TongTien - hoaDon.DaThanhToan;

        if (hoaDon.ConLai <= 0)
        {
            hoaDon.TrangThai = "da_thanh_toan";
            booking.TrangThai = "da_thanh_toan";
        }
        else
        {
            hoaDon.TrangThai = "thanh_toan_mot_phan";
            booking.TrangThai = "da_dat_coc";
        }

        _context.HoaDons.Update(hoaDon);
        _context.DatTours.Update(booking);
        await _context.SaveChangesAsync();

        return _mapper.Map<ThanhToanResponseDto>(thanhToan);
    }

    public async Task<IEnumerable<ThanhToanResponseDto>> GetPaymentsByBookingAsync(int bookingId, int userId, string userRole)
    {
        var booking = await _context.DatTours.FindAsync(bookingId);
        if (booking == null) return new List<ThanhToanResponseDto>();

        if (userRole == "customer" && booking.MaKhachHang != userId)
            throw new UnauthorizedAccessException("Không có quyền xem thanh toán của đơn này");

        var payments = await _context.ThanhToans
            .Where(p => p.MaDatTour == bookingId)
            .OrderBy(p => p.NgayThanhToan)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ThanhToanResponseDto>>(payments);
    }
}