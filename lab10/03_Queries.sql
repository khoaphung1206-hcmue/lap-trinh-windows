/* =====================================================================
   COMP1019 - Lập trình trên Windows | Lab 10
   File     : 03_Queries.sql
   Nội dung : A. Truy vấn SELECT (Câu 1-4)
              B. Cập nhật và xóa dữ liệu (Câu 5-6)
              C. Truy vấn liên kết và thống kê (Câu 7-10)
              D. Kiểm thử ràng buộc (Câu 11-12)
              E. Truy vấn bổ sung
              F. Bộ kiểm thử tự động các ràng buộc
   Lưu ý    : Chạy sau 01 và 02. Các câu lỗi dùng TRY...CATCH để hiển thị
              mã lỗi mà script vẫn chạy hết. Câu 5, 6 chạy trong transaction
              rồi ROLLBACK nên dữ liệu gốc không bị thay đổi.
   ===================================================================== */
USE EduManagerDB;
GO

/* ===================== A. TRUY VẤN SELECT ===================== */

-- Câu 1. Liệt kê toàn bộ sinh viên
SELECT MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop
FROM SINHVIEN
ORDER BY MaSV;
GO

-- Câu 2. Liệt kê sinh viên kèm tên lớp và tên khoa (INNER JOIN 3 bảng)
SELECT sv.MaSV, sv.HoTen, sv.GioiTinh, l.TenLop, k.TenKhoa
FROM SINHVIEN AS sv
INNER JOIN LOP  AS l ON sv.MaLop = l.MaLop
INNER JOIN KHOA AS k ON l.MaKhoa = k.MaKhoa
ORDER BY sv.MaSV;
GO

-- Câu 3. Tìm sinh viên theo một phần họ tên bằng LIKE
DECLARE @TuKhoa NVARCHAR(50) = N'Văn';
SELECT MaSV, HoTen, MaLop
FROM SINHVIEN
WHERE HoTen LIKE N'%' + @TuKhoa + N'%';
GO

-- Câu 4. Liệt kê sinh viên theo mã lớp
DECLARE @MaLop VARCHAR(10) = 'CNTT01';
SELECT MaSV, HoTen, GioiTinh, NgaySinh
FROM SINHVIEN
WHERE MaLop = @MaLop
ORDER BY MaSV;
GO

/* ===================== B. CẬP NHẬT VÀ XÓA ===================== */

-- Câu 5. Cập nhật họ tên của SV001 và chuyển SV002 sang lớp CNTT02
BEGIN TRANSACTION;

    UPDATE SINHVIEN SET HoTen = N'Nguyễn Văn Anh' WHERE MaSV = 'SV001';
    UPDATE SINHVIEN SET MaLop = 'CNTT02'          WHERE MaSV = 'SV002';

    -- Kết quả 1: dữ liệu trong transaction (đã thay đổi)
    SELECT N'Trong transaction' AS TrangThai, MaSV, HoTen, MaLop
    FROM SINHVIEN WHERE MaSV IN ('SV001', 'SV002');

ROLLBACK TRANSACTION;   -- Muốn giữ thay đổi thì đổi thành COMMIT TRANSACTION

-- Kết quả 2: sau ROLLBACK (đã trở về như cũ)
SELECT N'Sau ROLLBACK' AS TrangThai, MaSV, HoTen, MaLop
FROM SINHVIEN WHERE MaSV IN ('SV001', 'SV002');
GO

-- Câu 6. Xóa sinh viên chưa có dữ liệu điểm
--        Thử xóa SV003 (đã có điểm) và SV007 (chưa có điểm): chỉ SV007 bị xóa
BEGIN TRANSACTION;

    DELETE sv
    FROM SINHVIEN AS sv
    WHERE sv.MaSV IN ('SV003', 'SV007')
      AND NOT EXISTS (SELECT 1 FROM KETQUA AS kq WHERE kq.MaSV = sv.MaSV);

    -- Kết quả 1: trong transaction chỉ còn SV003
    SELECT N'Trong transaction' AS TrangThai, MaSV, HoTen
    FROM SINHVIEN WHERE MaSV IN ('SV003', 'SV007');

ROLLBACK TRANSACTION;

-- Kết quả 2: sau ROLLBACK, SV007 trở lại
SELECT N'Sau ROLLBACK' AS TrangThai, MaSV, HoTen
FROM SINHVIEN WHERE MaSV IN ('SV003', 'SV007');
GO

/* ============== C. TRUY VẤN LIÊN KẾT VÀ THỐNG KÊ ============== */

-- Câu 7. Điểm của từng sinh viên theo từng môn
SELECT sv.MaSV, sv.HoTen, mh.MaMH, mh.TenMH, kq.Diem
FROM KETQUA AS kq
INNER JOIN SINHVIEN AS sv ON kq.MaSV = sv.MaSV
INNER JOIN MONHOC   AS mh ON kq.MaMH = mh.MaMH
ORDER BY sv.MaSV, mh.MaMH;
GO

