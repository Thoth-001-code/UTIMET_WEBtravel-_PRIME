using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Models.DTOs.Schedule;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.Entities;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScheduleResponse>> GetSchedulesByTourAsync(int tourId)
        {
            return await _context.LichKhoiHanhs
                .Where(l => l.MaTour == tourId)
                .Select(l => new ScheduleResponse
                {
                    MaLich = l.MaLich,
                    MaTour = l.MaTour ?? 0,
                    TenTour = l.Tour.TenTour,
                    NgayKhoiHanh = l.NgayKhoiHanh,
                    NgayKetThuc = l.NgayKetThuc,
                    SoChoToiDa = l.SoChoToiDa,
                    SoChoConLai = l.SoChoConLai,
                    GiaTour = l.GiaTour,
                    TrangThai = l.TrangThai
                })
                .ToListAsync();
        }

        public async Task<PagedResult<ScheduleResponse>> GetAllSchedulesAsync(int page = 1, int pageSize = 10)
        {
            var query = _context.LichKhoiHanhs
                .Include(l => l.Tour)
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new ScheduleResponse
                {
                    MaLich = l.MaLich,
                    MaTour = l.MaTour ?? 0,
                    TenTour = l.Tour.TenTour,
                    NgayKhoiHanh = l.NgayKhoiHanh,
                    NgayKetThuc = l.NgayKetThuc,
                    SoChoToiDa = l.SoChoToiDa,
                    SoChoConLai = l.SoChoConLai,
                    GiaTour = l.GiaTour,
                    TrangThai = l.TrangThai
                })
                .ToListAsync();

            return new PagedResult<ScheduleResponse>
            {
                PageIndex = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<ScheduleResponse> CreateScheduleAsync(ScheduleRequest request)
        {
            if (request.NgayKetThuc < request.NgayKhoiHanh)
                throw new InvalidOperationException("Ngày kết thúc phải lớn hơn hoặc bằng ngày khởi hành.");

            var schedule = new LichKhoiHanh
            {
                MaTour = request.MaTour,
                NgayKhoiHanh = request.NgayKhoiHanh,
                NgayKetThuc = request.NgayKetThuc,
                SoChoToiDa = request.SoChoToiDa,
                SoChoConLai = request.SoChoToiDa,
                GiaTour = request.GiaTour,
                TrangThai = request.TrangThai ?? "sap_khoi_hanh"
            };

            _context.LichKhoiHanhs.Add(schedule);
            await _context.SaveChangesAsync();

            return new ScheduleResponse
            {
                MaLich = schedule.MaLich,
                MaTour = schedule.MaTour ?? 0,
                NgayKhoiHanh = schedule.NgayKhoiHanh,
                NgayKetThuc = schedule.NgayKetThuc,
                SoChoToiDa = schedule.SoChoToiDa,
                SoChoConLai = schedule.SoChoConLai,
                GiaTour = schedule.GiaTour,
                TrangThai = schedule.TrangThai
            };
        }

        public async Task<ScheduleResponse> UpdateScheduleAsync(int id, ScheduleRequest request)
        {
            var schedule = await _context.LichKhoiHanhs.FindAsync(id);
            if (schedule == null) throw new Exception("Không tìm thấy lịch.");

            if (request.NgayKetThuc < request.NgayKhoiHanh)
                throw new InvalidOperationException("Ngày kết thúc phải lớn hơn hoặc bằng ngày khởi hành.");

            schedule.MaTour = request.MaTour;
            schedule.NgayKhoiHanh = request.NgayKhoiHanh;
            schedule.NgayKetThuc = request.NgayKetThuc;
            schedule.SoChoToiDa = request.SoChoToiDa;
            // Không cập nhật SoChoConLai vì đã có booking
            schedule.GiaTour = request.GiaTour;
            schedule.TrangThai = request.TrangThai ?? schedule.TrangThai;

            await _context.SaveChangesAsync();

            return new ScheduleResponse
            {
                MaLich = schedule.MaLich,
                MaTour = schedule.MaTour ?? 0,
                NgayKhoiHanh = schedule.NgayKhoiHanh,
                NgayKetThuc = schedule.NgayKetThuc,
                SoChoToiDa = schedule.SoChoToiDa,
                SoChoConLai = schedule.SoChoConLai,
                GiaTour = schedule.GiaTour,
                TrangThai = schedule.TrangThai
            };
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await _context.LichKhoiHanhs
                .Include(l => l.DatTours)
                .FirstOrDefaultAsync(l => l.MaLich == id);

            if (schedule == null) return false;

            if (schedule.DatTours.Any(d => d.TrangThai != "da_huy"))
                throw new InvalidOperationException("Không thể xóa lịch vì có đặt tour đang hoạt động.");

            _context.LichKhoiHanhs.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}