using TravelCompany.API.DTOs.Customer;
namespace TravelCompany.API.Services.Interfaces;

public interface ICustomerAuthService
{
    Task<CustomerResponseDto> RegisterAsync(CustomerRegisterDto registerDto);
    Task<CustomerLoginResponseDto?> LoginAsync(CustomerLoginDto loginDto);
    Task<CustomerResponseDto?> GetCustomerByIdAsync(int customerId);
}