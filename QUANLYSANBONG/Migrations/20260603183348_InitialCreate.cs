using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QUANLYSANBONG.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BangGiaKhungGio",
                columns: table => new
                {
                    MaBangGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayTrongTuan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GioBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuThuCaoDiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangGiaKhungGio", x => x.MaBangGia);
                });

            migrationBuilder.CreateTable(
                name: "DichVu",
                columns: table => new
                {
                    MaDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TonKho = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVu", x => x.MaDichVu);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NhomKhach = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSan",
                columns: table => new
                {
                    MaLoaiSan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiSan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSan", x => x.MaLoaiSan);
                });

            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    MaVaiTro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.MaVaiTro);
                });

            migrationBuilder.CreateTable(
                name: "SanBong",
                columns: table => new
                {
                    MaSan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TinhThanh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoLuongSanCon = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaLoaiSan = table.Column<int>(type: "int", nullable: false),
                    GiaMacDinh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanBong", x => x.MaSan);
                    table.ForeignKey(
                        name: "FK_SanBong_LoaiSan_MaLoaiSan",
                        column: x => x.MaLoaiSan,
                        principalTable: "LoaiSan",
                        principalColumn: "MaLoaiSan",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaVaiTro = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_NguoiDung_VaiTro_MaVaiTro",
                        column: x => x.MaVaiTro,
                        principalTable: "VaiTro",
                        principalColumn: "MaVaiTro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatSan",
                columns: table => new
                {
                    MaDatSan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaSan = table.Column<int>(type: "int", nullable: false),
                    MaBangGia = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TienCoc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatSan", x => x.MaDatSan);
                    table.ForeignKey(
                        name: "FK_DatSan_BangGiaKhungGio_MaBangGia",
                        column: x => x.MaBangGia,
                        principalTable: "BangGiaKhungGio",
                        principalColumn: "MaBangGia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DatSan_KhachHang_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DatSan_SanBong_MaSan",
                        column: x => x.MaSan,
                        principalTable: "SanBong",
                        principalColumn: "MaSan",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NhanVien_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyThaoTac",
                columns: table => new
                {
                    MaNhatKy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: true),
                    HanhDong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyThaoTac", x => x.MaNhatKy);
                    table.ForeignKey(
                        name: "FK_NhatKyThaoTac_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDichVu",
                columns: table => new
                {
                    MaCTDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatSan = table.Column<int>(type: "int", nullable: false),
                    MaDichVu = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDichVu", x => x.MaCTDV);
                    table.ForeignKey(
                        name: "FK_ChiTietDichVu_DatSan_MaDatSan",
                        column: x => x.MaDatSan,
                        principalTable: "DatSan",
                        principalColumn: "MaDatSan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDichVu_DichVu_MaDichVu",
                        column: x => x.MaDichVu,
                        principalTable: "DichVu",
                        principalColumn: "MaDichVu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDatSan = table.Column<int>(type: "int", nullable: false),
                    TienSan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TienDichVu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiamGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrangThaiThanhToan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_HoaDon_DatSan_MaDatSan",
                        column: x => x.MaDatSan,
                        principalTable: "DatSan",
                        principalColumn: "MaDatSan",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BangGiaKhungGio",
                columns: new[] { "MaBangGia", "DonGia", "GioBatDau", "GioKetThuc", "NgayTrongTuan", "PhuThuCaoDiem" },
                values: new object[,]
                {
                    { 1, 200000m, new TimeSpan(0, 6, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), "Thu 2 - Thu 6", 0m },
                    { 2, 300000m, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 19, 0, 0, 0), "Thu 2 - Thu 6", 50000m },
                    { 3, 350000m, new TimeSpan(0, 7, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), "Thu 7 - Chu nhat", 50000m }
                });

            migrationBuilder.InsertData(
                table: "DichVu",
                columns: new[] { "MaDichVu", "DonGia", "DonViTinh", "TenDichVu", "TonKho" },
                values: new object[,]
                {
                    { 1, 10000m, "Chai", "Nuoc suoi", 100 },
                    { 2, 30000m, "Qua", "Thue bong", 10 },
                    { 3, 50000m, "Bo", "Thue ao bib", 20 }
                });

            migrationBuilder.InsertData(
                table: "KhachHang",
                columns: new[] { "MaKH", "GhiChu", "HoTen", "NhomKhach", "SoDienThoai" },
                values: new object[,]
                {
                    { 1, "Hay dat buoi toi", "Nguyen Van An", "Khach quen", "0901234567" },
                    { 2, null, "Tran Minh Binh", "Khach moi", "0912345678" }
                });

            migrationBuilder.InsertData(
                table: "LoaiSan",
                columns: new[] { "MaLoaiSan", "Icon", "MoTa", "TenLoaiSan" },
                values: new object[,]
                {
                    { 1, "fa-futbol", "Sân bóng đá", "Bóng đá" },
                    { 2, "fa-volleyball", "Sân bóng chuyền", "Bóng chuyền" },
                    { 3, "fa-basketball", "Sân bóng rổ", "Bóng rổ" },
                    { 4, "fa-table-tennis-paddle-ball", "Bàn bóng bàn", "Bóng bàn" },
                    { 5, "fa-baseball", "Sân tennis", "Quần vợt" },
                    { 6, "fa-table-tennis-paddle-ball", "Sân cầu lông", "Cầu lông" },
                    { 7, "fa-medal", "Sân Pickleball", "Pickleball" }
                });

            migrationBuilder.InsertData(
                table: "VaiTro",
                columns: new[] { "MaVaiTro", "MoTa", "TenVaiTro" },
                values: new object[,]
                {
                    { 1, "Quan tri he thong", "Admin" },
                    { 2, "Quan ly san bong", "Quan ly" },
                    { 3, "Nhan vien dat san va dich vu", "Nhan vien" },
                    { 4, "Thanh toan va bao cao", "Ke toan" },
                    { 5, "Khach hang dat san", "Khach hang" }
                });

            migrationBuilder.InsertData(
                table: "NguoiDung",
                columns: new[] { "MaNguoiDung", "MaVaiTro", "MatKhau", "TenDangNhap", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, "123456", "admin", true },
                    { 2, 3, "123456", "nhanvien", true },
                    { 3, 4, "123456", "ketoan", true }
                });

            migrationBuilder.InsertData(
                table: "SanBong",
                columns: new[] { "MaSan", "DiaChi", "GiaMacDinh", "HinhAnh", "MaLoaiSan", "SoLuongSanCon", "TenSan", "TinhThanh", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Thủ Đức, Hồ Chí Minh", 250000m, "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", 7, 2, "002 PB Club", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 2, "Quận 8, Hồ Chí Minh", 200000m, "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", 7, 2, "ST PB Club Q8", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 3, "Quận 8, Hồ Chí Minh", 220000m, "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", 1, 1, "9196 Sport Q8", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 4, "Đống Đa, Hà Nội", 300000m, "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", 7, 2, "ACE PB Club", "Hà Nội", "Hoat dong" },
                    { 5, "Thủ Đức, Hồ Chí Minh", 200000m, "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=2070&auto=format&fit=crop", 7, 1, "Apec PB Q9", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 6, "Tân Bình, Hồ Chí Minh", 300000m, "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", 1, 5, "Sân Bóng Chảo Lửa", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 7, "Cầu Giấy, Hà Nội", 400000m, "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", 5, 3, "Tennis Phượng Hoàng", "Hà Nội", "Hoat dong" },
                    { 8, "Tân Phú, Hồ Chí Minh", 150000m, "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", 6, 8, "Nhà thi đấu Cầu Lông Tân Phú", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 9, "Quận 1, Hồ Chí Minh", 250000m, "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", 3, 2, "Sân Bóng Rổ Thanh Niên", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 10, "Gò Vấp, Hồ Chí Minh", 200000m, "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", 1, 4, "Sân Cỏ Nhân Tạo K34", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 11, "Ba Đình, Hà Nội", 350000m, "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", 5, 2, "Sân Tennis Hoàng Hoa Thám", "Hà Nội", "Hoat dong" },
                    { 12, "Tân Bình, Hồ Chí Minh", 160000m, "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", 6, 6, "Cầu Lông Lê Thị Riêng", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 13, "Sơn Trà, Đà Nẵng", 300000m, "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", 2, 1, "Bóng Chuyền Bãi Biển", "Đà Nẵng", "Hoat dong" },
                    { 14, "Nam Từ Liêm, Hà Nội", 5000000m, "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=2070&auto=format&fit=crop", 1, 1, "Sân Vận Động Mỹ Đình", "Hà Nội", "Hoat dong" },
                    { 15, "Quận 5, Hồ Chí Minh", 100000m, "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", 4, 10, "CLB Bóng Bàn Quận 5", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 16, "Quận 1, Hồ Chí Minh", 220000m, "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", 3, 2, "Bóng Rổ Yết Kiêu", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 17, "Ninh Kiều, Cần Thơ", 200000m, "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", 1, 3, "Sân Bóng Đá Cần Thơ", "Cần Thơ", "Hoat dong" },
                    { 18, "Quận 10, Hồ Chí Minh", 250000m, "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", 7, 4, "Pickleball Vạn Hạnh", "TP. Hồ Chí Minh", "Hoat dong" },
                    { 19, "Lê Chân, Hải Phòng", 180000m, "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=2070&auto=format&fit=crop", 1, 2, "Sân Bóng Hải Phòng", "Hải Phòng", "Hoat dong" },
                    { 20, "Thủ Dầu Một, Bình Dương", 120000m, "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", 6, 5, "Cầu Lông Bình Dương", "Bình Dương", "Hoat dong" }
                });

            migrationBuilder.InsertData(
                table: "NhanVien",
                columns: new[] { "MaNV", "ChucVu", "Email", "HoTen", "MaNguoiDung", "SoDienThoai" },
                values: new object[,]
                {
                    { 1, "Admin", "admin@sanbong.local", "Quan tri vien", 1, "0900000000" },
                    { 2, "Nhan vien", "nhanvien@sanbong.local", "Nhan vien san", 2, "0911111111" },
                    { 3, "Ke toan", "ketoan@sanbong.local", "Nhan vien ke toan", 3, "0922222222" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDichVu_MaDatSan",
                table: "ChiTietDichVu",
                column: "MaDatSan");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDichVu_MaDichVu",
                table: "ChiTietDichVu",
                column: "MaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_DatSan_MaBangGia",
                table: "DatSan",
                column: "MaBangGia");

            migrationBuilder.CreateIndex(
                name: "IX_DatSan_MaKH",
                table: "DatSan",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_DatSan_MaSan_MaBangGia_NgayDat",
                table: "DatSan",
                columns: new[] { "MaSan", "MaBangGia", "NgayDat" },
                unique: true,
                filter: "[TrangThai] <> N'Da huy'");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaDatSan",
                table: "HoaDon",
                column: "MaDatSan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaVaiTro",
                table: "NguoiDung",
                column: "MaVaiTro");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_TenDangNhap",
                table: "NguoiDung",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaNguoiDung",
                table: "NhanVien",
                column: "MaNguoiDung",
                unique: true,
                filter: "[MaNguoiDung] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyThaoTac_MaNguoiDung",
                table: "NhatKyThaoTac",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_SanBong_MaLoaiSan",
                table: "SanBong",
                column: "MaLoaiSan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDichVu");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "NhatKyThaoTac");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "DatSan");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "BangGiaKhungGio");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "SanBong");

            migrationBuilder.DropTable(
                name: "VaiTro");

            migrationBuilder.DropTable(
                name: "LoaiSan");
        }
    }
}
