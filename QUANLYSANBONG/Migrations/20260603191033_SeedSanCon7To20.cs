using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class SeedSanCon7To20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SanCon",
                columns: new[] { "MaSanCon", "MaSan", "SucChua", "TenSanCon", "TrangThai" },
                values: new object[,]
                {
                    { 11, 7, 4, "Sân Tennis 1", "Hoat dong" },
                    { 12, 7, 4, "Sân Tennis 2", "Hoat dong" },
                    { 13, 8, 4, "Sân Cầu Lông 1", "Hoat dong" },
                    { 14, 8, 4, "Sân Cầu Lông 2", "Hoat dong" },
                    { 15, 9, 10, "Sân Bóng Rổ 1", "Hoat dong" },
                    { 16, 10, 10, "Sân 5 số 1", "Hoat dong" },
                    { 17, 11, 4, "Sân Tennis 1", "Hoat dong" },
                    { 18, 12, 4, "Sân Cầu Lông 1", "Hoat dong" },
                    { 19, 13, 12, "Sân Bóng Chuyền 1", "Hoat dong" },
                    { 20, 14, 22, "Sân 11", "Hoat dong" },
                    { 21, 15, 2, "Bàn 1", "Hoat dong" },
                    { 22, 16, 10, "Sân Bóng Rổ 1", "Hoat dong" },
                    { 23, 17, 10, "Sân 5 số 1", "Hoat dong" },
                    { 24, 18, 4, "Sân PB1", "Hoat dong" },
                    { 25, 19, 10, "Sân 5 số 1", "Hoat dong" },
                    { 26, 20, 4, "Sân Cầu Lông 1", "Hoat dong" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SanCon",
                keyColumn: "MaSanCon",
                keyValue: 26);
        }
    }
}