-- Câu 8. Điểm trung bình của từng sinh viên (GROUP BY, AVG)
SELECT sv.MaSV, sv.HoTen,
       COUNT(kq.MaMH) AS SoMon,
       CAST(AVG(kq.Diem) AS DECIMAL(4,2)) AS DiemTrungBinh
FROM SINHVIEN AS sv
INNER JOIN KETQUA AS kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen
ORDER BY DiemTrungBinh DESC;
GO

-- Câu 9. Thống kê số sinh viên theo từng lớp
--        Dùng LEFT JOIN để lớp chưa có sinh viên vẫn hiển thị với số lượng 0
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP AS l
LEFT JOIN SINHVIEN AS sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
ORDER BY SoSinhVien DESC, l.MaLop;
GO

-- Câu 10. Liệt kê các lớp có từ 2 sinh viên trở lên (HAVING)
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP AS l
LEFT JOIN SINHVIEN AS sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
HAVING COUNT(sv.MaSV) >= 2
ORDER BY SoSinhVien DESC, l.MaLop;
GO

/* ================= D. KIỂM THỬ RÀNG BUỘC ================= */

-- Câu 11. Thêm sinh viên vào lớp không tồn tại -> lỗi khóa ngoại (Msg 547)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop)
    VALUES ('SV099', N'Sinh viên lớp sai', N'Nam', '2005-01-01', 'sv099@student.edu.vn', '0900000099', 'CNTT99');

    SELECT N'KHÔNG CÓ LỖI - ràng buộc chưa hoạt động' AS KetQua;
END TRY
BEGIN CATCH
    SELECT N'Câu 11' AS Cau, ERROR_NUMBER() AS MaLoi, ERROR_MESSAGE() AS ThongBaoLoi;
END CATCH
GO

-- Câu 12. Nhập điểm ngoài khoảng 0-10 -> lỗi CHECK CK_KETQUA_Diem (Msg 547)
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV001', 'MH004', 12);

    SELECT N'KHÔNG CÓ LỖI - ràng buộc chưa hoạt động' AS KetQua;
END TRY
BEGIN CATCH
    SELECT N'Câu 12' AS Cau, ERROR_NUMBER() AS MaLoi, ERROR_MESSAGE() AS ThongBaoLoi;
END CATCH
GO

/* ================= E. TRUY VẤN BỔ SUNG ================= */

-- E1. Dùng view: thông tin sinh viên đầy đủ
SELECT * FROM vw_SinhVienChiTiet ORDER BY MaSV;
GO

-- E2. Điểm trung bình theo từng môn học
SELECT mh.MaMH, mh.TenMH, COUNT(kq.MaSV) AS SoSinhVien, CAST(AVG(kq.Diem) AS DECIMAL(4,2)) AS DiemTrungBinh
FROM MONHOC AS mh
LEFT JOIN KETQUA AS kq ON mh.MaMH = kq.MaMH
GROUP BY mh.MaMH, mh.TenMH
ORDER BY mh.MaMH;
GO

-- E3. Điểm trung bình có trọng số tín chỉ của từng sinh viên
SELECT sv.MaSV, sv.HoTen,
       CAST(SUM(kq.Diem * mh.SoTinChi) / SUM(mh.SoTinChi) AS DECIMAL(4,2)) AS DiemTBTinChi
FROM SINHVIEN AS sv
INNER JOIN KETQUA AS kq ON sv.MaSV = kq.MaSV
INNER JOIN MONHOC AS mh ON kq.MaMH = mh.MaMH
GROUP BY sv.MaSV, sv.HoTen
ORDER BY DiemTBTinChi DESC;
GO

-- E4. Sinh viên chưa có điểm môn nào
SELECT sv.MaSV, sv.HoTen, sv.MaLop
FROM SINHVIEN AS sv
WHERE NOT EXISTS (SELECT 1 FROM KETQUA AS kq WHERE kq.MaSV = sv.MaSV);
GO

/* ========= F. BỘ KIỂM THỬ TỰ ĐỘNG CÁC RÀNG BUỘC =========
   Mỗi test cố ý nhập dữ liệu sai và so sánh mã lỗi thực tế với mã lỗi mong đợi:
     547  = vi phạm khóa ngoại hoặc CHECK
     2627 = trùng khóa chính
     2601 = trùng chỉ mục UNIQUE
   Toàn bộ chạy trong transaction và ROLLBACK ở cuối nên không làm đổi dữ liệu. */
DECLARE @Test TABLE
(
    STT        INT IDENTITY(1,1),
    MoTa       NVARCHAR(200),
    LoiMongDoi INT,
    LoiThucTe  INT NULL,
    ThongBao   NVARCHAR(MAX),
    KetQua     AS (CASE WHEN LoiThucTe = LoiMongDoi THEN 'PASS' ELSE 'FAIL' END)
);

BEGIN TRANSACTION;

