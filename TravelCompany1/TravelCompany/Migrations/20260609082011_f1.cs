using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Migrations
{
    /// <inheritdoc />
    public partial class f1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiemDen",
                columns: table => new
                {
                    MaDiemDen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiemDen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    QuocGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThanhPho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDen", x => x.MaDiemDen);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiKhach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "moi"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKhachHang);
                    table.CheckConstraint("CK_KhachHang_GioiTinh", "GioiTinh IN ('Nam','Nữ','Khác')");
                    table.CheckConstraint("CK_KhachHang_LoaiKhach", "LoaiKhach IN ('moi','than_thiet','vip')");
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "nhan_vien"),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "hoat_dong"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.MaTaiKhoan);
                    table.CheckConstraint("CK_TaiKhoan_TrangThai", "TrangThai IN ('hoat_dong','khoa')");
                    table.CheckConstraint("CK_TaiKhoan_VaiTro", "VaiTro IN ('quan_tri','quan_ly','nhan_vien','ke_toan')");
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    MaTour = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCodeTour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenTour = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MaDiemDen = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoNgay = table.Column<int>(type: "int", nullable: false),
                    GiaCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "dang_mo"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.MaTour);
                    table.CheckConstraint("CK_Tour_TrangThai", "TrangThai IN ('dang_mo','tam_ngung','da_huy')");
                    table.ForeignKey(
                        name: "FK_Tour_DiemDen_MaDiemDen",
                        column: x => x.MaDiemDen,
                        principalTable: "DiemDen",
                        principalColumn: "MaDiemDen",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietLichTrinh",
                columns: table => new
                {
                    MaChiTiet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTour = table.Column<int>(type: "int", nullable: false),
                    NgayThu = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HoatDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThongTinBuaAn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ThongTinKhachSan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietLichTrinh", x => x.MaChiTiet);
                    table.ForeignKey(
                        name: "FK_ChiTietLichTrinh_Tour_MaTour",
                        column: x => x.MaTour,
                        principalTable: "Tour",
                        principalColumn: "MaTour",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichKhoiHanh",
                columns: table => new
                {
                    MaLich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTour = table.Column<int>(type: "int", nullable: false),
                    NgayKhoiHanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoChoToiDa = table.Column<int>(type: "int", nullable: false),
                    SoChoConLai = table.Column<int>(type: "int", nullable: false),
                    GiaTour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "con_cho")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichKhoiHanh", x => x.MaLich);
                    table.CheckConstraint("CK_LichKhoiHanh_TrangThai", "TrangThai IN ('con_cho','het_cho','da_khoi_hanh','da_huy')");
                    table.ForeignKey(
                        name: "FK_LichKhoiHanh_Tour_MaTour",
                        column: x => x.MaTour,
                        principalTable: "Tour",
                        principalColumn: "MaTour",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatTour",
                columns: table => new
                {
                    MaDatTour = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCodeDat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    MaLich = table.Column<int>(type: "int", nullable: false),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongNguoi = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienDatCoc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhiHuy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "cho_xac_nhan"),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatTour", x => x.MaDatTour);
                    table.CheckConstraint("CK_DatTour_TrangThai", "TrangThai IN ('cho_xac_nhan','da_xac_nhan','da_dat_coc','da_thanh_toan','da_huy')");
                    table.ForeignKey(
                        name: "FK_DatTour_KhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DatTour_LichKhoiHanh_MaLich",
                        column: x => x.MaLich,
                        principalTable: "LichKhoiHanh",
                        principalColumn: "MaLich",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DatTour_TaiKhoan_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DanhGiaTour",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatTour = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    SoSao = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGiaTour", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "FK_DanhGiaTour_DatTour_MaDatTour",
                        column: x => x.MaDatTour,
                        principalTable: "DatTour",
                        principalColumn: "MaDatTour",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DanhGiaTour_KhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCodeHoaDon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaDatTour = table.Column<int>(type: "int", nullable: false),
                    NgayXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DaThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "chua_thanh_toan")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHoaDon);
                    table.CheckConstraint("CK_HoaDon_TrangThai", "TrangThai IN ('chua_thanh_toan','thanh_toan_mot_phan','da_thanh_toan','da_huy')");
                    table.ForeignKey(
                        name: "FK_HoaDon_DatTour_MaDatTour",
                        column: x => x.MaDatTour,
                        principalTable: "DatTour",
                        principalColumn: "MaDatTour",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuDatTour",
                columns: table => new
                {
                    MaLichSu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatTour = table.Column<int>(type: "int", nullable: false),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true),
                    HanhDong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrangThaiCu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrangThaiMoi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuDatTour", x => x.MaLichSu);
                    table.ForeignKey(
                        name: "FK_LichSuDatTour_DatTour_MaDatTour",
                        column: x => x.MaDatTour,
                        principalTable: "DatTour",
                        principalColumn: "MaDatTour",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichSuDatTour_TaiKhoan_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDiTour",
                columns: table => new
                {
                    MaNguoiDi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatTour = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoCCCD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDiTour", x => x.MaNguoiDi);
                    table.ForeignKey(
                        name: "FK_NguoiDiTour_DatTour_MaDatTour",
                        column: x => x.MaDatTour,
                        principalTable: "DatTour",
                        principalColumn: "MaDatTour",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    MaThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatTour = table.Column<int>(type: "int", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoaiThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhuongThuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "thanh_cong"),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.MaThanhToan);
                    table.CheckConstraint("CK_ThanhToan_LoaiThanhToan", "LoaiThanhToan IN ('dat_coc','thanh_toan_day_du','thanh_toan_con_lai','hoan_tien')");
                    table.CheckConstraint("CK_ThanhToan_PhuongThuc", "PhuongThuc IN ('tien_mat','chuyen_khoan','the','vi_dien_tu')");
                    table.CheckConstraint("CK_ThanhToan_TrangThai", "TrangThai IN ('thanh_cong','that_bai','cho_xu_ly')");
                    table.ForeignKey(
                        name: "FK_ThanhToan_DatTour_MaDatTour",
                        column: x => x.MaDatTour,
                        principalTable: "DatTour",
                        principalColumn: "MaDatTour",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietLichTrinh_MaTour",
                table: "ChiTietLichTrinh",
                column: "MaTour");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaTour_MaDatTour",
                table: "DanhGiaTour",
                column: "MaDatTour");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaTour_MaKhachHang",
                table: "DanhGiaTour",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DatTour_MaCodeDat",
                table: "DatTour",
                column: "MaCodeDat",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatTour_MaKhachHang",
                table: "DatTour",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DatTour_MaLich",
                table: "DatTour",
                column: "MaLich");

            migrationBuilder.CreateIndex(
                name: "IX_DatTour_MaTaiKhoan",
                table: "DatTour",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaCodeHoaDon",
                table: "HoaDon",
                column: "MaCodeHoaDon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaDatTour",
                table: "HoaDon",
                column: "MaDatTour",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichKhoiHanh_MaTour",
                table: "LichKhoiHanh",
                column: "MaTour");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuDatTour_MaDatTour",
                table: "LichSuDatTour",
                column: "MaDatTour");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuDatTour_MaTaiKhoan",
                table: "LichSuDatTour",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDiTour_MaDatTour",
                table: "NguoiDiTour",
                column: "MaDatTour");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_Email",
                table: "TaiKhoan",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_MaDatTour",
                table: "ThanhToan",
                column: "MaDatTour");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_MaCodeTour",
                table: "Tour",
                column: "MaCodeTour",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tour_MaDiemDen",
                table: "Tour",
                column: "MaDiemDen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietLichTrinh");

            migrationBuilder.DropTable(
                name: "DanhGiaTour");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "LichSuDatTour");

            migrationBuilder.DropTable(
                name: "NguoiDiTour");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "DatTour");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "LichKhoiHanh");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "DiemDen");
        }
    }
}
