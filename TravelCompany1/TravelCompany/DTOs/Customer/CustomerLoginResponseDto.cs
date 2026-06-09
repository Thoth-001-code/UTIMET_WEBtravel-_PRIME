namespace TravelCompany.API.DTOs.Customer;

public class CustomerLoginResponseDto
{
    public string Token { get; set; } = null!;
    public CustomerResponseDto Customer { get; set; } = null!;
}