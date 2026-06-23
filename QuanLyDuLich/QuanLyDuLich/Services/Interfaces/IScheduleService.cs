using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Schedule;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<List<ScheduleResponse>> GetSchedulesByTourAsync(int tourId);
        Task<ScheduleResponse> CreateScheduleAsync(ScheduleRequest request);
        Task<ScheduleResponse> UpdateScheduleAsync(int id, ScheduleRequest request);
        Task<bool> DeleteScheduleAsync(int id);
    }
}