using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemSettingsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "NguoiDung",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "NguoiDung",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "NguoiDung",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CaiDatThongBao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThongBao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DangBat = table.Column<bool>(type: "bit", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaiDatThongBao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GioHoatDong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Thu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false),
                    GioMoCua = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioDongCua = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHoatDong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinSan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinSan", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CaiDatThongBao",
                columns: new[] { "Id", "DangBat", "Icon", "MoTa", "TenThongBao" },
                values: new object[,]
                {
                    { 1, true, "fas fa-plus-circle", "Nhận thông báo khi có khách đặt sân", "Đặt lịch mới" },
                    { 2, true, "fas fa-times-circle", "Nhận thông báo khi khách hủy lịch đặt", "Hủy lịch" },
                    { 3, true, "fas fa-check-double", "Nhận thông báo xác nhận thanh toán", "Thanh toán" },
                    { 4, true, "fas fa-file-invoice-dollar", "Nhận tóm tắt doanh thu cuối ngày", "Báo cáo ngày" },
                    { 5, true, "fas fa-tools", "Nhắc nhở lịch bảo trì định kỳ", "Nhắc bảo trì sân" }
                });

            migrationBuilder.InsertData(
                table: "GioHoatDong",
                columns: new[] { "Id", "DangHoatDong", "GioDongCua", "GioMoCua", "Thu" },
                values: new object[,]
                {
                    { 1, true, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Thứ 2" },
                    { 2, true, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Thứ 3" },
                    { 3, true, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Thứ 4" },
                    { 4, true, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Thứ 5" },
                    { 5, true, new TimeSpan(0, 22, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Thứ 6" },
                    { 6, true, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Thứ 7" },
                    { 7, true, new TimeSpan(0, 23, 0, 0, 0), new TimeSpan(0, 6, 0, 0, 0), "Chủ Nhật" }
                });

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "MaNguoiDung",
                keyValue: 1,
                columns: new[] { "Email", "HoTen", "SoDienThoai" },
                values: new object[] { "admin@sanbong.vn", "Quản trị viên", "0888888888" });

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "MaNguoiDung",
                keyValue: 2,
                columns: new[] { "Email", "HoTen", "SoDienThoai" },
                values: new object[] { "nhanvien@sanbong.vn", "Nhân viên", "0888111222" });

            migrationBuilder.UpdateData(
                table: "NguoiDung",
                keyColumn: "MaNguoiDung",
                keyValue: 3,
                columns: new[] { "Email", "HoTen", "SoDienThoai" },
                values: new object[] { "ketoan@sanbong.vn", "Kế toán", "0888333444" });

            migrationBuilder.InsertData(
                table: "ThongTinSan",
                columns: new[] { "Id", "DiaChi", "Email", "LogoUrl", "MoTa", "SoDienThoai", "TenSan", "Website" },
                values: new object[] { 1, "Linh Trung, Thu Duc, TP.HCM", "contact@sanbong.vn", null, "He thong quan ly san bong chuyen nghiep", "0888888888", "SanBong.vn", "www.sanbong.vn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaiDatThongBao");

            migrationBuilder.DropTable(
                name: "GioHoatDong");

            migrationBuilder.DropTable(
                name: "ThongTinSan");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "NguoiDung");
        }
    }
}
