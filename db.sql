
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


CREATE TABLE HoaDonNhap (
    MaHDN INT IDENTITY(1,1) PRIMARY KEY,  -- Mã hóa đơn nhập (tự tăng)
    NgayNhap DATETIME DEFAULT GETDATE(),  -- Ngày nhập hàng
    NhaCungCap NVARCHAR(255) NOT NULL,    -- Tên nhà cung cấp
    MaNhanVien NVARCHAR(100) NOT NULL,     -- Nhân viên nhập hàng
    MaMatHang INT NOT NULL,               -- Mã mặt hàng
    TenMatHang NVARCHAR(255) NOT NULL,    -- Tên mặt hàng
    SoLuongTon INT CHECK (SoLuongTon > 0),      -- Số lượng nhập
    DonGia DECIMAL(18,2) CHECK (DonGia >= 0),  -- Giá nhập hàng
    TongTien DECIMAL(18,2) 
);
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATE NOT NULL,
    MaKhachHang INT NULL,
    TongTien DECIMAL(18,2) NULL,
    MaNhanVien INT NOT NULL,
    MaDatPhong INT NULL
);

CREATE TABLE ChiTietHoaDon (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon INT NOT NULL,
    MaMatHang INT NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    CONSTRAINT FK_ChiTietHoaDon_MatHang FOREIGN KEY (MaMatHang) REFERENCES MatHang(MaMatHang)
);


