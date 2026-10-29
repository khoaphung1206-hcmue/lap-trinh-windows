/* =====================================================================
   COMP1019 - Lập trình trên Windows | Lab 10
   File     : 02_InsertSampleData.sql
   Nội dung : Nạp dữ liệu mẫu cho EduManagerDB.
              2 khoa, 5 lớp, 8 sinh viên, 5 môn học, 16 kết quả, 3 tài khoản.
   Lưu ý    : Chạy sau 01_CreateDatabase.sql. Thứ tự nhập: bảng cha trước,
              bảng con sau để không vi phạm khóa ngoại.
   ===================================================================== */
USE EduManagerDB;
GO

SET NOCOUNT ON;

-- 1. KHOA (2 khoa)
INSERT INTO KHOA (MaKhoa, TenKhoa, DienThoai) VALUES
('CNTT', N'Công nghệ Thông tin', '02838352020'),
('TOAN', N'Toán - Tin học',      '02838352021');

-- 2. LOP (5 lớp, lớp CNTT03 chưa có sinh viên để test thống kê)
INSERT INTO LOP (MaLop, TenLop, MaKhoa, NienKhoa) VALUES
('CNTT01',  N'Công nghệ Thông tin 1', 'CNTT', 'K51'),
('CNTT02',  N'Công nghệ Thông tin 2', 'CNTT', 'K51'),
('CNTT03',  N'Công nghệ Thông tin 3', 'CNTT', 'K52'),
('SPTIN01', N'Sư phạm Tin học 1',     'TOAN', 'K51'),
('SPTIN02', N'Sư phạm Tin học 2',     'TOAN', 'K51');

-- 3. SINHVIEN (8 sinh viên, SV007 chưa có điểm để test câu xóa)
INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop) VALUES
('SV001', N'Nguyễn Văn An',   N'Nam', '2005-03-12', 'an.nguyen@student.edu.vn',   '0901234567', 'CNTT01'),
('SV002', N'Trần Thị Bình',   N'Nữ',  '2005-07-21', 'binh.tran@student.edu.vn',   '0912345678', 'CNTT01'),
('SV003', N'Lê Hoàng Cường',  N'Nam', '2004-11-05', 'cuong.le@student.edu.vn',    '0923456789', 'CNTT01'),
('SV004', N'Phạm Thu Dung',   N'Nữ',  '2005-01-30', 'dung.pham@student.edu.vn',   '0934567890', 'CNTT02'),
('SV005', N'Võ Minh Đức',     N'Nam', '2005-09-18', 'duc.vo@student.edu.vn',      '0945678901', 'CNTT02'),
('SV006', N'Đặng Ngọc Hà',    N'Nữ',  '2005-05-09', 'ha.dang@student.edu.vn',     '0956789012', 'SPTIN01'),
('SV007', N'Bùi Quang Huy',   N'Nam', '2004-12-25', 'huy.bui@student.edu.vn',     '0967890123', 'SPTIN01'),
('SV008', N'Hoàng Mai Lan',   N'Nữ',  '2005-08-14', 'lan.hoang@student.edu.vn',   '0978901234', 'SPTIN02');

-- 4. MONHOC (5 môn)
INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES
('MH001', N'Lập trình trên Windows',          3),
('MH002', N'Cơ sở dữ liệu',                   3),
('MH003', N'Cấu trúc dữ liệu và giải thuật',  4),
('MH004', N'Lập trình Web',                   3),
('MH005', N'Tiếng Anh chuyên ngành',          2);

-- 5. KETQUA (16 dòng)
INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES
('SV001', 'MH001', 8.0), ('SV001', 'MH002', 7.5), ('SV001', 'MH003', 9.0),
('SV002', 'MH001', 9.0), ('SV002', 'MH002', 8.5), ('SV002', 'MH003', 8.0),
('SV003', 'MH001', 6.5), ('SV003', 'MH002', 7.0),
('SV004', 'MH001', 7.0), ('SV004', 'MH004', 8.0),
('SV005', 'MH003', 5.5), ('SV005', 'MH004', 6.5),
('SV006', 'MH001', 8.5), ('SV006', 'MH005', 9.0),
('SV008', 'MH002', 4.0), ('SV008', 'MH004', 5.5);

-- 6. TAIKHOAN (3 tài khoản demo, mật khẩu băm SHA-256)
--    admin / 123 : quản trị viên      user / 123 : người dùng
--    tester / 123: tài khoản bị khóa (TrangThai = 0)
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai) VALUES
('admin',  CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123'), 2), N'Quản trị viên',     'Admin', 1),
('user',   CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123'), 2), N'Nhân viên bán hàng', 'User',  1),
('tester', CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123'), 2), N'Tài khoản thử',     'User',  0);

SET NOCOUNT OFF;
GO

-- 7. Kiểm tra số dòng của từng bảng
SELECT N'KHOA' AS Bang,     COUNT(*) AS SoDong FROM KHOA
UNION ALL SELECT N'LOP',      COUNT(*) FROM LOP
UNION ALL SELECT N'SINHVIEN', COUNT(*) FROM SINHVIEN
UNION ALL SELECT N'MONHOC',   COUNT(*) FROM MONHOC
UNION ALL SELECT N'KETQUA',   COUNT(*) FROM KETQUA
UNION ALL SELECT N'TAIKHOAN', COUNT(*) FROM TAIKHOAN;
GO
