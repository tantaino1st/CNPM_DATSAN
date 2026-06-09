using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSanBongCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 1,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.849399999999999, 106.75369999999999 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 2,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.7415, 106.63420000000001 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 3,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.7356, 106.661 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 4,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 21.0123, 105.8284 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 5,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.8307, 106.82040000000001 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 6,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.8032, 106.6433 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 7,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 21.036200000000001, 105.7906 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 8,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.791499999999999, 106.63420000000001 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 9,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.7828, 106.69580000000001 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 10,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.8203, 106.67149999999999 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 11,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 21.037800000000001, 105.8156 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 12,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.785399999999999, 106.6631 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 13,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 16.0748, 108.2435 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 14,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 21.020299999999999, 105.76390000000001 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 15,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.754200000000001, 106.6635 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 16,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.776899999999999, 106.69540000000001 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 17,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.0342, 105.78279999999999 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 18,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.776400000000001, 106.6698 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 19,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 20.844899999999999, 106.68810000000001 });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 20,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 10.980399999999999, 106.6519 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 1,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 2,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 3,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 4,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 5,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 6,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 7,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 8,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 9,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 10,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 11,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 12,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 13,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 14,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 15,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 16,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 17,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 18,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 19,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 20,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { null, null });
        }
    }
}
