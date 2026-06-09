using FluentValidation;
using TravelCompany.API.DTOs.Auth;

namespace TravelCompany.API.Validators.Auth;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.HoTen).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.SoDienThoai).NotEmpty().Matches(@"^0[0-9]{9,10}$").WithMessage("Số điện thoại không hợp lệ");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.VaiTro).Must(role => new[] { "quan_tri", "quan_ly", "nhan_vien", "ke_toan" }.Contains(role))
            .WithMessage("Vai trò không hợp lệ");
    }
}   