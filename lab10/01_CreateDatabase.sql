/* =====================================================================
   COMP1019 - Lập trình trên Windows | Lab 10
   File     : 01_CreateDatabase.sql
   Nội dung : Tạo CSDL EduManagerDB, 6 bảng, khóa chính, khóa ngoại,
              ràng buộc CHECK/UNIQUE, chỉ mục và 2 view.
   Cách chạy: Mở trong SSMS và nhấn F5 (chạy cả file). Có thể chạy lại
              nhiều lần vì script tự xóa database cũ.
   ===================================================================== */

-- 1. Tạo lại database từ đầu -------------------------------------------
USE master;
GO

IF DB_ID(N'EduManagerDB') IS NOT NULL
BEGIN
    -- Đóng mọi kết nối đang mở để có thể xóa database
    ALTER DATABASE EduManagerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE EduManagerDB;
END
GO

CREATE DATABASE EduManagerDB;
GO

USE EduManagerDB;
GO

-- 2. Tạo bảng -------------------------------------------------------------

-- 2.1. KHOA: thông tin khoa
CREATE TABLE KHOA
(
    MaKhoa      VARCHAR(10)   NOT NULL,                 -- Mã khoa (khóa chính)
    TenKhoa     NVARCHAR(100) NOT NULL,                 -- Tên khoa, không trùng
    DienThoai   VARCHAR(11)   NULL,                     -- Điện thoại, chỉ gồm 9-11 chữ số
    CONSTRAINT PK_KHOA PRIMARY KEY (MaKhoa),
    CONSTRAINT UQ_KHOA_TenKhoa UNIQUE (TenKhoa),
    CONSTRAINT CK_KHOA_DienThoai CHECK
        (DienThoai IS NULL OR (LEN(DienThoai) BETWEEN 9 AND 11 AND DienThoai NOT LIKE '%[^0-9]%'))
);
GO

