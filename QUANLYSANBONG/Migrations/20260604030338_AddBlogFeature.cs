using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MoTaNgan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BlogCategoryId = table.Column<int>(type: "int", nullable: false),
                    NgayDang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianDoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LuotXem = table.Column<int>(type: "int", nullable: false),
                    NoiBat = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    NguoiTaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPost_BlogCategory_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPost_NguoiDung_NguoiTaoId",
                        column: x => x.NguoiTaoId,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung");
                });

            migrationBuilder.InsertData(
                table: "BlogCategory",
                columns: new[] { "Id", "MoTa", "Slug", "TenDanhMuc", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Kiến thức về phần mềm quản lý", "phan-mem", "Phần mềm", true },
                    { 2, "Kinh nghiệm kinh doanh sân thể thao", "kinh-doanh-san", "Kinh doanh sân", true },
                    { 3, "Cập nhật xu hướng thể thao mới nhất", "xu-huong", "Xu hướng", true },
                    { 4, "Hướng dẫn cho chủ sân và người chơi", "huong-dan", "Hướng dẫn", true }
                });

            migrationBuilder.InsertData(
                table: "BlogPost",
                columns: new[] { "Id", "AnhDaiDien", "BlogCategoryId", "LuotXem", "MoTaNgan", "NgayDang", "NguoiTaoId", "NoiBat", "NoiDung", "Slug", "ThoiGianDoc", "TieuDe", "TrangThai" },
                values: new object[,]
                {
                    { 1, null, 1, 0, "So sánh chi tiết các phần mềm quản lý sân bóng phổ biến nhất hiện nay.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9050), null, true, "Nội dung bài viết đang được cập nhật...", "top-5-phan-mem-quan-ly-san-bong-da-2025", "10 phút đọc", "TOP 5 Phần Mềm Quản Lý Sân Bóng Đá Tốt Nhất 2025", true },
                    { 2, null, 2, 0, "Chi phí, vốn và dự toán doanh thu khi kinh doanh sân Pickleball.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9053), null, true, "Nội dung bài viết đang được cập nhật...", "huong-dan-mo-san-pickleball", "15 phút đọc", "Hướng Dẫn Mở Sân Pickleball Từ A đến Z", true },
                    { 3, null, 2, 0, "Tăng doanh thu 40% trong 3 tháng với các bí quyết quản lý sau.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9056), null, false, "Nội dung bài viết đang được cập nhật...", "10-cach-quan-ly-san-bong-hieu-qua", "8 phút đọc", "10 Cách Quản Lý Sân Bóng Hiệu Quả", true },
                    { 4, null, 3, 0, "Cơ hội vàng cho các chủ đầu tư trong năm 2025.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9058), null, false, "Nội dung bài viết đang được cập nhật...", "xu-huong-san-pickleball-2025", "5 phút đọc", "Xu Hướng Sân Pickleball Việt Nam 2025", true },
                    { 5, null, 2, 0, "Bí quyết tối đa hóa doanh thu cho cơ sở của bạn.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9060), null, false, "Nội dung bài viết đang được cập nhật...", "cach-tinh-gia-thue-san-bong-da", "7 phút đọc", "Cách Tính Giá Thuê Sân Bóng Đá Theo Giờ", true },
                    { 6, null, 1, 0, "Bảng giá chi tiết các gói phần mềm quản lý sân thể thao.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9062), null, false, "Nội dung bài viết đang được cập nhật...", "gia-phan-mem-quan-ly-san-bong-2026", "6 phút đọc", "Giá Phần Mềm Quản Lý Sân Bóng 2026", true },
                    { 7, null, 1, 0, "Có thật sự dùng được không? Những rủi ro cần lưu ý.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9064), null, false, "Nội dung bài viết đang được cập nhật...", "phan-mem-quan-ly-san-bong-mien-phi", "12 phút đọc", "Phần Mềm Quản Lý Sân Bóng Miễn Phí", true },
                    { 8, null, 4, 0, "Hướng dẫn sử dụng QR Code để checkin sân bóng chuyên nghiệp.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9066), null, false, "Nội dung bài viết đang được cập nhật...", "phan-mem-checkin-qr-code", "4 phút đọc", "Phần Mềm Checkin Sân Bóng QR Code", true },
                    { 9, null, 2, 0, "Bảng tính chi tiết vốn đầu tư và thời gian thu hồi vốn.", new DateTime(2026, 6, 4, 10, 3, 37, 664, DateTimeKind.Local).AddTicks(9068), null, false, "Nội dung bài viết đang được cập nhật...", "mo-san-bong-da-mini-bao-nhieu-von", "20 phút đọc", "Mở Sân Bóng Đá Mini Cần Bao Nhiêu Vốn 2026?", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_BlogCategoryId",
                table: "BlogPost",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_NguoiTaoId",
                table: "BlogPost",
                column: "NguoiTaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPost");

            migrationBuilder.DropTable(
                name: "BlogCategory");
        }
    }
}
