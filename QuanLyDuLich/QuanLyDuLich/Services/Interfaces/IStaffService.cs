using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.DTOs.Staff;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface IStaffService
    {
        Task<PagedResult<StaffResponse>> GetStaffsAsync(string roleFilter, int pageIndex, int pageSize);
        Task<StaffResponse> CreateStaffAsync(StaffRequest request);
        Task<StaffResponse> ToggleLockStaffAsync(int id);
        Task<bool> DeleteStaffAsync(int id);
    }
}