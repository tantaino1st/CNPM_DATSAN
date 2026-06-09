# Huong dan chay project QUANLYSANBONG

## 1. Cau truc chinh

- `Models`: 12 model theo de bai: `VaiTro`, `NguoiDung`, `NhanVien`, `KhachHang`, `LoaiSan`, `SanBong`, `BangGiaKhungGio`, `DatSan`, `DichVu`, `ChiTietDichVu`, `HoaDon`, `NhatKyThaoTac`.
- `Data/ApplicationDbContext.cs`: DbContext, cau hinh khoa ngoai, decimal, ten bang SQL Server va seed du lieu mau.
- `Filters`: kiem tra dang nhap/session va vai tro.
- `Controllers`: cac controller quan ly san, loai san, bang gia, dat san, khach hang, dich vu, hoa don, bao cao, nguoi dung, nhat ky.
- `Views`: Razor view dung Bootstrap 5 va `_AdminLayout`.
- `Migrations/20260604000000_InitialCreate.cs`: migration tao database ban dau.

## 2. Cau hinh database

Mo `appsettings.json` va sua connection string neu ten SQL Server khac:

```json
"DefaultConnection": "Server=TANTAI\\SQLEXPRESS;Database=QUANLYSANBONG_DB;Trusted_Connection=True;TrustServerCertificate=True;"
```

## 3. Lenh Migration va Update-Database

Trong Visual Studio 2022, mo Package Manager Console:

```powershell
Update-Database
```

Neu muon tao lai migration tu dau:

```powershell
Add-Migration InitialCreate
Update-Database
```

Dung .NET CLI:

```powershell
dotnet ef database update
```

## 4. Chay project

Trong Visual Studio 2022:

1. Mo file `QUANLYSANBONG.sln`.
2. Chon project startup la `QUANLYSANBONG`.
3. Chay `Update-Database`.
4. Bam `F5` hoac `Ctrl+F5`.

Dung terminal:

```powershell
cd D:\DAPTTKHT\QUANLYSANBONG\QUANLYSANBONG
dotnet ef database update
dotnet run
```

Tai khoan mac dinh:

- Ten dang nhap: `admin`
- Mat khau: `123456`
- Vai tro: `Admin`
