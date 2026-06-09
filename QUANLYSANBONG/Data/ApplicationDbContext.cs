using Microsoft.EntityFrameworkCore;
using QUANLYSANBONG.Models;

namespace QUANLYSANBONG.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<VaiTro> VaiTros => Set<VaiTro>();
    public DbSet<NguoiDung> NguoiDungs => Set<NguoiDung>();
    public DbSet<NhanVien> NhanViens => Set<NhanVien>();
    public DbSet<KhachHang> KhachHangs => Set<KhachHang>();
    public DbSet<LoaiSan> LoaiSans => Set<LoaiSan>();
    public DbSet<SanBong> SanBongs => Set<SanBong>();
    public DbSet<BangGiaKhungGio> BangGiaKhungGios => Set<BangGiaKhungGio>();
    public DbSet<DatSan> DatSans => Set<DatSan>();
    public DbSet<DichVu> DichVus => Set<DichVu>();
    public DbSet<ChiTietDichVu> ChiTietDichVus => Set<ChiTietDichVu>();
    public DbSet<HoaDon> HoaDons => Set<HoaDon>();
    public DbSet<NhatKyThaoTac> NhatKyThaoTacs => Set<NhatKyThaoTac>();
    public DbSet<ThongTinSan> ThongTinSans => Set<ThongTinSan>();
    public DbSet<GioHoatDong> GioHoatDongs => Set<GioHoatDong>();
    public DbSet<CaiDatThongBao> CaiDatThongBaos => Set<CaiDatThongBao>();
    public DbSet<BlogCategory> BlogCategories => Set<BlogCategory>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VaiTro>().ToTable("VaiTro");
        modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
        modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
        modelBuilder.Entity<KhachHang>().ToTable("KhachHang");
        modelBuilder.Entity<LoaiSan>().ToTable("LoaiSan");
        modelBuilder.Entity<SanBong>().ToTable("SanBong");
        modelBuilder.Entity<BangGiaKhungGio>().ToTable("BangGiaKhungGio");
        modelBuilder.Entity<DatSan>().ToTable("DatSan");
        modelBuilder.Entity<DichVu>().ToTable("DichVu");
        modelBuilder.Entity<ChiTietDichVu>().ToTable("ChiTietDichVu");
        modelBuilder.Entity<HoaDon>().ToTable("HoaDon");
        modelBuilder.Entity<NhatKyThaoTac>().ToTable("NhatKyThaoTac");
        modelBuilder.Entity<ThongTinSan>().ToTable("ThongTinSan");
        modelBuilder.Entity<GioHoatDong>().ToTable("GioHoatDong");
        modelBuilder.Entity<CaiDatThongBao>().ToTable("CaiDatThongBao");
        modelBuilder.Entity<BlogCategory>().ToTable("BlogCategory");
        modelBuilder.Entity<BlogPost>().ToTable("BlogPost");

        modelBuilder.Entity<NguoiDung>()
            .HasIndex(x => x.TenDangNhap)
            .IsUnique();

        modelBuilder.Entity<DatSan>();

        modelBuilder.Entity<HoaDon>()
            .HasIndex(x => x.MaDatSan)
            .IsUnique();

        modelBuilder.Entity<NguoiDung>()
            .HasOne(x => x.VaiTro)
            .WithMany(x => x.NguoiDungs)
            .HasForeignKey(x => x.MaVaiTro)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<NhanVien>()
            .HasOne(x => x.NguoiDung)
            .WithOne(x => x.NhanVien)
            .HasForeignKey<NhanVien>(x => x.MaNguoiDung)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<SanBong>()
            .HasOne(x => x.LoaiSan)
            .WithMany(x => x.SanBongs)
            .HasForeignKey(x => x.MaLoaiSan)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DatSan>()
            .HasOne(x => x.KhachHang)
            .WithMany(x => x.DatSans)
            .HasForeignKey(x => x.MaKH)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DatSan>()
            .HasOne(x => x.SanBong)
            .WithMany(x => x.DatSans)
            .HasForeignKey(x => x.MaSan)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DatSan>()
            .HasOne(x => x.SanCon)
            .WithMany()
            .HasForeignKey(x => x.MaSanCon)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DatSan>()
            .HasOne(x => x.BangGiaKhungGio)
            .WithMany(x => x.DatSans)
            .HasForeignKey(x => x.MaBangGia)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChiTietDichVu>()
            .HasOne(x => x.DichVu)
            .WithMany(x => x.ChiTietDichVus)
            .HasForeignKey(x => x.MaDichVu)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HoaDon>()
            .HasOne(x => x.DatSan)
            .WithOne(x => x.HoaDon)
            .HasForeignKey<HoaDon>(x => x.MaDatSan)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<NhatKyThaoTac>()
            .HasOne(x => x.NguoiDung)
            .WithMany(x => x.NhatKyThaoTacs)
            .HasForeignKey(x => x.MaNguoiDung)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<SanBong>().Property(x => x.GiaMacDinh).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<BangGiaKhungGio>().Property(x => x.DonGia).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<BangGiaKhungGio>().Property(x => x.PhuThuCaoDiem).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<DatSan>().Property(x => x.TienCoc).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<DichVu>().Property(x => x.DonGia).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<ChiTietDichVu>().Property(x => x.DonGia).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<ChiTietDichVu>().Property(x => x.ThanhTien).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<HoaDon>().Property(x => x.TienSan).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<HoaDon>().Property(x => x.TienDichVu).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<HoaDon>().Property(x => x.GiamGia).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<HoaDon>().Property(x => x.PhuThu).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<HoaDon>().Property(x => x.TongTien).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<VaiTro>().HasData(
            new VaiTro { MaVaiTro = 1, TenVaiTro = "Admin", MoTa = "Quan tri he thong" },
            new VaiTro { MaVaiTro = 2, TenVaiTro = "Quan ly", MoTa = "Quan ly san bong" },
            new VaiTro { MaVaiTro = 3, TenVaiTro = "Nhan vien", MoTa = "Nhan vien dat san va dich vu" },
            new VaiTro { MaVaiTro = 4, TenVaiTro = "Ke toan", MoTa = "Thanh toan va bao cao" },
            new VaiTro { MaVaiTro = 5, TenVaiTro = "Khach hang", MoTa = "Khach hang dat san" }
        );

        modelBuilder.Entity<NguoiDung>().HasData(
            new NguoiDung { MaNguoiDung = 1, TenDangNhap = "admin", MatKhau = "123456", MaVaiTro = 1, TrangThai = true, HoTen = "Quản trị viên", Email = "admin@sanbong.vn", SoDienThoai = "0888888888" },
            new NguoiDung { MaNguoiDung = 2, TenDangNhap = "nhanvien", MatKhau = "123456", MaVaiTro = 3, TrangThai = true, HoTen = "Nhân viên", Email = "nhanvien@sanbong.vn", SoDienThoai = "0888111222" },
            new NguoiDung { MaNguoiDung = 3, TenDangNhap = "ketoan", MatKhau = "123456", MaVaiTro = 4, TrangThai = true, HoTen = "Kế toán", Email = "ketoan@sanbong.vn", SoDienThoai = "0888333444" }
        );

        modelBuilder.Entity<NhanVien>().HasData(
            new NhanVien { MaNV = 1, HoTen = "Quan tri vien", SoDienThoai = "0900000000", Email = "admin@sanbong.local", ChucVu = "Admin", MaNguoiDung = 1 },
            new NhanVien { MaNV = 2, HoTen = "Nhan vien san", SoDienThoai = "0911111111", Email = "nhanvien@sanbong.local", ChucVu = "Nhan vien", MaNguoiDung = 2 },
            new NhanVien { MaNV = 3, HoTen = "Nhan vien ke toan", SoDienThoai = "0922222222", Email = "ketoan@sanbong.local", ChucVu = "Ke toan", MaNguoiDung = 3 }
        );

        modelBuilder.Entity<LoaiSan>().HasData(
            new LoaiSan { MaLoaiSan = 1, TenLoaiSan = "Bóng đá", MoTa = "Sân bóng đá", Icon = "fa-futbol" },
            new LoaiSan { MaLoaiSan = 2, TenLoaiSan = "Bóng chuyền", MoTa = "Sân bóng chuyền", Icon = "fa-volleyball" },
            new LoaiSan { MaLoaiSan = 3, TenLoaiSan = "Bóng rổ", MoTa = "Sân bóng rổ", Icon = "fa-basketball" },
            new LoaiSan { MaLoaiSan = 4, TenLoaiSan = "Bóng bàn", MoTa = "Bàn bóng bàn", Icon = "fa-table-tennis-paddle-ball" },
            new LoaiSan { MaLoaiSan = 5, TenLoaiSan = "Quần vợt", MoTa = "Sân tennis", Icon = "fa-baseball" },
            new LoaiSan { MaLoaiSan = 6, TenLoaiSan = "Cầu lông", MoTa = "Sân cầu lông", Icon = "fa-table-tennis-paddle-ball" },
            new LoaiSan { MaLoaiSan = 7, TenLoaiSan = "Pickleball", MoTa = "Sân Pickleball", Icon = "fa-medal" }
        );

        modelBuilder.Entity<SanBong>().HasData(
            new SanBong { MaSan = 1, TenSan = "002 PB Club", DiaChi = "Thủ Đức, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 2, GiaMacDinh = 250000, MaLoaiSan = 7, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", Latitude = 10.8494, Longitude = 106.7537 },
            new SanBong { MaSan = 2, TenSan = "ST PB Club Q8", DiaChi = "Quận 8, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 2, GiaMacDinh = 200000, MaLoaiSan = 7, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", Latitude = 10.7415, Longitude = 106.6342 },
            new SanBong { MaSan = 3, TenSan = "9196 Sport Q8", DiaChi = "Quận 8, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 1, GiaMacDinh = 220000, MaLoaiSan = 1, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", Latitude = 10.7356, Longitude = 106.6610 },
            new SanBong { MaSan = 4, TenSan = "ACE PB Club", DiaChi = "Đống Đa, Hà Nội", TinhThanh = "Hà Nội", SoLuongSanCon = 2, GiaMacDinh = 300000, MaLoaiSan = 7, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", Latitude = 21.0123, Longitude = 105.8284 },
            new SanBong { MaSan = 5, TenSan = "Apec PB Q9", DiaChi = "Thủ Đức, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 1, GiaMacDinh = 200000, MaLoaiSan = 7, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=2070&auto=format&fit=crop", Latitude = 10.8307, Longitude = 106.8204 },
            new SanBong { MaSan = 6, TenSan = "Sân Bóng Chảo Lửa", DiaChi = "Tân Bình, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 5, GiaMacDinh = 300000, MaLoaiSan = 1, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", Latitude = 10.8032, Longitude = 106.6433 },
            new SanBong { MaSan = 7, TenSan = "Tennis Phượng Hoàng", DiaChi = "Cầu Giấy, Hà Nội", TinhThanh = "Hà Nội", SoLuongSanCon = 3, GiaMacDinh = 400000, MaLoaiSan = 5, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", Latitude = 21.0362, Longitude = 105.7906 },
            new SanBong { MaSan = 8, TenSan = "Nhà thi đấu Cầu Lông Tân Phú", DiaChi = "Tân Phú, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 8, GiaMacDinh = 150000, MaLoaiSan = 6, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", Latitude = 10.7915, Longitude = 106.6342 },
            new SanBong { MaSan = 9, TenSan = "Sân Bóng Rổ Thanh Niên", DiaChi = "Quận 1, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 2, GiaMacDinh = 250000, MaLoaiSan = 3, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", Latitude = 10.7828, Longitude = 106.6958 },
            new SanBong { MaSan = 10, TenSan = "Sân Cỏ Nhân Tạo K34", DiaChi = "Gò Vấp, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 4, GiaMacDinh = 200000, MaLoaiSan = 1, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", Latitude = 10.8203, Longitude = 106.6715 },
            new SanBong { MaSan = 11, TenSan = "Sân Tennis Hoàng Hoa Thám", DiaChi = "Ba Đình, Hà Nội", TinhThanh = "Hà Nội", SoLuongSanCon = 2, GiaMacDinh = 350000, MaLoaiSan = 5, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", Latitude = 21.0378, Longitude = 105.8156 },
            new SanBong { MaSan = 12, TenSan = "Cầu Lông Lê Thị Riêng", DiaChi = "Tân Bình, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 6, GiaMacDinh = 160000, MaLoaiSan = 6, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", Latitude = 10.7854, Longitude = 106.6631 },
            new SanBong { MaSan = 13, TenSan = "Bóng Chuyền Bãi Biển", DiaChi = "Sơn Trà, Đà Nẵng", TinhThanh = "Đà Nẵng", SoLuongSanCon = 1, GiaMacDinh = 300000, MaLoaiSan = 2, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", Latitude = 16.0748, Longitude = 108.2435 },
            new SanBong { MaSan = 14, TenSan = "Sân Vận Động Mỹ Đình", DiaChi = "Nam Từ Liêm, Hà Nội", TinhThanh = "Hà Nội", SoLuongSanCon = 1, GiaMacDinh = 5000000, MaLoaiSan = 1, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=2070&auto=format&fit=crop", Latitude = 21.0203, Longitude = 105.7639 },
            new SanBong { MaSan = 15, TenSan = "CLB Bóng Bàn Quận 5", DiaChi = "Quận 5, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 10, GiaMacDinh = 100000, MaLoaiSan = 4, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", Latitude = 10.7542, Longitude = 106.6635 },
            new SanBong { MaSan = 16, TenSan = "Bóng Rổ Yết Kiêu", DiaChi = "Quận 1, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 2, GiaMacDinh = 220000, MaLoaiSan = 3, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1541534741688-6078c6bfb5c5?q=80&w=2069&auto=format&fit=crop", Latitude = 10.7769, Longitude = 106.6954 },
            new SanBong { MaSan = 17, TenSan = "Sân Bóng Đá Cần Thơ", DiaChi = "Ninh Kiều, Cần Thơ", TinhThanh = "Cần Thơ", SoLuongSanCon = 3, GiaMacDinh = 200000, MaLoaiSan = 1, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1526232761682-d26e03ac148e?q=80&w=2029&auto=format&fit=crop", Latitude = 10.0342, Longitude = 105.7828 },
            new SanBong { MaSan = 18, TenSan = "Pickleball Vạn Hạnh", DiaChi = "Quận 10, Hồ Chí Minh", TinhThanh = "TP. Hồ Chí Minh", SoLuongSanCon = 4, GiaMacDinh = 250000, MaLoaiSan = 7, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1622279457486-62dcc4a431d6?q=80&w=2070&auto=format&fit=crop", Latitude = 10.7764, Longitude = 106.6698 },
            new SanBong { MaSan = 19, TenSan = "Sân Bóng Hải Phòng", DiaChi = "Lê Chân, Hải Phòng", TinhThanh = "Hải Phòng", SoLuongSanCon = 2, GiaMacDinh = 180000, MaLoaiSan = 1, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=2070&auto=format&fit=crop", Latitude = 20.8449, Longitude = 106.6881 },
            new SanBong { MaSan = 20, TenSan = "Cầu Lông Bình Dương", DiaChi = "Thủ Dầu Một, Bình Dương", TinhThanh = "Bình Dương", SoLuongSanCon = 5, GiaMacDinh = 120000, MaLoaiSan = 6, TrangThai = "Hoat dong", HinhAnh = "https://images.unsplash.com/photo-1518605368461-1ee7e1273932?q=80&w=2070&auto=format&fit=crop", Latitude = 10.9804, Longitude = 106.6519 }
        );

        modelBuilder.Entity<SanCon>().HasData(
            new SanCon { MaSanCon = 1, MaSan = 1, TenSanCon = "Sân PB1", SucChua = 4 },
            new SanCon { MaSanCon = 2, MaSan = 1, TenSanCon = "Sân PB2", SucChua = 4 },
            new SanCon { MaSanCon = 3, MaSan = 2, TenSanCon = "Sân PB1", SucChua = 4 },
            new SanCon { MaSanCon = 4, MaSan = 2, TenSanCon = "Sân PB2", SucChua = 4 },
            new SanCon { MaSanCon = 5, MaSan = 3, TenSanCon = "Sân 5 số 1", SucChua = 10 },
            new SanCon { MaSanCon = 6, MaSan = 4, TenSanCon = "Sân PB1", SucChua = 4 },
            new SanCon { MaSanCon = 7, MaSan = 4, TenSanCon = "Sân PB2", SucChua = 4 },
            new SanCon { MaSanCon = 8, MaSan = 5, TenSanCon = "Sân PB1", SucChua = 4 },
            new SanCon { MaSanCon = 9, MaSan = 6, TenSanCon = "Sân 5 số 1", SucChua = 10 },
            new SanCon { MaSanCon = 10, MaSan = 6, TenSanCon = "Sân 5 số 2", SucChua = 10 },
            new SanCon { MaSanCon = 11, MaSan = 7, TenSanCon = "Sân Tennis 1", SucChua = 4 },
            new SanCon { MaSanCon = 12, MaSan = 7, TenSanCon = "Sân Tennis 2", SucChua = 4 },
            new SanCon { MaSanCon = 13, MaSan = 8, TenSanCon = "Sân Cầu Lông 1", SucChua = 4 },
            new SanCon { MaSanCon = 14, MaSan = 8, TenSanCon = "Sân Cầu Lông 2", SucChua = 4 },
            new SanCon { MaSanCon = 15, MaSan = 9, TenSanCon = "Sân Bóng Rổ 1", SucChua = 10 },
            new SanCon { MaSanCon = 16, MaSan = 10, TenSanCon = "Sân 5 số 1", SucChua = 10 },
            new SanCon { MaSanCon = 17, MaSan = 11, TenSanCon = "Sân Tennis 1", SucChua = 4 },
            new SanCon { MaSanCon = 18, MaSan = 12, TenSanCon = "Sân Cầu Lông 1", SucChua = 4 },
            new SanCon { MaSanCon = 19, MaSan = 13, TenSanCon = "Sân Bóng Chuyền 1", SucChua = 12 },
            new SanCon { MaSanCon = 20, MaSan = 14, TenSanCon = "Sân 11", SucChua = 22 },
            new SanCon { MaSanCon = 21, MaSan = 15, TenSanCon = "Bàn 1", SucChua = 2 },
            new SanCon { MaSanCon = 22, MaSan = 16, TenSanCon = "Sân Bóng Rổ 1", SucChua = 10 },
            new SanCon { MaSanCon = 23, MaSan = 17, TenSanCon = "Sân 5 số 1", SucChua = 10 },
            new SanCon { MaSanCon = 24, MaSan = 18, TenSanCon = "Sân PB1", SucChua = 4 },
            new SanCon { MaSanCon = 25, MaSan = 19, TenSanCon = "Sân 5 số 1", SucChua = 10 },
            new SanCon { MaSanCon = 26, MaSan = 20, TenSanCon = "Sân Cầu Lông 1", SucChua = 4 }
        );

        modelBuilder.Entity<BangGiaKhungGio>().HasData(
            new BangGiaKhungGio { MaBangGia = 1, NgayTrongTuan = "Thu 2 - Thu 6", GioBatDau = new TimeSpan(6, 0, 0), GioKetThuc = new TimeSpan(8, 0, 0), DonGia = 200000, PhuThuCaoDiem = 0 },
            new BangGiaKhungGio { MaBangGia = 2, NgayTrongTuan = "Thu 2 - Thu 6", GioBatDau = new TimeSpan(17, 0, 0), GioKetThuc = new TimeSpan(19, 0, 0), DonGia = 300000, PhuThuCaoDiem = 50000 },
            new BangGiaKhungGio { MaBangGia = 3, NgayTrongTuan = "Thu 7 - Chu nhat", GioBatDau = new TimeSpan(7, 0, 0), GioKetThuc = new TimeSpan(9, 0, 0), DonGia = 350000, PhuThuCaoDiem = 50000 }
        );

        modelBuilder.Entity<KhachHang>().HasData(
            new KhachHang { MaKH = 1, HoTen = "Nguyen Van An", SoDienThoai = "0901234567", NhomKhach = "Khach quen", GhiChu = "Hay dat buoi toi" },
            new KhachHang { MaKH = 2, HoTen = "Tran Minh Binh", SoDienThoai = "0912345678", NhomKhach = "Khach moi", GhiChu = null }
        );

        modelBuilder.Entity<DichVu>().HasData(
            new DichVu { MaDichVu = 1, TenDichVu = "Nuoc suoi", DonViTinh = "Chai", DonGia = 10000, TonKho = 100 },
            new DichVu { MaDichVu = 2, TenDichVu = "Thue bong", DonViTinh = "Qua", DonGia = 30000, TonKho = 10 },
            new DichVu { MaDichVu = 3, TenDichVu = "Thue ao bib", DonViTinh = "Bo", DonGia = 50000, TonKho = 20 }
        );

        modelBuilder.Entity<ThongTinSan>().HasData(
            new ThongTinSan { Id = 1, TenSan = "SanBong.vn", SoDienThoai = "0888888888", DiaChi = "Linh Trung, Thu Duc, TP.HCM", Email = "contact@sanbong.vn", Website = "www.sanbong.vn", MoTa = "He thong quan ly san bong chuyen nghiep" }
        );

        modelBuilder.Entity<GioHoatDong>().HasData(
            new GioHoatDong { Id = 1, Thu = "Thứ 2", DangHoatDong = true, GioMoCua = new TimeSpan(6, 0, 0), GioDongCua = new TimeSpan(22, 0, 0) },
            new GioHoatDong { Id = 2, Thu = "Thứ 3", DangHoatDong = true, GioMoCua = new TimeSpan(6, 0, 0), GioDongCua = new TimeSpan(22, 0, 0) },
            new GioHoatDong { Id = 3, Thu = "Thứ 4", DangHoatDong = true, GioMoCua = new TimeSpan(6, 0, 0), GioDongCua = new TimeSpan(22, 0, 0) },
            new GioHoatDong { Id = 4, Thu = "Thứ 5", DangHoatDong = true, GioMoCua = new TimeSpan(6, 0, 0), GioDongCua = new TimeSpan(22, 0, 0) },
            new GioHoatDong { Id = 5, Thu = "Thứ 6", DangHoatDong = true, GioMoCua = new TimeSpan(6, 0, 0), GioDongCua = new TimeSpan(22, 0, 0) },
            new GioHoatDong { Id = 6, Thu = "Thứ 7", DangHoatDong = true, GioMoCua = new TimeSpan(6, 0, 0), GioDongCua = new TimeSpan(23, 0, 0) },
            new GioHoatDong { Id = 7, Thu = "Chủ Nhật", DangHoatDong = true, GioMoCua = new TimeSpan(6, 0, 0), GioDongCua = new TimeSpan(23, 0, 0) }
        );

        modelBuilder.Entity<CaiDatThongBao>().HasData(
            new CaiDatThongBao { Id = 1, TenThongBao = "Đặt lịch mới", MoTa = "Nhận thông báo khi có khách đặt sân", DangBat = true, Icon = "fas fa-plus-circle" },
            new CaiDatThongBao { Id = 2, TenThongBao = "Hủy lịch", MoTa = "Nhận thông báo khi khách hủy lịch đặt", DangBat = true, Icon = "fas fa-times-circle" },
            new CaiDatThongBao { Id = 3, TenThongBao = "Thanh toán", MoTa = "Nhận thông báo xác nhận thanh toán", DangBat = true, Icon = "fas fa-check-double" },
            new CaiDatThongBao { Id = 4, TenThongBao = "Báo cáo ngày", MoTa = "Nhận tóm tắt doanh thu cuối ngày", DangBat = true, Icon = "fas fa-file-invoice-dollar" },
            new CaiDatThongBao { Id = 5, TenThongBao = "Nhắc bảo trì sân", MoTa = "Nhắc nhở lịch bảo trì định kỳ", DangBat = true, Icon = "fas fa-tools" }
        );

        modelBuilder.Entity<BlogCategory>().HasData(
            new BlogCategory { Id = 2, TenDanhMuc = "Kinh doanh sân", Slug = "kinh-doanh-san", MoTa = "Kinh nghiệm kinh doanh sân thể thao" },
            new BlogCategory { Id = 3, TenDanhMuc = "Xu hướng", Slug = "xu-huong", MoTa = "Cập nhật xu hướng thể thao mới nhất" },
            new BlogCategory { Id = 4, TenDanhMuc = "Hướng dẫn", Slug = "huong-dan", MoTa = "Hướng dẫn cho chủ sân và người chơi" }
        );

        modelBuilder.Entity<BlogPost>().HasData(
            new BlogPost { Id = 2, TieuDe = "Hướng Dẫn Mở Sân Pickleball Từ A đến Z", Slug = "huong-dan-mo-san-pickleball", MoTaNgan = "Chi phí, vốn và dự toán doanh thu khi kinh doanh sân Pickleball.", NoiDung = "Nội dung bài viết đang được cập nhật...", BlogCategoryId = 2, NgayDang = DateTime.Now, ThoiGianDoc = "15 phút đọc", NoiBat = true },
            new BlogPost { Id = 3, TieuDe = "10 Cách Quản Lý Sân Bóng Hiệu Quả", Slug = "10-cach-quan-ly-san-bong-hieu-qua", MoTaNgan = "Tăng doanh thu 40% trong 3 tháng với các bí quyết quản lý sau.", NoiDung = "Nội dung bài viết đang được cập nhật...", BlogCategoryId = 2, NgayDang = DateTime.Now, ThoiGianDoc = "8 phút đọc", NoiBat = true },
            new BlogPost { Id = 4, TieuDe = "Xu Hướng Sân Pickleball Việt Nam 2025", Slug = "xu-huong-san-pickleball-2025", MoTaNgan = "Cơ hội vàng cho các chủ đầu tư trong năm 2025.", NoiDung = "Nội dung bài viết đang được cập nhật...", BlogCategoryId = 3, NgayDang = DateTime.Now, ThoiGianDoc = "5 phút đọc" },
            new BlogPost { Id = 5, TieuDe = "Cách Tính Giá Thuê Sân Bóng Đá Theo Giờ", Slug = "cach-tinh-gia-thue-san-bong-da", MoTaNgan = "Bí quyết tối đa hóa doanh thu cho cơ sở của bạn.", NoiDung = "Nội dung bài viết đang được cập nhật...", BlogCategoryId = 2, NgayDang = DateTime.Now, ThoiGianDoc = "7 phút đọc" },
            new BlogPost { Id = 8, TieuDe = "Hướng dẫn Check-in Sân Bóng bằng QR Code", Slug = "huong-dan-checkin-qr-code", MoTaNgan = "Cách ứng dụng công nghệ QR Code để quản lý lượt vào sân chuyên nghiệp.", NoiDung = "Nội dung bài viết đang được cập nhật...", BlogCategoryId = 4, NgayDang = DateTime.Now, ThoiGianDoc = "4 phút đọc" },
            new BlogPost { Id = 9, TieuDe = "Mở Sân Bóng Đá Mini Cần Bao Nhiêu Vốn 2026?", Slug = "mo-san-bong-da-mini-bao-nhieu-von", MoTaNgan = "Bảng tính chi tiết vốn đầu tư và thời gian thu hồi vốn.", NoiDung = "Nội dung bài viết đang được cập nhật...", BlogCategoryId = 2, NgayDang = DateTime.Now, ThoiGianDoc = "20 phút đọc" }
        );
    }
}
