using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class AddCoordinatesToSanBong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "SanBong",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "SanBong",
                type: "float",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "SanBong");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "SanBong");
        }
    }
}
