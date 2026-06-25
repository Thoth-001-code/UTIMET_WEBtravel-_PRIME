using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDuLich.Migrations
{
    /// <inheritdoc />
    public partial class f4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "Tour",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "DiemDen",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "DiemDen");
        }
    }
}
