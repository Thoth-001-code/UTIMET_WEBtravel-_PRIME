using System.Threading.Tasks;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.DTOs.Customer;

namespace QuanLyDuLich.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<PagedResult<CustomerResponse>> GetCustomersAsync(string search, int pageIndex, int pageSize);
        Task<CustomerDetailResponse> GetCustomerByIdAsync(int id);
        Task<CustomerResponse> UpdateCustomerAsync(int id, CustomerRequest request);
        Task<bool> DeleteCustomerAsync(int id);
    }
}