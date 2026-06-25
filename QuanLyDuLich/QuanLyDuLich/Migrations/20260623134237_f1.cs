using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDuLich.Migrations
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
                    TenDiemDen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    QuocGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThanhPho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDen", x => x.MaDiemDen);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaNhanVien = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LoaiNhanVien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.MaTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    MaTour = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCodeTour = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenTour = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaDiemDen = table.Column<int>(type: "int", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNgay = table.Column<int>(type: "int", nullable: true),
                    GiaCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.MaTour);
                    table.ForeignKey(
                        name: "FK_Tour_DiemDen_MaDiemDen",
                        column: x => x.MaDiemDen,
                        principalTable: "DiemDen",
                        principalColumn: "MaDiemDen",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoaiKhach = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "FK_KhachHang_TaiKhoan_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietLichTrinh",
                columns: table => new
                {
                    MaChiTiet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTour = table.Column<int>(type: "int", nullable: true),
                    NgayThu = table.Column<int>(type: "int", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HoatDong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThongTinBuaAn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThongTinKhachSan = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    MaTour = table.Column<int>(type: "int", nullable: true),
                    NgayKhoiHanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoChoToiDa = table.Column<int>(type: "int", nullable: true),
                    SoChoConLai = table.Column<int>(type: "int", nullable: true),
                    GiaTour = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichKhoiHanh", x => x.MaLich);
                    table.ForeignKey(
                        name: "FK_LichKhoiHanh_Tour_MaTour",
                        column: x => x.MaTour,
                        principalTable: "Tour",
                        principalColumn: "MaTour",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatTour",
                columns: table => new
                {
                    MaDatTour = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCodeDat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: true),
                    MaLich = table.Column<int>(type: "int", nullable: true),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongNguoi = table.Column<int>(type: "int", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TienDatCoc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TienConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PhiHuy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatTour", x => x.MaDatTour);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DanhGiaTour",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatTour = table.Column<int>(type: "int", nullable: true),
                    MaKhachHang = table.Column<int>(type: "int", nullable: true),
                    SoSao = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    MaCodeHoaDon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaDatTour = table.Column<int>(type: "int", nullable: true),
                    NgayXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DaThanhToan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDon_DatTour_MaDatTour",
                        column: x => x.MaDatTour,
                        principalTable: "DatTour",
                        principalColumn: "MaDatTour",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichSuDatTour",
                columns: table => new
                {
                    MaLichSu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatTour = table.Column<int>(type: "int", nullable: true),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true),
                    HanhDong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThaiCu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThaiMoi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDiTour",
                columns: table => new
                {
                    MaNguoiDi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatTour = table.Column<int>(type: "int", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoCCCD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
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
                    MaDatTour = table.Column<int>(type: "int", nullable: true),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoaiThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhuongThuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHoaDon = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.MaThanhToan);
                    table.ForeignKey(
                        name: "FK_ThanhToan_DatTour_MaDatTour",
                        column: x => x.MaDatTour,
                        principalTable: "DatTour",
                        principalColumn: "MaDatTour",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThanhToan_HoaDon_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "MaHoaDon");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietLichTrinh_MaTour",
                table: "ChiTietLichTrinh",
                column: "MaTour");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaTour_MaDatTour",
                table: "DanhGiaTour",
                column: "MaDatTour",
                unique: true,
                filter: "[MaDatTour] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaTour_MaKhachHang",
                table: "DanhGiaTour",
                column: "MaKhachHang");

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
                name: "IX_HoaDon_MaDatTour",
                table: "HoaDon",
                column: "MaDatTour",
                unique: true,
                filter: "[MaDatTour] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_MaTaiKhoan",
                table: "KhachHang",
                column: "MaTaiKhoan",
                unique: true,
                filter: "[MaTaiKhoan] IS NOT NULL");

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
                name: "IX_ThanhToan_MaDatTour",
                table: "ThanhToan",
                column: "MaDatTour");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_MaHoaDon",
                table: "ThanhToan",
                column: "MaHoaDon");

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
                name: "LichSuDatTour");

            migrationBuilder.DropTable(
                name: "NguoiDiTour");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "HoaDon");

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
