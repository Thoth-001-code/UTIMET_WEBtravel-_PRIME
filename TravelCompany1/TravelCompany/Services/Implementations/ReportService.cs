using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TravelCompany.API.Data;
using TravelCompany.API.DTOs.Report;
using TravelCompany.API.Services.Interfaces;

namespace TravelCompany.API.Services.Implementations;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ReportService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        // Cấu hình license cho EPPlus (phi thương mại)
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public async Task<IEnumerable<DoanhThuTheoTourDto>> GetRevenueByTourAsync(DateTime? fromDate, DateTime? toDate)
    {
        var query = _context.DatTours
            .Include(b => b.LichKhoiHanh).ThenInclude(l => l.Tour)
            .Where(b => b.TrangThai == "da_thanh_toan" || b.TrangThai == "da_xac_nhan");

        if (fromDate.HasValue)
            query = query.Where(b => b.NgayDat >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(b => b.NgayDat <= toDate.Value);

        var result = await query
            .GroupBy(b => new { b.LichKhoiHanh.Tour.MaTour, b.LichKhoiHanh.Tour.TenTour })
            .Select(g => new DoanhThuTheoTourDto
            {
                MaTour = g.Key.MaTour,
                TenTour = g.Key.TenTour,
                SoLuongBooking = g.Count(),
                TongSoKhach = g.Sum(b => b.SoLuongNguoi),
                TongDoanhThu = g.Sum(b => b.TongTien)
            })
            .OrderByDescending(g => g.TongDoanhThu)
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<DoanhThuTheoThangDto>> GetRevenueByMonthAsync(int year)
    {
        var result = await _context.DatTours
            .Where(b => (b.TrangThai == "da_thanh_toan" || b.TrangThai == "da_xac_nhan") && b.NgayDat.Year == year)
            .GroupBy(b => new { b.NgayDat.Month, b.NgayDat.Year })
            .Select(g => new DoanhThuTheoThangDto
            {
                Thang = g.Key.Month,
                Nam = g.Key.Year,
                SoLuongBooking = g.Count(),
                TongDoanhThu = g.Sum(b => b.TongTien)
            })
            .OrderBy(g => g.Thang)
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<TopKhachHangDto>> GetTopCustomersAsync(int top)
    {
        var result = await _context.KhachHangs
            .Select(k => new TopKhachHangDto
            {
                MaKhachHang = k.MaKhachHang,
                HoTen = k.HoTen,
                SoDienThoai = k.SoDienThoai,
                Email = k.Email,
                SoLuongBooking = k.DatTours.Count(b => b.TrangThai == "da_thanh_toan" || b.TrangThai == "da_xac_nhan"),
                TongChiTieu = k.DatTours.Where(b => b.TrangThai == "da_thanh_toan" || b.TrangThai == "da_xac_nhan").Sum(b => b.TongTien)
            })
            .OrderByDescending(k => k.TongChiTieu)
            .Take(top)
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<ThongKeTrangThaiBookingDto>> GetBookingStatusStatisticsAsync()
    {
        var result = await _context.DatTours
            .GroupBy(b => b.TrangThai)
            .Select(g => new ThongKeTrangThaiBookingDto
            {
                TrangThai = g.Key,
                SoLuong = g.Count()
            })
            .ToListAsync();

        return result;
    }

    public async Task<byte[]> ExportRevenueByTourExcelAsync(DateTime? fromDate, DateTime? toDate)
    {
        var data = await GetRevenueByTourAsync(fromDate, toDate);
        var list = data.ToList();

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("DoanhThuTheoTour");

        // Header
        worksheet.Cells[1, 1].Value = "Mã Tour";
        worksheet.Cells[1, 2].Value = "Tên Tour";
        worksheet.Cells[1, 3].Value = "Số lượng booking";
        worksheet.Cells[1, 4].Value = "Tổng số khách";
        worksheet.Cells[1, 5].Value = "Tổng doanh thu (VND)";

        using (var range = worksheet.Cells[1, 1, 1, 5])
        {
            range.Style.Font.Bold = true;
            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        }

        // Data rows
        int row = 2;
        foreach (var item in list)
        {
            worksheet.Cells[row, 1].Value = item.MaTour;
            worksheet.Cells[row, 2].Value = item.TenTour;
            worksheet.Cells[row, 3].Value = item.SoLuongBooking;
            worksheet.Cells[row, 4].Value = item.TongSoKhach;
            worksheet.Cells[row, 5].Value = item.TongDoanhThu;
            worksheet.Cells[row, 5].Style.Numberformat.Format = "#,##0";
            row++;
        }

        worksheet.Cells.AutoFitColumns();
        return await package.GetAsByteArrayAsync();
    }
}