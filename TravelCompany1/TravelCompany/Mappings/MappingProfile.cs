using AutoMapper;
using TravelCompany.API.DTOs.Auth;
using TravelCompany.API.DTOs.Booking;
using TravelCompany.API.DTOs.Customer;
using TravelCompany.API.DTOs.Destination;
using TravelCompany.API.DTOs.Invoice;
using TravelCompany.API.DTOs.Payment;
using TravelCompany.API.DTOs.Tour;
using TravelCompany.API.Models.Entities;

namespace TravelCompany.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaiKhoan, UserResponseDto>();
        CreateMap<RegisterDto, TaiKhoan>()
            .ForMember(dest => dest.MatKhau, opt => opt.Ignore()); // sẽ hash riêng
        CreateMap<CustomerRegisterDto, KhachHang>();
        CreateMap<KhachHang, CustomerResponseDto>();



        // Destination
        CreateMap<DiemDenCreateDto, DiemDen>();
        CreateMap<DiemDenUpdateDto, DiemDen>();
        CreateMap<DiemDen, DiemDenResponseDto>();

        // Tour
        CreateMap<TourCreateDto, Tour>();
        CreateMap<TourUpdateDto, Tour>();
        CreateMap<Tour, TourResponseDto>()
            .ForMember(dest => dest.TenDiemDen, opt => opt.MapFrom(src => src.DiemDen != null ? src.DiemDen.TenDiemDen : null));

        // LichKhoiHanh
        CreateMap<LichKhoiHanhCreateDto, LichKhoiHanh>();
        CreateMap<LichKhoiHanhUpdateDto, LichKhoiHanh>();
        CreateMap<LichKhoiHanh, LichKhoiHanhResponseDto>();

        // ChiTietLichTrinh
        CreateMap<ChiTietLichTrinhCreateDto, ChiTietLichTrinh>();
        CreateMap<ChiTietLichTrinhUpdateDto, ChiTietLichTrinh>();
        CreateMap<ChiTietLichTrinh, ChiTietLichTrinhResponseDto>();

        // AnhTour
        CreateMap<AnhTourCreateDto, AnhTour>();
        CreateMap<AnhTourUpdateDto, AnhTour>();
        CreateMap<AnhTour, AnhTourResponseDto>();


        // Booking
        CreateMap<BookingCreateDto, DatTour>();
        CreateMap<NguoiDiTourDto, NguoiDiTour>();
        CreateMap<DatTour, BookingResponseDto>()
            .ForMember(dest => dest.TenKhachHang, opt => opt.MapFrom(src => src.KhachHang.HoTen))
            .ForMember(dest => dest.TenTour, opt => opt.MapFrom(src => src.LichKhoiHanh.Tour.TenTour))
            .ForMember(dest => dest.NgayKhoiHanh, opt => opt.MapFrom(src => src.LichKhoiHanh.NgayKhoiHanh))
            .ForMember(dest => dest.DanhSachNguoiDi, opt => opt.MapFrom(src => src.NguoiDiTours));
        CreateMap<NguoiDiTour, NguoiDiTourDto>();



        // Payment & Invoice
        CreateMap<ThanhToanCreateDto, ThanhToan>();
        CreateMap<ThanhToan, ThanhToanResponseDto>();
        CreateMap<HoaDon, HoaDonResponseDto>()
            .ForMember(dest => dest.DanhSachThanhToan, opt => opt.MapFrom(src => src.DatTour.ThanhToans));












    }
}