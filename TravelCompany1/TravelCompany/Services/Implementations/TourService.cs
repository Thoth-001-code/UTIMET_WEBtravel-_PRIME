using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Tour;
using TravelCompany.API.Models.Entities;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Services.Implementations;

public class TourService : ITourService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;

    public TourService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
    {
        _context = context;
        _mapper = mapper;
        _environment = environment;
    }

    // Tour
    public async Task<IEnumerable<TourResponseDto>> GetAllToursAsync()
    {
        var tours = await _context.Tours
            .Include(t => t.DiemDen)
            .Include(t => t.AnhTours)
            .ToListAsync();
        return _mapper.Map<IEnumerable<TourResponseDto>>(tours);
    }

    public async Task<TourResponseDto?> GetTourByIdAsync(int id)
    {
        var tour = await _context.Tours
            .Include(t => t.DiemDen)
            .Include(t => t.LichKhoiHanhs)
            .Include(t => t.ChiTietLichTrinhs)
            .Include(t => t.AnhTours)
            .FirstOrDefaultAsync(t => t.MaTour == id);
        return tour == null ? null : _mapper.Map<TourResponseDto>(tour);
    }

    public async Task<TourResponseDto> CreateTourAsync(TourCreateDto dto)
    {
        var tour = _mapper.Map<Tour>(dto);
        _context.Tours.Add(tour);
        await _context.SaveChangesAsync();
        return _mapper.Map<TourResponseDto>(tour);
    }

    public async Task<TourResponseDto?> UpdateTourAsync(TourUpdateDto dto)
    {
        var tour = await _context.Tours.FindAsync(dto.MaTour);
        if (tour == null) return null;
        _mapper.Map(dto, tour);
        await _context.SaveChangesAsync();
        return _mapper.Map<TourResponseDto>(tour);
    }

    public async Task<bool> DeleteTourAsync(int id)
    {
        var tour = await _context.Tours.FindAsync(id);
        if (tour == null) return false;
        _context.Tours.Remove(tour);
        await _context.SaveChangesAsync();
        return true;
    }
    // LichKhoiHanh
    public async Task<LichKhoiHanhResponseDto> AddLichKhoiHanhAsync(LichKhoiHanhCreateDto dto)
    {
        var lich = _mapper.Map<LichKhoiHanh>(dto);
        lich.SoChoConLai = lich.SoChoToiDa; // khi tạo mới, số chỗ còn = tối đa
        _context.LichKhoiHanhs.Add(lich);
        await _context.SaveChangesAsync();
        return _mapper.Map<LichKhoiHanhResponseDto>(lich);
    }

    public async Task<LichKhoiHanhResponseDto?> UpdateLichKhoiHanhAsync(LichKhoiHanhUpdateDto dto)
    {
        var lich = await _context.LichKhoiHanhs.FindAsync(dto.MaLich);
        if (lich == null) return null;
        _mapper.Map(dto, lich);
        await _context.SaveChangesAsync();
        return _mapper.Map<LichKhoiHanhResponseDto>(lich);
    }

    public async Task<bool> DeleteLichKhoiHanhAsync(int maLich)
    {
        var lich = await _context.LichKhoiHanhs.FindAsync(maLich);
        if (lich == null) return false;
        _context.LichKhoiHanhs.Remove(lich);
        await _context.SaveChangesAsync();
        return true;
    }

    //ChiTietLichTrinh
    public async Task<ChiTietLichTrinhResponseDto> AddChiTietLichTrinhAsync(ChiTietLichTrinhCreateDto dto)
    {
        var chiTiet = _mapper.Map<ChiTietLichTrinh>(dto);
        _context.ChiTietLichTrinhs.Add(chiTiet);
        await _context.SaveChangesAsync();
        return _mapper.Map<ChiTietLichTrinhResponseDto>(chiTiet);
    }

    public async Task<ChiTietLichTrinhResponseDto?> UpdateChiTietLichTrinhAsync(ChiTietLichTrinhUpdateDto dto)
    {
        var chiTiet = await _context.ChiTietLichTrinhs.FindAsync(dto.MaChiTiet);
        if (chiTiet == null) return null;
        _mapper.Map(dto, chiTiet);
        await _context.SaveChangesAsync();
        return _mapper.Map<ChiTietLichTrinhResponseDto>(chiTiet);
    }

    public async Task<bool> DeleteChiTietLichTrinhAsync(int maChiTiet)
    {
        var chiTiet = await _context.ChiTietLichTrinhs.FindAsync(maChiTiet);
        if (chiTiet == null) return false;
        _context.ChiTietLichTrinhs.Remove(chiTiet);
        await _context.SaveChangesAsync();
        return true;
    }

    // AnhTour
    public async Task<IEnumerable<AnhTourResponseDto>> GetAnhTourByMaTourAsync(int maTour)
    {
        var anhTours = await _context.AnhTours
            .Where(a => a.MaTour == maTour)
            .OrderBy(a => a.ThuTu)
            .ToListAsync();
        return _mapper.Map<IEnumerable<AnhTourResponseDto>>(anhTours);
    }

    public async Task<AnhTourResponseDto> AddAnhTourAsync(AnhTourCreateDto dto)
    {
        var anhTour = _mapper.Map<AnhTour>(dto);
        _context.AnhTours.Add(anhTour);
        await _context.SaveChangesAsync();
        return _mapper.Map<AnhTourResponseDto>(anhTour);
    }

    public async Task<IEnumerable<AnhTourResponseDto>> AddMultipleAnhTourAsync(IEnumerable<AnhTourCreateDto> dtos)
    {
        var anhTours = _mapper.Map<List<AnhTour>>(dtos);
        _context.AnhTours.AddRange(anhTours);
        await _context.SaveChangesAsync();
        return _mapper.Map<IEnumerable<AnhTourResponseDto>>(anhTours);
    }

    public async Task<AnhTourResponseDto?> UpdateAnhTourAsync(AnhTourUpdateDto dto)
    {
        var anhTour = await _context.AnhTours.FindAsync(dto.MaAnh);
        if (anhTour == null) return null;
        _mapper.Map(dto, anhTour);
        await _context.SaveChangesAsync();
        return _mapper.Map<AnhTourResponseDto>(anhTour);
    }

    public async Task<bool> DeleteAnhTourAsync(int maAnh)
    {
        var anhTour = await _context.AnhTours.FindAsync(maAnh);
        if (anhTour == null) return false;
        _context.AnhTours.Remove(anhTour);
        await _context.SaveChangesAsync();
        return true;
    }

    // Upload file ảnh
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File không được để trống");
        }

        // Kiểm tra định dạng file
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
        {
            throw new ArgumentException("Chỉ cho phép upload file ảnh (jpg, jpeg, png, gif)");
        }

        // Tạo tên file unique
        var fileName = $"{Guid.NewGuid()}{extension}";
        var uploadPath = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, "uploads");
        
        // Đảm bảo thư mục tồn tại
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var filePath = Path.Combine(uploadPath, fileName);

        // Lưu file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Trả về đường dẫn tương đối
        return $"/uploads/{fileName}";
    }

    public async Task<IEnumerable<string>> UploadMultipleImagesAsync(IEnumerable<IFormFile> files)
    {
        var filePaths = new List<string>();
        foreach (var file in files)
        {
            var filePath = await UploadImageAsync(file);
            filePaths.Add(filePath);
        }
        return filePaths;
    }

    public async Task<AnhTourResponseDto> UploadAndSaveImageAsync(AnhTourUploadDto dto)
    {
        // Kiểm tra tour có tồn tại không
        var tour = await _context.Tours.FindAsync(dto.MaTour);
        if (tour == null)
        {
            throw new ArgumentException("Tour không tồn tại");
        }

        // Upload file
        var filePath = await UploadImageAsync(dto.File);

        // Tạo AnhTour
        var anhTour = new AnhTour
        {
            MaTour = dto.MaTour,
            DuongDanAnh = filePath,
            MoTaAnh = dto.MoTaAnh,
            ThuTu = dto.ThuTu
        };

        _context.AnhTours.Add(anhTour);
        await _context.SaveChangesAsync();

        return _mapper.Map<AnhTourResponseDto>(anhTour);
    }

    public async Task<IEnumerable<AnhTourResponseDto>> UploadAndSaveMultipleImagesAsync(AnhTourMultipleUploadDto dto)
    {
        // Kiểm tra tour có tồn tại không
        var tour = await _context.Tours.FindAsync(dto.MaTour);
        if (tour == null)
        {
            throw new ArgumentException("Tour không tồn tại");
        }

        var result = new List<AnhTourResponseDto>();
        var thuTu = 0;
        
        foreach (var file in dto.Files)
        {
            // Upload file
            var filePath = await UploadImageAsync(file);

            // Tạo AnhTour
            var anhTour = new AnhTour
            {
                MaTour = dto.MaTour,
                DuongDanAnh = filePath,
                MoTaAnh = null,
                ThuTu = thuTu++
            };

            _context.AnhTours.Add(anhTour);
            result.Add(_mapper.Map<AnhTourResponseDto>(anhTour));
        }

        await _context.SaveChangesAsync();
        return result;
    }
}