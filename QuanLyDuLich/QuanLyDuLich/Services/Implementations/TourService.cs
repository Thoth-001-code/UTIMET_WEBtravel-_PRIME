using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Helpers;
using QuanLyDuLich.Models.DTOs.Common;
using QuanLyDuLich.Models.DTOs.Tour;
using QuanLyDuLich.Models.DTOs.Schedule;
using QuanLyDuLich.Models.Entities;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Services
{
    public class TourService : ITourService
    {
        private readonly ApplicationDbContext _context;
        private readonly CodeGenerator _codeGenerator;

        public TourService(ApplicationDbContext context, CodeGenerator codeGenerator)
        {
            _context = context;
            _codeGenerator = codeGenerator;
        }

        public async Task<PagedResult<TourResponse>> GetToursAsync(string search, int pageIndex, int pageSize)
        {
            var query = _context.Tours
                .Include(t => t.DiemDen)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.TenTour.Contains(search));

            var total = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TourResponse
                {
                    MaTour = t.MaTour,
                    MaCodeTour = t.MaCodeTour,
                    TenTour = t.TenTour,
                    MaDiemDen = t.MaDiemDen,
                    TenDiemDen = t.DiemDen != null ? t.DiemDen.TenDiemDen : null,
                    MoTa = t.MoTa,
                    SoNgay = t.SoNgay,
                    GiaCoBan = t.GiaCoBan,
                    TrangThai = t.TrangThai,
                    NgayTao = t.NgayTao,
                    HinhAnh = t.HinhAnh // Thêm dòng này
                })
                .ToListAsync();

            return new PagedResult<TourResponse>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = total,
                Items = items
            };
        }

        public async Task<TourDetailResponse> GetTourByIdAsync(int id)
        {
            var tour = await _context.Tours
                .Include(t => t.DiemDen)
                .Include(t => t.LichKhoiHanhs)
                .Include(t => t.ChiTietLichTrinhs)
                .FirstOrDefaultAsync(t => t.MaTour == id);

            if (tour == null) return null;

            return new TourDetailResponse
            {
                MaTour = tour.MaTour,
                MaCodeTour = tour.MaCodeTour,
                TenTour = tour.TenTour,
                MaDiemDen = tour.MaDiemDen,
                TenDiemDen = tour.DiemDen?.TenDiemDen,
                MoTa = tour.MoTa,
                SoNgay = tour.SoNgay,
                GiaCoBan = tour.GiaCoBan,
                TrangThai = tour.TrangThai,
                NgayTao = tour.NgayTao,
                HinhAnh = tour.HinhAnh, // Thêm dòng này
                LichKhoiHanhs = tour.LichKhoiHanhs.Select(l => new ScheduleResponse
                {
                    MaLich = l.MaLich,
                    MaTour = l.MaTour ?? 0,
                    NgayKhoiHanh = l.NgayKhoiHanh,
                    NgayKetThuc = l.NgayKetThuc,
                    SoChoToiDa = l.SoChoToiDa,
                    SoChoConLai = l.SoChoConLai,
                    GiaTour = l.GiaTour,
                    TrangThai = l.TrangThai
                }).ToList(),
                ChiTietLichTrinhs = tour.ChiTietLichTrinhs.Select(ct => new ChiTietLichTrinhDto
                {
                    MaChiTiet = ct.MaChiTiet,
                    NgayThu = ct.NgayThu,
                    TieuDe = ct.TieuDe,
                    HoatDong = ct.HoatDong,
                    DiaDiem = ct.DiaDiem,
                    ThongTinBuaAn = ct.ThongTinBuaAn,
                    ThongTinKhachSan = ct.ThongTinKhachSan
                }).ToList()
            };
        }

        public async Task<TourResponse> CreateTourAsync(TourRequest request)
        {
            string hinhAnh = null;
            if (request.HinhAnhFile != null)
            {
                hinhAnh = await FileUploadHelper.SaveFileAsync(request.HinhAnhFile, "tours");
            }

            var tour = new Tour
            {
                MaCodeTour = _codeGenerator.GenerateTourCode(),
                TenTour = request.TenTour,
                MaDiemDen = request.MaDiemDen,
                MoTa = request.MoTa,
                SoNgay = request.SoNgay,
                GiaCoBan = request.GiaCoBan,
                TrangThai = request.TrangThai ?? "hien_an",
                NgayTao = DateTime.Now,
                HinhAnh = hinhAnh
            };

            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            return new TourResponse
            {
                MaTour = tour.MaTour,
                MaCodeTour = tour.MaCodeTour,
                TenTour = tour.TenTour,
                MaDiemDen = tour.MaDiemDen,
                MoTa = tour.MoTa,
                SoNgay = tour.SoNgay,
                GiaCoBan = tour.GiaCoBan,
                TrangThai = tour.TrangThai,
                NgayTao = tour.NgayTao,
                HinhAnh = tour.HinhAnh
            };
        }

        public async Task<TourResponse> UpdateTourAsync(int id, TourRequest request)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) throw new Exception("Không tìm thấy tour.");

            // Xử lý ảnh
            if (request.HinhAnhFile != null)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(tour.HinhAnh))
                    FileUploadHelper.DeleteFile(tour.HinhAnh);

                // Lưu ảnh mới
                tour.HinhAnh = await FileUploadHelper.SaveFileAsync(request.HinhAnhFile, "tours");
            }

            tour.TenTour = request.TenTour;
            tour.MaDiemDen = request.MaDiemDen;
            tour.MoTa = request.MoTa;
            tour.SoNgay = request.SoNgay;
            tour.GiaCoBan = request.GiaCoBan;
            tour.TrangThai = request.TrangThai ?? tour.TrangThai;

            await _context.SaveChangesAsync();

            return new TourResponse
            {
                MaTour = tour.MaTour,
                MaCodeTour = tour.MaCodeTour,
                TenTour = tour.TenTour,
                MaDiemDen = tour.MaDiemDen,
                MoTa = tour.MoTa,
                SoNgay = tour.SoNgay,
                GiaCoBan = tour.GiaCoBan,
                TrangThai = tour.TrangThai,
                NgayTao = tour.NgayTao,
                HinhAnh = tour.HinhAnh
            };
        }

        public async Task<bool> DeleteTourAsync(int id)
        {
            var tour = await _context.Tours
                .Include(t => t.LichKhoiHanhs)
                .FirstOrDefaultAsync(t => t.MaTour == id);

            if (tour == null) return false;

            if (tour.LichKhoiHanhs.Any())
                throw new InvalidOperationException("Không thể xóa tour vì đã có lịch khởi hành.");

            // Xóa ảnh
            if (!string.IsNullOrEmpty(tour.HinhAnh))
                FileUploadHelper.DeleteFile(tour.HinhAnh);

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}