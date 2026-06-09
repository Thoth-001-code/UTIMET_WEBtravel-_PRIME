using Microsoft.EntityFrameworkCore;
using TravelCompany.API.Models.Entities;

namespace TravelCompany.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaiKhoan> TaiKhoans { get; set; }
    public DbSet<KhachHang> KhachHangs { get; set; }
    public DbSet<DiemDen> DiemDens { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<LichKhoiHanh> LichKhoiHanhs { get; set; }
    public DbSet<ChiTietLichTrinh> ChiTietLichTrinhs { get; set; }
    public DbSet<AnhTour> AnhTours { get; set; }
    public DbSet<DatTour> DatTours { get; set; }
    public DbSet<NguoiDiTour> NguoiDiTours { get; set; }
    public DbSet<ThanhToan> ThanhToans { get; set; }
    public DbSet<HoaDon> HoaDons { get; set; }
    public DbSet<LichSuDatTour> LichSuDatTours { get; set; }
    public DbSet<DanhGiaTour> DanhGiaTours { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===== TaiKhoan =====
        modelBuilder.Entity<TaiKhoan>(e =>
        {
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.VaiTro).HasDefaultValue("nhan_vien");
            e.Property(x => x.TrangThai).HasDefaultValue("hoat_dong");
            e.ToTable(t => t.HasCheckConstraint("CK_TaiKhoan_VaiTro", "VaiTro IN ('quan_tri','quan_ly','nhan_vien','ke_toan')"));
            e.ToTable(t => t.HasCheckConstraint("CK_TaiKhoan_TrangThai", "TrangThai IN ('hoat_dong','khoa')"));
        });

        // ===== KhachHang =====
        modelBuilder.Entity<KhachHang>(e =>
        {
            e.Property(x => x.LoaiKhach).HasDefaultValue("moi");
            e.ToTable(t => t.HasCheckConstraint("CK_KhachHang_GioiTinh", "GioiTinh IN ('Nam','Nữ','Khác')"));
            e.ToTable(t => t.HasCheckConstraint("CK_KhachHang_LoaiKhach", "LoaiKhach IN ('moi','than_thiet','vip')"));
        });

        // ===== Tour =====
        modelBuilder.Entity<Tour>(e =>
        {
            e.HasIndex(x => x.MaCodeTour).IsUnique();
            e.Property(x => x.TrangThai).HasDefaultValue("dang_mo");
            e.ToTable(t => t.HasCheckConstraint("CK_Tour_TrangThai", "TrangThai IN ('dang_mo','tam_ngung','da_huy')"));
            e.HasOne(x => x.DiemDen).WithMany(x => x.Tours).HasForeignKey(x => x.MaDiemDen).OnDelete(DeleteBehavior.SetNull);
        });

        // ===== LichKhoiHanh =====
        modelBuilder.Entity<LichKhoiHanh>(e =>
        {
            e.Property(x => x.TrangThai).HasDefaultValue("con_cho");
            e.ToTable(t => t.HasCheckConstraint("CK_LichKhoiHanh_TrangThai", "TrangThai IN ('con_cho','het_cho','da_khoi_hanh','da_huy')"));
            e.HasOne(x => x.Tour).WithMany(x => x.LichKhoiHanhs).HasForeignKey(x => x.MaTour).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== ChiTietLichTrinh =====
        modelBuilder.Entity<ChiTietLichTrinh>(e =>
        {
            e.HasOne(x => x.Tour).WithMany(x => x.ChiTietLichTrinhs).HasForeignKey(x => x.MaTour).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== AnhTour =====
        modelBuilder.Entity<AnhTour>(e =>
        {
            e.HasOne(x => x.Tour).WithMany(x => x.AnhTours).HasForeignKey(x => x.MaTour).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== DatTour =====
        modelBuilder.Entity<DatTour>(e =>
        {
            e.HasIndex(x => x.MaCodeDat).IsUnique();
            e.Property(x => x.TrangThai).HasDefaultValue("cho_xac_nhan");
            e.ToTable(t => t.HasCheckConstraint("CK_DatTour_TrangThai", "TrangThai IN ('cho_xac_nhan','da_xac_nhan','da_dat_coc','da_thanh_toan','da_huy')"));
            e.HasOne(x => x.KhachHang).WithMany(x => x.DatTours).HasForeignKey(x => x.MaKhachHang).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.LichKhoiHanh).WithMany(x => x.DatTours).HasForeignKey(x => x.MaLich).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.TaiKhoan).WithMany(x => x.DatTours).HasForeignKey(x => x.MaTaiKhoan).OnDelete(DeleteBehavior.SetNull);
        });

        // ===== NguoiDiTour =====
        modelBuilder.Entity<NguoiDiTour>(e =>
        {
            e.HasOne(x => x.DatTour).WithMany(x => x.NguoiDiTours).HasForeignKey(x => x.MaDatTour).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== ThanhToan =====
        modelBuilder.Entity<ThanhToan>(e =>
        {
            e.Property(x => x.TrangThai).HasDefaultValue("thanh_cong");
            e.ToTable(t => t.HasCheckConstraint("CK_ThanhToan_LoaiThanhToan", "LoaiThanhToan IN ('dat_coc','thanh_toan_day_du','thanh_toan_con_lai','hoan_tien')"));
            e.ToTable(t => t.HasCheckConstraint("CK_ThanhToan_PhuongThuc", "PhuongThuc IN ('tien_mat','chuyen_khoan','the','vi_dien_tu')"));
            e.ToTable(t => t.HasCheckConstraint("CK_ThanhToan_TrangThai", "TrangThai IN ('thanh_cong','that_bai','cho_xu_ly')"));
            e.HasOne(x => x.DatTour).WithMany(x => x.ThanhToans).HasForeignKey(x => x.MaDatTour).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== HoaDon =====
        modelBuilder.Entity<HoaDon>(e =>
        {
            e.HasIndex(x => x.MaCodeHoaDon).IsUnique();
            e.Property(x => x.TrangThai).HasDefaultValue("chua_thanh_toan");
            e.ToTable(t => t.HasCheckConstraint("CK_HoaDon_TrangThai", "TrangThai IN ('chua_thanh_toan','thanh_toan_mot_phan','da_thanh_toan','da_huy')"));
            e.HasOne(x => x.DatTour).WithOne(x => x.HoaDon).HasForeignKey<HoaDon>(x => x.MaDatTour).OnDelete(DeleteBehavior.Cascade);
        });

        // ===== LichSuDatTour =====
        modelBuilder.Entity<LichSuDatTour>(e =>
        {
            e.HasOne(x => x.DatTour).WithMany(x => x.LichSuDatTours).HasForeignKey(x => x.MaDatTour).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.TaiKhoan).WithMany(x => x.LichSuDatTours).HasForeignKey(x => x.MaTaiKhoan).OnDelete(DeleteBehavior.SetNull);
        });

        // ===== DanhGiaTour =====
        modelBuilder.Entity<DanhGiaTour>(e =>
        {
            e.HasOne(x => x.DatTour).WithMany(x => x.DanhGiaTours).HasForeignKey(x => x.MaDatTour).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.KhachHang).WithMany(x => x.DanhGiaTours).HasForeignKey(x => x.MaKhachHang).OnDelete(DeleteBehavior.Restrict);
        });
    }
}