using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Dashboard;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsResponse> GetStatsAsync();
        Task<List<RecentBookingResponse>> GetRecentBookingsAsync(int count = 5);
    }
}