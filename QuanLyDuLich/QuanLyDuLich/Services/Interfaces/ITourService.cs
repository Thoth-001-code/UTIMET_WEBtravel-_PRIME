using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.DTOs.Tour;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface ITourService
    {
        Task<PagedResult<TourResponse>> GetToursAsync(string search, int pageIndex, int pageSize);
        Task<TourDetailResponse> GetTourByIdAsync(int id);
        Task<TourResponse> CreateTourAsync(TourRequest request);
        Task<TourResponse> UpdateTourAsync(int id, TourRequest request);
        Task<bool> DeleteTourAsync(int id);
    }
}