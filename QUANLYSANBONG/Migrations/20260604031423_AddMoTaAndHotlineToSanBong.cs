using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class AddMoTaAndHotlineToSanBong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "SanBong",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "SanBong",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 14, 22, 712, DateTimeKind.Local).AddTicks(6163));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 14, 22, 712, DateTimeKind.Local).AddTicks(6167));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 14, 22, 712, DateTimeKind.Local).AddTicks(6170));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 5,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 14, 22, 712, DateTimeKind.Local).AddTicks(6173));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 8,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 14, 22, 712, DateTimeKind.Local).AddTicks(6175));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 9,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 14, 22, 712, DateTimeKind.Local).AddTicks(6177));

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 1,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 2,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 3,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 4,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 5,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 6,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 7,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 8,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 9,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 10,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 11,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 12,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 13,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 14,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 15,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 16,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 17,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 18,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 19,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SanBong",
                keyColumn: "MaSan",
                keyValue: 20,
                columns: new[] { "MoTa", "SoDienThoai" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "SanBong");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "SanBong");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4232));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4235));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4238));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 5,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4240));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 8,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4243));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 9,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4245));
        }
    }
}
