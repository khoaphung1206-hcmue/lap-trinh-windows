# DANH SÁCH TEST CASE - LAB 11 (ADO.NET và CRUD với SQL Server)

Điền cột **Kết quả** (`Đạt` / `Không đạt`) sau khi tự chạy thử từng test case.
Dữ liệu ban đầu giả định là database `EduManagerDB` của Lab 10 (CNTT01 có 3 sinh viên, CNTT03 chưa có sinh viên).

| Mã | Chức năng | Thao tác | Kết quả mong đợi | Kết quả |
| :--- | :--- | :--- | :--- | :--- |
| TC01 | Load dữ liệu (F1) | Mở Form | Lưới hiển thị danh sách lớp từ SQL Server kèm số sinh viên; thanh trạng thái báo đã tải | |
| TC02 | Chọn dòng (F2) | Bấm vào dòng CNTT01 | Mã lớp, tên lớp, khoa hiện lên ô nhập; ô mã lớp bị khóa | |
| TC03 | Chọn dòng bằng bàn phím (F2) | Bấm vào lưới rồi nhấn phím mũi tên xuống | Dữ liệu ô nhập đổi theo dòng đang chọn | |
| TC04 | Thêm hợp lệ (F3) | Nhập mã `TEST01`, tên `Lớp thử nghiệm`, chọn khoa, bấm Thêm | Lớp xuất hiện trong lưới và trong bảng LOP của SSMS; các ô nhập được xóa | |
| TC05 | Thêm thiếu mã (F3) | Để trống mã lớp, bấm Thêm | Báo lỗi thiếu mã lớp, con trỏ về ô mã lớp, không thêm | |
| TC06 | Thêm thiếu tên (F3) | Nhập mã, để trống tên lớp, bấm Thêm | Báo lỗi thiếu tên lớp, con trỏ về ô tên lớp, không thêm | |
| TC07 | Thêm trùng mã (F3) | Nhập mã `CNTT01`, bấm Thêm | Báo mã lớp đã tồn tại, không thêm | |
| TC08 | Thêm mã có ký tự lạ (F3) | Nhập mã `A B!`, bấm Thêm | Báo mã lớp chỉ gồm chữ không dấu, số, `_`, `-`; không thêm | |
| TC09 | Sửa hợp lệ (F4) | Chọn `TEST01`, đổi tên lớp, bấm Sửa | Lưới hiển thị tên mới; SSMS cho thấy tên đã đổi | |
| TC10 | Sửa khi chưa chọn (F4) | Bấm Sửa khi chưa chọn dòng nào | Báo cần chọn lớp trước khi sửa, chương trình không lỗi | |
| TC11 | Xóa khi chưa chọn (F5) | Bấm Xóa khi chưa chọn dòng nào | Báo cần chọn lớp trước khi xóa, chương trình không lỗi | |
| TC12 | Xóa lớp có sinh viên (F5) | Chọn `CNTT01`, bấm Xóa | Báo lớp đang có sinh viên nên không thể xóa; lớp vẫn còn | |
| TC13 | Xóa có xác nhận (F5) | Chọn `TEST01`, bấm Xóa, chọn No | Hộp thoại xác nhận hiện ra; chọn No thì lớp vẫn còn | |
| TC14 | Xóa lớp không có sinh viên (F5) | Chọn `TEST01`, bấm Xóa, chọn Yes | Lớp bị xóa khỏi lưới và khỏi SSMS | |
| TC15 | Tìm kiếm (F6) | Nhập `CNTT`, bấm Tìm kiếm (hoặc nhấn Enter) | Lưới chỉ còn các lớp có mã hoặc tên chứa `CNTT`; trạng thái báo số lớp tìm thấy | |
| TC16 | Tìm không phân biệt hoa thường (F6) | Nhập `cntt`, bấm Tìm kiếm | Kết quả giống TC15 | |
| TC17 | Tìm không thấy (F6) | Nhập `ZZZ`, bấm Tìm kiếm | Lưới rỗng, trạng thái báo tìm thấy 0 lớp | |
| TC18 | Chống SQL injection (F8) | Nhập `' OR 1=1 --` vào ô tìm kiếm, bấm Tìm kiếm | Lưới không hiện dòng nào, không lộ toàn bộ dữ liệu | |
| TC19 | Ký tự đặc biệt của LIKE (F6) | Nhập `%` rồi bấm Tìm kiếm | Chỉ hiện lớp có chứa ký tự `%` (không có), không hiện toàn bộ | |
| TC20 | Làm mới (F7) | Nhập dữ liệu và từ khóa, bấm Làm mới | Ô nhập và ô tìm kiếm được xóa, mã lớp được mở khóa, lưới tải lại đầy đủ | |
| TC21 | Lỗi kết nối | Dừng dịch vụ SQL Server (hoặc đổi sai `Data Source`) rồi mở Form | Hiện thông báo lỗi kết nối kèm gợi ý, chương trình không bị văng | |
