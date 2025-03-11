
-- Table: NhanVien
CREATE TABLE NhanVien (
    MaNhanVien INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    ChucVu NVARCHAR(50) NULL,
    SoDienThoai NVARCHAR(15) NULL,
    LuongCoBan DECIMAL(10,2) NULL
);


-- Table: PhongHat
CREATE TABLE PhongHat (
    MaPhong INT PRIMARY KEY IDENTITY(1,1),
    TenPhong NVARCHAR(50) NOT NULL,
    LoaiPhong NVARCHAR(20) NULL,
    GiaGio DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(20) NULL
);


-- Table: HoaDon
CREATE TABLE HoaDon (
    MaHoaDon INT PRIMARY KEY IDENTITY(1,1),
    NgayLap DATETIME NULL,
    TongTien DECIMAL(10,2) NULL,
    MaNhanVien INT NULL,
    PhuongThucThanhToan NVARCHAR(20) NULL,
    CONSTRAINT FK_HoaDon_MaNhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);


-- Table: MatHang
CREATE TABLE MatHang (
    MaMatHang INT PRIMARY KEY IDENTITY(1,1),
    TenMatHang NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(10,2) NOT NULL,
    SoLuongTon INT NOT NULL
);


-- Table: ChiTietDatHang
CREATE TABLE ChiTietDatHang (
    MaChiTiet INT PRIMARY KEY IDENTITY(1,1),
    MaDatPhong INT NOT NULL,
    MaMatHang INT NOT NULL,
    SoLuong INT NOT NULL,
    ThanhTien DECIMAL(10,2) NULL,
    CONSTRAINT FK_ChiTietDatHang_MaDatPhong FOREIGN KEY (MaDatPhong) REFERENCES DatPhong(MaDatPhong),
    CONSTRAINT FK_ChiTietDatHang_MaMatHang FOREIGN KEY (MaMatHang) REFERENCES MatHang(MaMatHang)
);


-- Table: DatPhong
CREATE TABLE DatPhong (
    MaDatPhong INT PRIMARY KEY IDENTITY(1,1),
    MaPhong INT NOT NULL,
    HoTenKhach NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15) NULL,
    ThoiGianBatDau DATETIME NOT NULL,
    ThoiGianKetThuc DATETIME NULL,
    TongTien DECIMAL(10,2) NULL,
    TrangThai NVARCHAR(20) NULL,
    CONSTRAINT FK_DatPhong_MaPhong FOREIGN KEY (MaPhong) REFERENCES PhongHat(MaPhong)
);


-- Table: TaiKhoan
CREATE TABLE TaiKhoan (
    IDTaiKhoan INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(100) NOT NULL,
    VaiTro NVARCHAR(20) NULL,
    MaNhanVien INT NULL,
    CONSTRAINT FK_TaiKhoan_MaNhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);
