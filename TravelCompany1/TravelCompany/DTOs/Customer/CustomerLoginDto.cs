using System.ComponentModel.DataAnnotations;
namespace TravelCompany.API.DTOs.Customer;

public class CustomerLoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string MatKhau { get; set; } = null!;
}