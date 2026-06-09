using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Booking;
using TravelCompany.API.Helpers;
using TravelCompany.API.Models.Entities;
using TravelCompany.API.Services.Interfaces;
using TravelCompany.API.Validators.Booking;
using FluentValidation;
using FluentValidation.Results;

namespace TravelCompany.API.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<BookingService> _logger;

    public BookingService(AppDbContext context, IMapper mapper, ILogger<BookingService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookingResponseDto> CreateBookingAsync(int userId, string userRole, BookingCreateDto dto)
    {
        _logger.LogInformation("Creating booking. UserId: {UserId}, Role: {Role}", userId, userRole);

        // Validate DTO
        var validator = new BookingCreateDtoValidator();
        ValidationResult validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for booking creation: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        // Lấy thông tin lịch khởi hành
        var lich = await _context.LichKhoiHanhs
            .Include(l => l.Tour)
            .FirstOrDefaultAsync(l => l.MaLich == dto.MaLich);
        if (lich == null)
            throw new Exception("Lịch khởi hành không tồn tại");

        if (lich.SoChoConLai < dto.SoLuongNguoi)
            throw new Exception("Không đủ chỗ trống cho lịch khởi hành này");

        // Xác định MaKhachHang
        int maKhachHang;
        if (userRole == "customer")
        {
            maKhachHang = userId;
        }
        else
        {
            if (dto.MaKhachHang == null)
                throw new Exception("Nhân viên phải cung cấp MaKhachHang khi tạo booking");
            maKhachHang = dto.MaKhachHang.Value;
        }

        // Tạo booking
        var booking = new DatTour
        {
            MaCodeDat = CodeGenerator.GenerateBookingCode(),
            MaKhachHang = maKhachHang,
            MaLich = dto.MaLich,
            MaTaiKhoan = userRole == "customer" ? null : userId,
            NgayDat = DateTime.UtcNow,
            SoLuongNguoi = dto.SoLuongNguoi,
            TongTien = dto.SoLuongNguoi * lich.GiaTour,
            TienDatCoc = 0,
            TienConLai = dto.SoLuongNguoi * lich.GiaTour,
            PhiHuy = 0,
            TrangThai = "cho_xac_nhan",
            GhiChu = dto.GhiChu
        };

        _context.DatTours.Add(booking);
        await _context.SaveChangesAsync();

        // Thêm danh sách người đi
        if (dto.DanhSachNguoiDi != null && dto.DanhSachNguoiDi.Any())
        {
            var nguoiDiList = dto.DanhSachNguoiDi.Select(nd => new NguoiDiTour
            {
                MaDatTour = booking.MaDatTour,
                HoTen = nd.HoTen,
                GioiTinh = nd.GioiTinh,
                NgaySinh = nd.NgaySinh,
                SoCCCD = nd.SoCCCD,
                SoDienThoai = nd.SoDienThoai
            }).ToList();
            _context.NguoiDiTours.AddRange(nguoiDiList);
            await _context.SaveChangesAsync();
        }

        // Ghi lịch sử
        var history = new LichSuDatTour
        {
            MaDatTour = booking.MaDatTour,
            MaTaiKhoan = userRole == "customer" ? null : userId,
            HanhDong = "Tạo đơn",
            TrangThaiCu = null,
            TrangThaiMoi = "cho_xac_nhan",
            GhiChu = dto.GhiChu,
            NgayTao = DateTime.UtcNow
        };
        _context.LichSuDatTours.Add(history);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Booking created successfully. BookingId: {BookingId}", booking.MaDatTour);
        return await GetBookingByIdAsync(booking.MaDatTour, userId, userRole);
    }

    public async Task<IEnumerable<BookingResponseDto>> GetMyBookingsAsync(int customerId)
    {
        var bookings = await _context.DatTours
            .Include(b => b.KhachHang)
            .Include(b => b.LichKhoiHanh).ThenInclude(l => l.Tour)
            .Include(b => b.NguoiDiTours)
            .Where(b => b.MaKhachHang == customerId)
            .OrderByDescending(b => b.NgayDat)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
    }

    public async Task<BookingResponseDto?> GetBookingByIdAsync(int bookingId, int userId, string userRole)
    {
        var booking = await _context.DatTours
            .Include(b => b.KhachHang)
            .Include(b => b.LichKhoiHanh).ThenInclude(l => l.Tour)
            .Include(b => b.NguoiDiTours)
            .FirstOrDefaultAsync(b => b.MaDatTour == bookingId);

        if (booking == null) return null;

        if (userRole == "customer" && booking.MaKhachHang != userId)
            throw new UnauthorizedAccessException("Không có quyền xem đơn này");

        return _mapper.Map<BookingResponseDto>(booking);
    }

    public async Task<bool> CancelBookingAsync(int bookingId, int userId, string userRole, string? lyDo = null)
    {
        _logger.LogInformation("Cancelling booking {BookingId} by UserId {UserId} role {Role}", bookingId, userId, userRole);

        var booking = await _context.DatTours
            .Include(b => b.LichKhoiHanh)
            .FirstOrDefaultAsync(b => b.MaDatTour == bookingId);
        if (booking == null) return false;

        if (userRole == "customer" && booking.MaKhachHang != userId)
            throw new UnauthorizedAccessException("Không thể hủy đơn của khách khác");

        if (booking.TrangThai != "cho_xac_nhan" && booking.TrangThai != "da_xac_nhan")
            throw new Exception("Không thể hủy đơn ở trạng thái hiện tại");

        // Tính phí hủy
        decimal phiHuy = 0;
        if (booking.TrangThai == "da_xac_nhan")
        {
            phiHuy = booking.TongTien * 0.1m; // 10% phí hủy
        }

        // Hoàn trả số chỗ nếu đã xác nhận
        if (booking.TrangThai == "da_xac_nhan")
        {
            var lich = booking.LichKhoiHanh;
            lich.SoChoConLai += booking.SoLuongNguoi;
            _context.LichKhoiHanhs.Update(lich);
        }

        booking.TrangThai = "da_huy";
        booking.PhiHuy = phiHuy;
        booking.GhiChu = string.IsNullOrEmpty(booking.GhiChu) ? lyDo : booking.GhiChu + " | " + lyDo;
        _context.DatTours.Update(booking);

        var history = new LichSuDatTour
        {
            MaDatTour = bookingId,
            MaTaiKhoan = userRole == "customer" ? null : userId,
            HanhDong = "Hủy đơn",
            TrangThaiCu = booking.TrangThai,
            TrangThaiMoi = "da_huy",
            GhiChu = lyDo,
            NgayTao = DateTime.UtcNow
        };
        _context.LichSuDatTours.Add(history);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Booking {BookingId} cancelled", bookingId);
        return true;
    }

    public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync(int page, int pageSize, string? trangThai = null)
    {
        var query = _context.DatTours
            .Include(b => b.KhachHang)
            .Include(b => b.LichKhoiHanh).ThenInclude(l => l.Tour)
            .Include(b => b.NguoiDiTours)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(trangThai))
            query = query.Where(b => b.TrangThai == trangThai);

        var bookings = await query
            .OrderByDescending(b => b.NgayDat)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
    }

    public async Task<bool> ConfirmBookingAsync(int bookingId, int staffId)
    {
        _logger.LogInformation("Confirming booking {BookingId} by Staff {StaffId}", bookingId, staffId);

        var booking = await _context.DatTours
            .Include(b => b.LichKhoiHanh)
            .FirstOrDefaultAsync(b => b.MaDatTour == bookingId);
        if (booking == null) return false;
        if (booking.TrangThai != "cho_xac_nhan")
            throw new Exception("Chỉ có thể xác nhận đơn ở trạng thái chờ xác nhận");

        var lich = booking.LichKhoiHanh;
        if (lich.SoChoConLai < booking.SoLuongNguoi)
            throw new Exception("Không đủ chỗ cho đơn này, vui lòng liên hệ khách hàng");

        // Trừ số chỗ
        lich.SoChoConLai -= booking.SoLuongNguoi;
        if (lich.SoChoConLai == 0) lich.TrangThai = "het_cho";
        _context.LichKhoiHanhs.Update(lich);

        booking.TrangThai = "da_xac_nhan";
        booking.MaTaiKhoan = staffId;
        _context.DatTours.Update(booking);

        var history = new LichSuDatTour
        {
            MaDatTour = bookingId,
            MaTaiKhoan = staffId,
            HanhDong = "Xác nhận đơn",
            TrangThaiCu = "cho_xac_nhan",
            TrangThaiMoi = "da_xac_nhan",
            NgayTao = DateTime.UtcNow
        };
        _context.LichSuDatTours.Add(history);

        // Tạo hóa đơn nếu chưa có
        var existingInvoice = await _context.HoaDons.FirstOrDefaultAsync(h => h.MaDatTour == bookingId);
        if (existingInvoice == null)
        {
            var hoaDon = new HoaDon
            {
                MaCodeHoaDon = CodeGenerator.GenerateInvoiceCode(),
                MaDatTour = bookingId,
                NgayXuat = DateTime.UtcNow,
                TongTien = booking.TongTien,
                DaThanhToan = 0,
                ConLai = booking.TongTien,
                TrangThai = "chua_thanh_toan"
            };
            _context.HoaDons.Add(hoaDon);
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Booking {BookingId} confirmed", bookingId);
        return true;
    }

    public async Task<bool> UpdateBookingStatusAsync(int bookingId, int staffId, string newStatus, string? note = null)
    {
        _logger.LogInformation("Updating booking {BookingId} status to {NewStatus} by Staff {StaffId}", bookingId, newStatus, staffId);

        var booking = await _context.DatTours.FindAsync(bookingId);
        if (booking == null) return false;

        var oldStatus = booking.TrangThai;
        booking.TrangThai = newStatus;
        booking.GhiChu = note ?? booking.GhiChu;
        _context.DatTours.Update(booking);

        var history = new LichSuDatTour
        {
            MaDatTour = bookingId,
            MaTaiKhoan = staffId,
            HanhDong = "Cập nhật trạng thái",
            TrangThaiCu = oldStatus,
            TrangThaiMoi = newStatus,
            GhiChu = note,
            NgayTao = DateTime.UtcNow
        };
        _context.LichSuDatTours.Add(history);
        await _context.SaveChangesAsync();

        return true;
    }
}