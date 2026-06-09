using FluentValidation;
using TravelCompany.API.DTOs.Booking;

namespace TravelCompany.API.Validators.Booking;

public class BookingCreateDtoValidator : AbstractValidator<BookingCreateDto>
{
    public BookingCreateDtoValidator()
    {
        RuleFor(x => x.MaLich).GreaterThan(0);
        RuleFor(x => x.SoLuongNguoi).GreaterThan(0);
        RuleForEach(x => x.DanhSachNguoiDi).SetValidator(new NguoiDiTourDtoValidator());
    }
}

public class NguoiDiTourDtoValidator : AbstractValidator<NguoiDiTourDto>
{
    public NguoiDiTourDtoValidator()
    {
        RuleFor(x => x.HoTen).NotEmpty().MaximumLength(100);
        RuleFor(x => x.SoCCCD).MaximumLength(50);
        RuleFor(x => x.SoDienThoai).MaximumLength(20);
    }
}