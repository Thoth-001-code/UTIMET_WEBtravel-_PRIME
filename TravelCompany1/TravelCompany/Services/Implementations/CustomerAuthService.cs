using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Customer;
using TravelCompany.API.Helpers;
using TravelCompany.API.Models.Entities;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Services.Implementations;

public class CustomerAuthService : ICustomerAuthService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CustomerAuthService(AppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<CustomerResponseDto> RegisterAsync(CustomerRegisterDto registerDto)
    {
        var existing = await _context.KhachHangs.FirstOrDefaultAsync(k => k.Email == registerDto.Email);
        if (existing != null)
            throw new Exception("Email đã được đăng ký");

        var customer = _mapper.Map<KhachHang>(registerDto);
        customer.MatKhau = BCrypt.Net.BCrypt.HashPassword(registerDto.MatKhau);
        customer.LoaiKhach = "moi";
        customer.NgayTao = DateTime.UtcNow;

        _context.KhachHangs.Add(customer);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<CustomerLoginResponseDto?> LoginAsync(CustomerLoginDto loginDto)
    {
        var customer = await _context.KhachHangs.FirstOrDefaultAsync(k => k.Email == loginDto.Email);
        if (customer == null || string.IsNullOrEmpty(customer.MatKhau))
            return null;

        bool isValid = BCrypt.Net.BCrypt.Verify(loginDto.MatKhau, customer.MatKhau);
        if (!isValid) return null;

        // Tạo token với role "customer"
        var token = JwtHelper.GenerateCustomerToken(customer, _configuration);
        var customerDto = _mapper.Map<CustomerResponseDto>(customer);
        return new CustomerLoginResponseDto
        {
            Token = token,
            Customer = customerDto
        };
    }

    public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int customerId)
    {
        var customer = await _context.KhachHangs.FindAsync(customerId);
        return customer == null ? null : _mapper.Map<CustomerResponseDto>(customer);
    }
}