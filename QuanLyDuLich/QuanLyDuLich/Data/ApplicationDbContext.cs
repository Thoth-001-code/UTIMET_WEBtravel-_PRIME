using Microsoft.EntityFrameworkCore;
using QuanLyDuLich.Models.Entities;

namespace QuanLyDuLich.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<DiemDen> DiemDens { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<LichKhoiHanh> LichKhoiHanhs { get; set; }
        public DbSet<ChiTietLichTrinh> ChiTietLichTrinhs { get; set; }
        public DbSet<DatTour> DatTours { get; set; }
        public DbSet<NguoiDiTour> NguoiDiTours { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<LichSuDatTour> LichSuDatTours { get; set; }
        public DbSet<DanhGiaTour> DanhGiaTours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ
            modelBuilder.Entity<KhachHang>()
                .HasOne(kh => kh.TaiKhoan)
                .WithOne(tk => tk.KhachHang)
                .HasForeignKey<KhachHang>(kh => kh.MaTaiKhoan)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DatTour>()
                .HasOne(dt => dt.KhachHang)
                .WithMany(kh => kh.DatTours)
                .HasForeignKey(dt => dt.MaKhachHang)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DatTour>()
                .HasOne(dt => dt.LichKhoiHanh)
                .WithMany(lkh => lkh.DatTours)
                .HasForeignKey(dt => dt.MaLich)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DatTour>()
                .HasOne(dt => dt.TaiKhoan)
                .WithMany(tk => tk.DatTours)
                .HasForeignKey(dt => dt.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.DiemDen)
                .WithMany(dd => dd.Tours)
                .HasForeignKey(t => t.MaDiemDen)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LichKhoiHanh>()
                .HasOne(lkh => lkh.Tour)
                .WithMany(t => t.LichKhoiHanhs)
                .HasForeignKey(lkh => lkh.MaTour)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChiTietLichTrinh>()
                .HasOne(ct => ct.Tour)
                .WithMany(t => t.ChiTietLichTrinhs)
                .HasForeignKey(ct => ct.MaTour)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NguoiDiTour>()
                .HasOne(ndt => ndt.DatTour)
                .WithMany(dt => dt.NguoiDiTours)
                .HasForeignKey(ndt => ndt.MaDatTour)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThanhToan>()
                .HasOne(tt => tt.DatTour)
                .WithMany(dt => dt.ThanhToans)
                .HasForeignKey(tt => tt.MaDatTour)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HoaDon>()
                .HasOne(hd => hd.DatTour)
                .WithOne(dt => dt.HoaDon)
                .HasForeignKey<HoaDon>(hd => hd.MaDatTour)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LichSuDatTour>()
                .HasOne(ls => ls.DatTour)
                .WithMany(dt => dt.LichSuDatTours)
                .HasForeignKey(ls => ls.MaDatTour)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LichSuDatTour>()
                .HasOne(ls => ls.TaiKhoan)
                .WithMany(tk => tk.LichSuDatTours)
                .HasForeignKey(ls => ls.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGiaTour>()
                .HasOne(dg => dg.DatTour)
                .WithOne(dt => dt.DanhGiaTour)
                .HasForeignKey<DanhGiaTour>(dg => dg.MaDatTour)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGiaTour>()
                .HasOne(dg => dg.KhachHang)
                .WithMany(kh => kh.DanhGiaTours)
                .HasForeignKey(dg => dg.MaKhachHang)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}