using Microsoft.AspNetCore.Http;
using TravelCompany.API.DTOs.Tour;

namespace TravelCompany.API.Services.Interfaces;

public interface ITourService
{
    // Tour
    Task<IEnumerable<TourResponseDto>> GetAllToursAsync();
    Task<TourResponseDto?> GetTourByIdAsync(int id);
    Task<TourResponseDto> CreateTourAsync(TourCreateDto dto);
    Task<TourResponseDto?> UpdateTourAsync(TourUpdateDto dto);
    Task<bool> DeleteTourAsync(int id);

    // LichKhoiHanh
    Task<LichKhoiHanhResponseDto> AddLichKhoiHanhAsync(LichKhoiHanhCreateDto dto);
    Task<LichKhoiHanhResponseDto?> UpdateLichKhoiHanhAsync(LichKhoiHanhUpdateDto dto);
    Task<bool> DeleteLichKhoiHanhAsync(int maLich);

    // ChiTietLichTrinh
    Task<ChiTietLichTrinhResponseDto> AddChiTietLichTrinhAsync(ChiTietLichTrinhCreateDto dto);
    Task<ChiTietLichTrinhResponseDto?> UpdateChiTietLichTrinhAsync(ChiTietLichTrinhUpdateDto dto);
    Task<bool> DeleteChiTietLichTrinhAsync(int maChiTiet);

    // AnhTour
    Task<IEnumerable<AnhTourResponseDto>> GetAnhTourByMaTourAsync(int maTour);
    Task<AnhTourResponseDto> AddAnhTourAsync(AnhTourCreateDto dto);
    Task<IEnumerable<AnhTourResponseDto>> AddMultipleAnhTourAsync(IEnumerable<AnhTourCreateDto> dtos);
    Task<AnhTourResponseDto?> UpdateAnhTourAsync(AnhTourUpdateDto dto);
    Task<bool> DeleteAnhTourAsync(int maAnh);
    
    // Upload file
    Task<string> UploadImageAsync(IFormFile file);
    Task<IEnumerable<string>> UploadMultipleImagesAsync(IEnumerable<IFormFile> files);
    
    // Upload và lưu ảnh trực tiếp
    Task<AnhTourResponseDto> UploadAndSaveImageAsync(AnhTourUploadDto dto);
    Task<IEnumerable<AnhTourResponseDto>> UploadAndSaveMultipleImagesAsync(AnhTourMultipleUploadDto dto);
}