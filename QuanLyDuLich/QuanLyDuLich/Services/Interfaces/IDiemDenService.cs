using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.DiemDen;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface IDiemDenService
    {
        Task<IEnumerable<DiemDenResponse>> GetAllAsync();
        Task<DiemDenResponse> GetByIdAsync(int id);
        Task<DiemDenResponse> CreateAsync(DiemDenRequest request);
        Task<DiemDenResponse> UpdateAsync(int id, DiemDenRequest request);
        Task<bool> DeleteAsync(int id);
    }
}