using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSanConModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "GioBatDau",
                table: "DatSan",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "GioKetThuc",
                table: "DatSan",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "MaSanCon",
                table: "DatSan",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "DatSan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "ThoiLuong",
                table: "DatSan",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "TongTien",
                table: "DatSan",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "SanCon",
                columns: table => new
                {
                    MaSanCon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSan = table.Column<int>(type: "int", nullable: false),
                    TenSanCon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanCon", x => x.MaSanCon);
                    table.ForeignKey(
                        name: "FK_SanCon_SanBong_MaSan",
                        column: x => x.MaSan,
                        principalTable: "SanBong",
                        principalColumn: "MaSan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SanCon",
                columns: new[] { "MaSanCon", "MaSan", "SucChua", "TenSanCon", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, 4, "Sân PB1", "Hoat dong" },
                    { 2, 1, 4, "Sân PB2", "Hoat dong" },
                    { 3, 2, 4, "Sân PB1", "Hoat dong" },
                    { 4, 2, 4, "Sân PB2", "Hoat dong" },
                    { 5, 3, 10, "Sân 5 số 1", "Hoat dong" },
                    { 6, 4, 4, "Sân PB1", "Hoat dong" },
                    { 7, 4, 4, "Sân PB2", "Hoat dong" },
                    { 8, 5, 4, "Sân PB1", "Hoat dong" },
                    { 9, 6, 10, "Sân 5 số 1", "Hoat dong" },
                    { 10, 6, 10, "Sân 5 số 2", "Hoat dong" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatSan_MaSanCon",
                table: "DatSan",
                column: "MaSanCon");

            migrationBuilder.CreateIndex(
                name: "IX_SanCon_MaSan",
                table: "SanCon",
                column: "MaSan");

            migrationBuilder.AddForeignKey(
                name: "FK_DatSan_SanCon_MaSanCon",
                table: "DatSan",
                column: "MaSanCon",
                principalTable: "SanCon",
                principalColumn: "MaSanCon",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatSan_SanCon_MaSanCon",
                table: "DatSan");

            migrationBuilder.DropTable(
                name: "SanCon");

            migrationBuilder.DropIndex(
                name: "IX_DatSan_MaSanCon",
                table: "DatSan");

            migrationBuilder.DropColumn(
                name: "GioBatDau",
                table: "DatSan");

            migrationBuilder.DropColumn(
                name: "GioKetThuc",
                table: "DatSan");

            migrationBuilder.DropColumn(
                name: "MaSanCon",
                table: "DatSan");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "DatSan");

            migrationBuilder.DropColumn(
                name: "ThoiLuong",
                table: "DatSan");

            migrationBuilder.DropColumn(
                name: "TongTien",
                table: "DatSan");
        }
    }
}
