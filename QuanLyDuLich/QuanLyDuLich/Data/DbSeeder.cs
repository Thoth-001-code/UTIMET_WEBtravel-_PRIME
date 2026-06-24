using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Helpers;
using QuanLyDuLich.Models.Entities;

namespace QuanLyDuLich.Data
{
    public static class DbSeeder
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            // Apply any pending migrations
            await context.Database.MigrateAsync();

            // Seed accounts if they don't exist
            if (!context.TaiKhoans.Any())
            {
                // 1. Admin account
                var adminAccount = new TaiKhoan
                {
                    HoTen = "Administrator",
                    Email = "admin@example.com",
                    MatKhau = PasswordHelper.HashPassword("Admin@123"),
                    VaiTro = "quan_tri",
                    TrangThai = "hoat_dong",
                    NgayTao = DateTime.Now
                };
                context.TaiKhoans.Add(adminAccount);

                // 2. Manager account
                var managerAccount = new TaiKhoan
                {
                    HoTen = "Manager User",
                    Email = "manager@example.com",
                    MatKhau = PasswordHelper.HashPassword("Manager@123"),
                    VaiTro = "quan_ly",
                    TrangThai = "hoat_dong",
                    NgayTao = DateTime.Now
                };
                context.TaiKhoans.Add(managerAccount);

                // 3. Staff account
                var staffAccount = new TaiKhoan
                {
                    HoTen = "Staff User",
                    Email = "staff@example.com",
                    MatKhau = PasswordHelper.HashPassword("Staff@123"),
                    VaiTro = "nhan_vien",
                    TrangThai = "hoat_dong",
                    MaNhanVien = "NV001",
                    LoaiNhanVien = "ban_hang",
                    NgayTao = DateTime.Now
                };
                context.TaiKhoans.Add(staffAccount);

                // 4. Customer account 1
                var customerAccount1 = new TaiKhoan
                {
                    HoTen = "Nguyen Van A",
                    Email = "customer1@example.com",
                    MatKhau = PasswordHelper.HashPassword("Customer@123"),
                    VaiTro = "khach_hang",
                    TrangThai = "hoat_dong",
                    NgayTao = DateTime.Now
                };
                context.TaiKhoans.Add(customerAccount1);

                // 5. Customer account 2
                var customerAccount2 = new TaiKhoan
                {
                    HoTen = "Tran Thi B",
                    Email = "customer2@example.com",
                    MatKhau = PasswordHelper.HashPassword("Customer@123"),
                    VaiTro = "khach_hang",
                    TrangThai = "hoat_dong",
                    NgayTao = DateTime.Now
                };
                context.TaiKhoans.Add(customerAccount2);

                await context.SaveChangesAsync();

                // Now create corresponding customer records
                var customer1 = new KhachHang
                {
                    HoTen = "Nguyen Van A",
                    SoDienThoai = "0901234567",
                    Email = "customer1@example.com",
                    GioiTinh = "Nam",
                    NgaySinh = new DateTime(1990, 1, 1),
                    DiaChi = "123 Đường ABC, Quận 1, TP.HCM",
                    LoaiKhach = "thuong",
                    MaTaiKhoan = customerAccount1.MaTaiKhoan,
                    NgayTao = DateTime.Now
                };
                context.KhachHangs.Add(customer1);

                var customer2 = new KhachHang
                {
                    HoTen = "Tran Thi B",
                    SoDienThoai = "0912345678",
                    Email = "customer2@example.com",
                    GioiTinh = "Nu",
                    NgaySinh = new DateTime(1995, 5, 15),
                    DiaChi = "456 Đường DEF, Quận 3, TP.HCM",
                    LoaiKhach = "thuong",
                    MaTaiKhoan = customerAccount2.MaTaiKhoan,
                    NgayTao = DateTime.Now
                };
                context.KhachHangs.Add(customer2);

                await context.SaveChangesAsync();

               
                
                        
                    
                
            }
        }
    }
}
