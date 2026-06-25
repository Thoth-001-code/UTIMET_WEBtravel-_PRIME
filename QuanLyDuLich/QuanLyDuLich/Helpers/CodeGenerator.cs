using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Data;

namespace QuanLyDuLich.Helpers
{
    public class CodeGenerator
    {
        private readonly ApplicationDbContext _context;

        public CodeGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tạo mã cho Tour: TOUR-YYMMDD-XXX
        public string GenerateTourCode()
        {
            string prefix = "TOUR";
            return GenerateCode(prefix, _context.Tours, t => t.MaCodeTour);
        }

        // Tạo mã cho Đặt tour: BOOK-YYMMDD-XXX
        public string GenerateBookingCode()
        {
            string prefix = "BOOK";
            return GenerateCode(prefix, _context.DatTours, d => d.MaCodeDat);
        }

        // Tạo mã cho Hóa đơn: HOA-YYMMDD-XXX
        public string GenerateInvoiceCode()
        {
            string prefix = "HOA";
            return GenerateCode(prefix, _context.HoaDons, h => h.MaCodeHoaDon);
        }

        private string GenerateCode<T>(string prefix, DbSet<T> dbSet, Func<T, string> codeSelector) where T : class
        {
            string datePart = DateTime.Now.ToString("yyMMdd");
            string prefixFull = $"{prefix}-{datePart}-";

            // Lấy tất cả mã bắt đầu với prefixFull và tìm số lớn nhất
            var codes = dbSet.AsEnumerable()
                             .Select(codeSelector)
                             .Where(c => c.StartsWith(prefixFull))
                             .ToList();

            int maxNumber = 0;
            foreach (var code in codes)
            {
                string numberPart = code.Substring(prefixFull.Length);
                if (int.TryParse(numberPart, out int num))
                {
                    if (num > maxNumber) maxNumber = num;
                }
            }

            int nextNumber = maxNumber + 1;
            return $"{prefixFull}{nextNumber:D3}";
        }
    }
}