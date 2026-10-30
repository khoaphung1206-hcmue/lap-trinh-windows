/* =====================================================================
   COMP1019 - Lập trình trên Windows | Lab 11
   File     : EduManagerDB.sql
   Nội dung : Script database tối thiểu cho Lab 11 (bảng LOP, SINHVIEN)
              kèm dữ liệu mẫu của đề.
   Đặc điểm : An toàn khi chạy lại - chỉ tạo database, bảng, dòng dữ liệu
              nào chưa có, không xóa dữ liệu hiện có.
   Lưu ý    : Nếu đã có database EduManagerDB của Lab 10 (bảng LOP có thêm
              cột MaKhoa) thì phần dữ liệu mẫu tối thiểu sẽ tự bỏ qua,
              không cần chạy file này.
   ===================================================================== */
USE master;
GO

IF DB_ID(N'EduManagerDB') IS NULL
    CREATE DATABASE EduManagerDB;
GO

USE EduManagerDB;
GO

-- 1. Tạo bảng nếu chưa có
IF OBJECT_ID(N'dbo.LOP', N'U') IS NULL
    CREATE TABLE LOP
    (
        MaLop  VARCHAR(20)   PRIMARY KEY,
        TenLop NVARCHAR(100) NOT NULL
    );
GO

IF OBJECT_ID(N'dbo.SINHVIEN', N'U') IS NULL
    CREATE TABLE SINHVIEN
    (
        MaSV   VARCHAR(20)   PRIMARY KEY,
        HoTen  NVARCHAR(100) NOT NULL,
        MaLop  VARCHAR(20)   NOT NULL,
        CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP (MaLop)
    );
GO

-- 2. Dữ liệu mẫu (chỉ nạp khi bảng LOP là dạng tối thiểu, không có cột MaKhoa)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.LOP') AND name = N'MaKhoa')
BEGIN
    INSERT INTO LOP (MaLop, TenLop)
    SELECT v.MaLop, v.TenLop
    FROM (VALUES
            ('CNTT01',  N'Công nghệ thông tin 01'),
            ('CNTT02',  N'Công nghệ thông tin 02'),
            ('SPTIN01', N'Sư phạm Tin học 01')
         ) AS v (MaLop, TenLop)
    WHERE NOT EXISTS (SELECT 1 FROM LOP AS l WHERE l.MaLop = v.MaLop);

    INSERT INTO SINHVIEN (MaSV, HoTen, MaLop)
    SELECT v.MaSV, v.HoTen, v.MaLop
    FROM (VALUES
            ('SV001', N'Nguyễn Văn An', 'CNTT01')
         ) AS v (MaSV, HoTen, MaLop)
    WHERE NOT EXISTS (SELECT 1 FROM SINHVIEN AS sv WHERE sv.MaSV = v.MaSV);
END
ELSE
    PRINT N'Đã có database Lab 10 (bảng LOP có cột MaKhoa), bỏ qua dữ liệu mẫu tối thiểu.';
GO

-- 3. Kiểm tra nhanh
SELECT * FROM LOP ORDER BY MaLop;
SELECT * FROM SINHVIEN ORDER BY MaSV;
GO
