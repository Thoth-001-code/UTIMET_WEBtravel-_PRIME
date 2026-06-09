using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Destination;
using TravelCompany.API.Models.Entities;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Services.Implementations;

public class DestinationService : IDestinationService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DestinationService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DiemDenResponseDto>> GetAllAsync()
    {
        var entities = await _context.DiemDens.ToListAsync();
        return _mapper.Map<IEnumerable<DiemDenResponseDto>>(entities);
    }

    public async Task<DiemDenResponseDto?> GetByIdAsync(int id)
    {
        var entity = await _context.DiemDens.FindAsync(id);
        return entity == null ? null : _mapper.Map<DiemDenResponseDto>(entity);
    }

    public async Task<DiemDenResponseDto> CreateAsync(DiemDenCreateDto dto)
    {
        var entity = _mapper.Map<DiemDen>(dto);
        _context.DiemDens.Add(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<DiemDenResponseDto>(entity);
    }

    public async Task<DiemDenResponseDto?> UpdateAsync(DiemDenUpdateDto dto)
    {
        var entity = await _context.DiemDens.FindAsync(dto.MaDiemDen);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<DiemDenResponseDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.DiemDens.FindAsync(id);
        if (entity == null) return false;
        _context.DiemDens.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}