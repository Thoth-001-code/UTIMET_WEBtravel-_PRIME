using TravelCompany.API.DTOs.Destination;

namespace TravelCompany.API.Services.Interfaces;

public interface IDestinationService
{
    Task<IEnumerable<DiemDenResponseDto>> GetAllAsync();
    Task<DiemDenResponseDto?> GetByIdAsync(int id);
    Task<DiemDenResponseDto> CreateAsync(DiemDenCreateDto dto);
    Task<DiemDenResponseDto?> UpdateAsync(DiemDenUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}