-- 2.2. LOP: mỗi lớp thuộc một khoa
CREATE TABLE LOP
(
    MaLop       VARCHAR(10)   NOT NULL,                 -- Mã lớp (khóa chính)
    TenLop      NVARCHAR(100) NOT NULL,                 -- Tên lớp, không trùng
    MaKhoa      VARCHAR(10)   NOT NULL,                 -- Khóa ngoại -> KHOA
    NienKhoa    VARCHAR(10)   NULL,                     -- Niên khóa, ví dụ K51
    CONSTRAINT PK_LOP PRIMARY KEY (MaLop),
    CONSTRAINT UQ_LOP_TenLop UNIQUE (TenLop),
    CONSTRAINT FK_LOP_KHOA FOREIGN KEY (MaKhoa) REFERENCES KHOA (MaKhoa)
        ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- 2.3. SINHVIEN: mỗi sinh viên thuộc một lớp
CREATE TABLE SINHVIEN
(
    MaSV        VARCHAR(10)   NOT NULL,                 -- Mã sinh viên (khóa chính)
    HoTen       NVARCHAR(100) NOT NULL,                 -- Họ tên, không rỗng
    GioiTinh    NVARCHAR(3)   NOT NULL,                 -- Chỉ nhận Nam hoặc Nữ
    NgaySinh    DATE          NOT NULL,                 -- Không ở tương lai
    Email       VARCHAR(100)  NULL,                     -- Đúng dạng, không trùng (xem chỉ mục bên dưới)
    SoDienThoai VARCHAR(11)   NULL,                     -- 9-11 chữ số
    MaLop       VARCHAR(10)   NOT NULL,                 -- Khóa ngoại -> LOP
    CONSTRAINT PK_SINHVIEN PRIMARY KEY (MaSV),
    CONSTRAINT CK_SINHVIEN_HoTen CHECK (LEN(LTRIM(RTRIM(HoTen))) > 0),
    CONSTRAINT CK_SINHVIEN_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    CONSTRAINT CK_SINHVIEN_NgaySinh CHECK (NgaySinh <= CAST(GETDATE() AS DATE)),
    CONSTRAINT CK_SINHVIEN_Email CHECK (Email IS NULL OR Email LIKE '_%@_%._%'),
    CONSTRAINT CK_SINHVIEN_SoDienThoai CHECK
        (SoDienThoai IS NULL OR (LEN(SoDienThoai) BETWEEN 9 AND 11 AND SoDienThoai NOT LIKE '%[^0-9]%')),
    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP (MaLop)
        ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- 2.4. MONHOC: thông tin môn học
CREATE TABLE MONHOC
(
    MaMH        VARCHAR(10)   NOT NULL,                 -- Mã môn học (khóa chính)
    TenMH       NVARCHAR(100) NOT NULL,                 -- Tên môn học, không trùng
    SoTinChi    INT           NOT NULL,                 -- Số tín chỉ phải lớn hơn 0
    CONSTRAINT PK_MONHOC PRIMARY KEY (MaMH),
    CONSTRAINT UQ_MONHOC_TenMH UNIQUE (TenMH),
    CONSTRAINT CK_MONHOC_SoTinChi CHECK (SoTinChi > 0)
);
GO

-- 2.5. KETQUA: điểm của sinh viên theo từng môn (khóa chính ghép)
CREATE TABLE KETQUA
(
    MaSV        VARCHAR(10)   NOT NULL,                 -- Khóa ngoại -> SINHVIEN
    MaMH        VARCHAR(10)   NOT NULL,                 -- Khóa ngoại -> MONHOC
    Diem        DECIMAL(4,2)  NOT NULL,                 -- Điểm từ 0 đến 10
    CONSTRAINT PK_KETQUA PRIMARY KEY (MaSV, MaMH),
    CONSTRAINT CK_KETQUA_Diem CHECK (Diem BETWEEN 0 AND 10),
    CONSTRAINT FK_KETQUA_SINHVIEN FOREIGN KEY (MaSV) REFERENCES SINHVIEN (MaSV)
        ON UPDATE CASCADE ON DELETE NO ACTION,
    CONSTRAINT FK_KETQUA_MONHOC FOREIGN KEY (MaMH) REFERENCES MONHOC (MaMH)
        ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- 2.6. TAIKHOAN: tài khoản đăng nhập demo
CREATE TABLE TAIKHOAN
(
    TenDangNhap VARCHAR(50)   NOT NULL,                 -- Tên đăng nhập (khóa chính)
    MatKhau     VARCHAR(64)   NOT NULL,                 -- Mật khẩu đã băm SHA-256 (dạng hex)
    HoTen       NVARCHAR(100) NOT NULL,
    VaiTro      VARCHAR(10)   NOT NULL
        CONSTRAINT DF_TAIKHOAN_VaiTro DEFAULT 'User',   -- Mặc định User
    TrangThai   BIT           NOT NULL
        CONSTRAINT DF_TAIKHOAN_TrangThai DEFAULT 1,     -- 1: hoạt động, 0: bị khóa
    CONSTRAINT PK_TAIKHOAN PRIMARY KEY (TenDangNhap),
    CONSTRAINT CK_TAIKHOAN_VaiTro CHECK (VaiTro IN ('Admin', 'User'))
);
GO

-- 3. Chỉ mục ------------------------------------------------------------------
-- Email không được trùng nhưng cho phép nhiều dòng chưa có email (NULL)
CREATE UNIQUE INDEX UX_SINHVIEN_Email ON SINHVIEN (Email) WHERE Email IS NOT NULL;
CREATE INDEX IX_LOP_MaKhoa      ON LOP (MaKhoa);
CREATE INDEX IX_SINHVIEN_MaLop  ON SINHVIEN (MaLop);
CREATE INDEX IX_SINHVIEN_HoTen  ON SINHVIEN (HoTen);
CREATE INDEX IX_KETQUA_MaMH     ON KETQUA (MaMH);
GO

-- 4. View -----------------------------------------------------------------------
-- 4.1. Thông tin sinh viên kèm lớp và khoa
CREATE VIEW vw_SinhVienChiTiet
AS
SELECT sv.MaSV, sv.HoTen, sv.GioiTinh, sv.NgaySinh, sv.Email, sv.SoDienThoai,
       l.MaLop, l.TenLop, k.MaKhoa, k.TenKhoa
FROM SINHVIEN AS sv
INNER JOIN LOP AS l ON sv.MaLop = l.MaLop
INNER JOIN KHOA AS k ON l.MaKhoa = k.MaKhoa;
GO

-- 4.2. Điểm trung bình của từng sinh viên (chỉ gồm sinh viên đã có điểm)
CREATE VIEW vw_DiemTrungBinh
AS
SELECT sv.MaSV, sv.HoTen,
       COUNT(kq.MaMH) AS SoMon,
       CAST(AVG(kq.Diem) AS DECIMAL(4,2)) AS DiemTrungBinh
FROM SINHVIEN AS sv
INNER JOIN KETQUA AS kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen;
GO

-- 5. Kiểm tra cấu trúc vừa tạo ---------------------------------------------
-- 5.1. Danh sách bảng (phải có đủ 6 bảng)
SELECT TABLE_NAME AS TenBang
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO

-- 5.2. Danh sách khóa ngoại và kiểu cập nhật/xóa
SELECT fk.name                              AS TenKhoaNgoai,
       OBJECT_NAME(fk.parent_object_id)     AS BangCon,
       OBJECT_NAME(fk.referenced_object_id) AS BangCha,
       fk.update_referential_action_desc    AS KhiCapNhat,
       fk.delete_referential_action_desc    AS KhiXoa
FROM sys.foreign_keys AS fk
ORDER BY fk.name;
GO