-- Test 1: Thêm sinh viên vào lớp không tồn tại (FK)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop) VALUES ('SV100', N'Test lớp sai', N'Nam', '2005-01-01', 'sv100@student.edu.vn', '0900000100', 'CNTT99');
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Thêm sinh viên vào lớp không tồn tại (FK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Thêm sinh viên vào lớp không tồn tại (FK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 2: Điểm lớn hơn 10 (CHECK)
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV001', 'MH004', 12);
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Điểm lớn hơn 10 (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Điểm lớn hơn 10 (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 3: Điểm nhỏ hơn 0 (CHECK)
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV001', 'MH004', -1);
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Điểm nhỏ hơn 0 (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Điểm nhỏ hơn 0 (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 4: Giới tính không phải Nam/Nữ (CHECK)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop) VALUES ('SV101', N'Test giới tính', N'Khác', '2005-01-01', 'sv101@student.edu.vn', '0900000101', 'CNTT01');
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Giới tính không phải Nam/Nữ (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Giới tính không phải Nam/Nữ (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 5: Số tín chỉ bằng 0 (CHECK)
BEGIN TRY
    INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES ('MH099', N'Môn tín chỉ bằng 0', 0);
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Số tín chỉ bằng 0 (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Số tín chỉ bằng 0 (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 6: Số tín chỉ âm (CHECK)
BEGIN TRY
    INSERT INTO MONHOC (MaMH, TenMH, SoTinChi) VALUES ('MH098', N'Môn tín chỉ âm', -2);
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Số tín chỉ âm (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Số tín chỉ âm (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 7: Trùng khóa chính bảng SINHVIEN (PK)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop) VALUES ('SV001', N'Test trùng mã', N'Nam', '2005-01-01', 'sv102@student.edu.vn', '0900000102', 'CNTT01');
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Trùng khóa chính bảng SINHVIEN (PK)', 2627, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Trùng khóa chính bảng SINHVIEN (PK)', 2627, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 8: Trùng khóa chính ghép bảng KETQUA (PK)
BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem) VALUES ('SV001', 'MH001', 5);
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Trùng khóa chính ghép bảng KETQUA (PK)', 2627, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Trùng khóa chính ghép bảng KETQUA (PK)', 2627, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 9: Trùng email sinh viên (UNIQUE INDEX)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop) VALUES ('SV103', N'Test trùng email', N'Nam', '2005-01-01', 'an.nguyen@student.edu.vn', '0900000103', 'CNTT01');
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Trùng email sinh viên (UNIQUE INDEX)', 2601, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Trùng email sinh viên (UNIQUE INDEX)', 2601, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 10: Họ tên rỗng (CHECK)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop) VALUES ('SV104', N'   ', N'Nam', '2005-01-01', 'sv104@student.edu.vn', '0900000104', 'CNTT01');
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Họ tên rỗng (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Họ tên rỗng (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 11: Ngày sinh ở tương lai (CHECK)
BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, GioiTinh, NgaySinh, Email, SoDienThoai, MaLop) VALUES ('SV105', N'Test ngày sinh', N'Nam', '2099-01-01', 'sv105@student.edu.vn', '0900000105', 'CNTT01');
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Ngày sinh ở tương lai (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Ngày sinh ở tương lai (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 12: Xóa lớp đang có sinh viên (FK, NO ACTION)
BEGIN TRY
    DELETE FROM LOP WHERE MaLop = 'CNTT01';
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Xóa lớp đang có sinh viên (FK, NO ACTION)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Xóa lớp đang có sinh viên (FK, NO ACTION)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 13: Xóa môn học đã có điểm (FK, NO ACTION)
BEGIN TRY
    DELETE FROM MONHOC WHERE MaMH = 'MH001';
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Xóa môn học đã có điểm (FK, NO ACTION)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Xóa môn học đã có điểm (FK, NO ACTION)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

-- Test 14: Vai trò tài khoản không hợp lệ (CHECK)
BEGIN TRY
    INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai) VALUES ('hacker', 'x', N'Test vai trò', 'Guest', 1);
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Vai trò tài khoản không hợp lệ (CHECK)', 547, NULL, N'Không có lỗi: dữ liệu sai đã được chấp nhận');
END TRY
BEGIN CATCH
    INSERT INTO @Test (MoTa, LoiMongDoi, LoiThucTe, ThongBao)
    VALUES (N'Vai trò tài khoản không hợp lệ (CHECK)', 547, ERROR_NUMBER(), ERROR_MESSAGE());
END CATCH

ROLLBACK TRANSACTION;   -- Hoàn tác toàn bộ, dữ liệu gốc không đổi

-- Chi tiết từng test
SELECT STT, MoTa, LoiMongDoi, LoiThucTe, KetQua, ThongBao FROM @Test ORDER BY STT;

-- Tổng kết
SELECT COUNT(*) AS TongSoTest,
       SUM(CASE WHEN KetQua = 'PASS' THEN 1 ELSE 0 END) AS SoPASS,
       SUM(CASE WHEN KetQua = 'FAIL' THEN 1 ELSE 0 END) AS SoFAIL
FROM @Test;
GO
