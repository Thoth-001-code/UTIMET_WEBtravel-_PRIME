using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Models.DTOs.Dashboard;
using QuanLyDuLich.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDuLich.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsResponse> GetStatsAsync()
        {
            var totalTours = await _context.Tours.CountAsync();
            var totalCustomers = await _context.KhachHangs.CountAsync();
            var totalRevenue = await _context.ThanhToans
                .Where(t => t.TrangThai == "thanh_cong")
                .SumAsync(t => t.SoTien) ?? 0;

            return new DashboardStatsResponse
            {
                TotalTours = totalTours,
                TotalCustomers = totalCustomers,
                TotalRevenue = totalRevenue
            };
        }

        public async Task<List<RecentBookingResponse>> GetRecentBookingsAsync(int count)
        {
            return await _context.DatTours
                .OrderByDescending(d => d.NgayDat)
                .Take(count)
                .Include(d => d.KhachHang)
                .Select(d => new RecentBookingResponse
                {
                    MaDatTour = d.MaDatTour,
                    MaCodeDat = d.MaCodeDat,
                    TenKhachHang = d.KhachHang.HoTen,
                    NgayDat = d.NgayDat,
                    TongTien = d.TongTien,
                    TrangThai = d.TrangThai
                })
                .ToListAsync();
        }
    }
}   