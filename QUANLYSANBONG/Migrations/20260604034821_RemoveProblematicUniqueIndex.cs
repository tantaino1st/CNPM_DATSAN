using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProblematicUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DatSan_MaSan_MaBangGia_NgayDat",
                table: "DatSan");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 48, 21, 289, DateTimeKind.Local).AddTicks(5355));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 48, 21, 289, DateTimeKind.Local).AddTicks(5358));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 48, 21, 289, DateTimeKind.Local).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 5,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 48, 21, 289, DateTimeKind.Local).AddTicks(5364));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 8,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 48, 21, 289, DateTimeKind.Local).AddTicks(5366));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 9,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 48, 21, 289, DateTimeKind.Local).AddTicks(5369));

            migrationBuilder.CreateIndex(
                name: "IX_DatSan_MaSan",
                table: "DatSan",
                column: "MaSan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DatSan_MaSan",
                table: "DatSan");

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

            migrationBuilder.CreateIndex(
                name: "IX_DatSan_MaSan_MaBangGia_NgayDat",
                table: "DatSan",
                columns: new[] { "MaSan", "MaBangGia", "NgayDat" },
                unique: true,
                filter: "[TrangThai] <> N'Da huy'");
        }
    }
}
