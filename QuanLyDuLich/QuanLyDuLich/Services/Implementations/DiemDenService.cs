using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;
using QuanLyDuLich.Helpers;
using QuanLyDuLich.Models.DTOs.DiemDen;
using QuanLyDuLich.Models.Entities;
using QuanLyDuLich.Services.Interfaces;

namespace QuanLyDuLich.Services
{
    public class DiemDenService : IDiemDenService
    {
        private readonly ApplicationDbContext _context;

        public DiemDenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DiemDenResponse>> GetAllAsync()
        {
            return await _context.DiemDens
                .Select(d => new DiemDenResponse
                {
                    MaDiemDen = d.MaDiemDen,
                    TenDiemDen = d.TenDiemDen,
                    QuocGia = d.QuocGia,
                    ThanhPho = d.ThanhPho,
                    MoTa = d.MoTa,
                    HinhAnh = d.HinhAnh
                })
                .ToListAsync();
        }

        public async Task<DiemDenResponse> GetByIdAsync(int id)
        {
            var diemDen = await _context.DiemDens.FindAsync(id);
            if (diemDen == null) return null;

            return new DiemDenResponse
            {
                MaDiemDen = diemDen.MaDiemDen,
                TenDiemDen = diemDen.TenDiemDen,
                QuocGia = diemDen.QuocGia,
                ThanhPho = diemDen.ThanhPho,
                MoTa = diemDen.MoTa,
                HinhAnh = diemDen.HinhAnh
            };
        }

        public async Task<DiemDenResponse> CreateAsync(DiemDenRequest request)
        {
            string hinhAnh = null;
            if (request.HinhAnhFile != null)
            {
                hinhAnh = await FileUploadHelper.SaveFileAsync(request.HinhAnhFile, "diadiem");
            }

            var entity = new DiemDen
            {
                TenDiemDen = request.TenDiemDen,
                QuocGia = request.QuocGia,
                ThanhPho = request.ThanhPho,
                MoTa = request.MoTa,
                HinhAnh = hinhAnh
            };

            _context.DiemDens.Add(entity);
            await _context.SaveChangesAsync();

            return new DiemDenResponse
            {
                MaDiemDen = entity.MaDiemDen,
                TenDiemDen = entity.TenDiemDen,
                QuocGia = entity.QuocGia,
                ThanhPho = entity.ThanhPho,
                MoTa = entity.MoTa,
                HinhAnh = entity.HinhAnh
            };
        }

        public async Task<DiemDenResponse> UpdateAsync(int id, DiemDenRequest request)
        {
            var entity = await _context.DiemDens.FindAsync(id);
            if (entity == null) throw new Exception("Không tìm thấy điểm đến.");

            if (request.HinhAnhFile != null)
            {
                if (!string.IsNullOrEmpty(entity.HinhAnh))
                    FileUploadHelper.DeleteFile(entity.HinhAnh);

                entity.HinhAnh = await FileUploadHelper.SaveFileAsync(request.HinhAnhFile, "diadiem");
            }

            entity.TenDiemDen = request.TenDiemDen;
            entity.QuocGia = request.QuocGia;
            entity.ThanhPho = request.ThanhPho;
            entity.MoTa = request.MoTa;

            await _context.SaveChangesAsync();

            return new DiemDenResponse
            {
                MaDiemDen = entity.MaDiemDen,
                TenDiemDen = entity.TenDiemDen,
                QuocGia = entity.QuocGia,
                ThanhPho = entity.ThanhPho,
                MoTa = entity.MoTa,
                HinhAnh = entity.HinhAnh
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DiemDens.FindAsync(id);
            if (entity == null) return false;

            var hasTour = await _context.Tours.AnyAsync(t => t.MaDiemDen == id);
            if (hasTour)
                throw new InvalidOperationException("Không thể xóa điểm đến vì đã có tour sử dụng.");

            if (!string.IsNullOrEmpty(entity.HinhAnh))
                FileUploadHelper.DeleteFile(entity.HinhAnh);

            _context.DiemDens.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}