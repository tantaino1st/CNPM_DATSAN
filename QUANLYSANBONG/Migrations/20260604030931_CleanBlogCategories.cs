using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class CleanBlogCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BlogCategory",
                keyColumn: "Id",
                keyValue: 1);

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
                columns: new[] { "NgayDang", "NoiBat" },
                values: new object[] { new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4235), true });

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
                columns: new[] { "MoTaNgan", "NgayDang", "Slug", "TieuDe" },
                values: new object[] { "Cách ứng dụng công nghệ QR Code để quản lý lượt vào sân chuyên nghiệp.", new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4243), "huong-dan-checkin-qr-code", "Hướng dẫn Check-in Sân Bóng bằng QR Code" });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 9,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 9, 30, 837, DateTimeKind.Local).AddTicks(4245));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BlogCategory",
                columns: new[] { "Id", "MoTa", "Slug", "TenDanhMuc", "TrangThai" },
                values: new object[] { 1, "Kiến thức về phần mềm quản lý", "phan-mem", "Phần mềm", true });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9053));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NgayDang", "NoiBat" },
                values: new object[] { new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9056), false });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9058));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 5,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MoTaNgan", "NgayDang", "Slug", "TieuDe" },
                values: new object[] { "Hướng dẫn sử dụng QR Code để checkin sân bóng chuyên nghiệp.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9066), "phan-mem-checkin-qr-code", "Phần Mềm Checkin Sân Bóng QR Code" });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 9,
                column: "NgayDang",
                value: new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9068));

            migrationBuilder.InsertData(
                table: "BlogPost",
                columns: new[] { "Id", "AnhDaiDien", "BlogCategoryId", "LuotXem", "MoTaNgan", "NgayDang", "NguoiTaoId", "NoiBat", "NoiDung", "Slug", "ThoiGianDoc", "TieuDe", "TrangThai" },
                values: new object[,]
                {
                    { 1, null, 1, 0, "So sánh chi tiết các phần mềm quản lý sân bóng phổ biến nhất hiện nay.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9050), null, true, "Nội dung bài viết đang được cập nhật...", "top-5-phan-mem-quan-ly-san-bong-da-2025", "10 phút đọc", "TOP 5 Phần Mềm Quản Lý Sân Bóng Đá Tốt Nhất 2025", true },
                    { 6, null, 1, 0, "Bảng giá chi tiết các gói phần mềm quản lý sân thể thao.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9062), null, false, "Nội dung bài viết đang được cập nhật...", "gia-phan-mem-quan-ly-san-bong-2026", "6 phút đọc", "Giá Phần Mềm Quản Lý Sân Bóng 2026", true },
                    { 7, null, 1, 0, "Có thật sự dùng được không? Những rủi ro cần lưu ý.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9064), null, false, "Nội dung bài viết đang được cập nhật...", "phan-mem-quan-ly-san-bong-mien-phi", "12 phút đọc", "Phần Mềm Quản Lý Sân Bóng Miễn Phí", true }
                });
        }
    }
}
