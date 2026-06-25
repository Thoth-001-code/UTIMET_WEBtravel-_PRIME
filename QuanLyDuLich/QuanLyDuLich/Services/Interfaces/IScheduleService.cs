using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Schedule;
using QuanLyDuLich.Models.DTOs.Common;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<List<ScheduleResponse>> GetSchedulesByTourAsync(int tourId);
        Task<PagedResult<ScheduleResponse>> GetAllSchedulesAsync(int page = 1, int pageSize = 10);
        Task<ScheduleResponse> CreateScheduleAsync(ScheduleRequest request);
        Task<ScheduleResponse> UpdateScheduleAsync(int id, ScheduleRequest request);
        Task<bool> DeleteScheduleAsync(int id);
    }
}