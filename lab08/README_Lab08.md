# ỨNG DỤNG PAINT ĐƠN GIẢN (SIMPLE PAINT)

* **Học phần:** 2611COMP101904 - Lập trình trên Windows
* **Giảng viên hướng dẫn:** ThS. Lê Thanh Thoại
* **Sinh viên thực hiện:** Phùng Đăng Khoa - 51.01.104.046
* **Môi trường phát triển:** Microsoft Visual Studio 2022 - C# .NET 8 Windows Forms App
* **Tên ứng dụng:** `SimplePaint`

\---

## 1\. Giới thiệu và Kiến trúc Hệ thống

`SimplePaint` là ứng dụng vẽ đơn giản, sử dụng GDI+ (`Graphics`, `Pen`, `Brush`) trên Windows Forms. Người dùng chọn chế độ vẽ, màu, độ dày nét rồi vẽ trực tiếp lên vùng canvas bằng chuột.

* **Lưu dữ liệu gốc của nét vẽ:** mỗi nét vẽ là một đối tượng `DrawItem` (loại hình, màu, độ dày, các điểm) và được lưu trong `List<DrawItem>`. Canvas luôn vẽ lại toàn bộ danh sách trong sự kiện `Paint`, nên nét vẽ không bị mất khi Form bị resize, thu nhỏ hoặc bị che rồi hiện lại.
* **Chống nhấp nháy:** canvas là `DrawSurface` (kế thừa `Panel` với `DoubleBuffered = true`), giúp hình vẽ mượt khi kéo chuột liên tục.
* **Xử lý chuột:** `MouseDown` tạo nét mới, `MouseMove` cập nhật điểm cuối và gọi `canvas.Invalidate()` để xem trước hình theo thời gian thực, `MouseUp` chốt nét vẽ vào danh sách.
* **Vẽ hình theo mọi hướng:** hình chữ nhật và ellipse được tính từ `Math.Min` / `Math.Abs` của hai điểm đầu - cuối, nên kéo chuột theo hướng nào cũng đúng.
* **Giải phóng tài nguyên:** `Pen`, `SolidBrush`, `ColorDialog`, `OpenFileDialog`, `SaveFileDialog` đều được bọc trong `using`.

\---

## 2\. Danh mục Thành phần Điều khiển (Controls)

|Thành phần|Tên control|Chức năng chính|
|-|-|-|
|`ComboBox`|`cboMode`|Chọn chế độ vẽ: `Pen`, `Line`, `Rectangle`, `Ellipse`.|
|`NumericUpDown`|`nudWidth`|Chọn độ dày nét vẽ từ 1 đến 20.|
|`Button`|`btnColor`|Mở `ColorDialog` để chọn màu vẽ (ô vuông bên cạnh hiển thị màu đang chọn).|
|`CheckBox`|`chkFill`|Tô màu hình chữ nhật / ellipse (chỉ bật khi chọn 2 chế độ này).|
|`Button`|`btnUndo`|Hoàn tác, xóa nét vẽ gần nhất (cũng dùng được phím tắt `Ctrl + Z`).|
|`Button`|`btnClear`|Xóa toàn bộ nét vẽ sau khi hỏi xác nhận.|
|`Button`|`btnBackColor`|Đổi màu nền của canvas.|
|`Button`|`btnOpenImage` / `btnRemoveImage`|Mở ảnh nền để vẽ đè lên / bỏ ảnh nền.|
|`Button`|`btnSavePng`|Lưu toàn bộ vùng vẽ ra file PNG.|
|`Custom Panel`|`canvas`|Vùng vẽ chính (nền trắng, `DoubleBuffered`).|
|`ToolStripStatusLabel`|`lblStatus`, `lblCount`|Hiển thị tọa độ chuột (X, Y) theo thời gian thực và số nét đã vẽ.|

\---

## 3\. Cách chạy chương trình

1. Giải nén và mở file `SimplePaint.csproj` bằng Visual Studio 2022 (cần .NET 8 SDK và workload *.NET desktop development*).
2. Nhấn `F5` để chạy.
3. Chọn chế độ vẽ, màu, độ dày rồi nhấn giữ chuột trái và kéo trên vùng canvas để vẽ.

\---

## 4\. Minh chứng Thực nghiệm (Test Cases)

### 4.1. Giao diện ứng dụng

Thanh công cụ phía trên và vùng canvas bên dưới:

!\[Khởi tạo giao diện](images/1.png)

### 4.2. Vẽ đủ 4 loại hình

Nét tự do (`Pen`), đoạn thẳng (`Line`), hình chữ nhật (`Rectangle`) và hình ellipse (`Ellipse`):

!\[Vẽ đủ 4 loại hình](images/2.png)

### 4.3. Chọn màu bằng `ColorDialog`

!\[Bảng màu ColorDialog](images/3.png)

### 4.4. Hoàn tác (`Undo`)

* Trước khi Undo:

  !\[Undo trước](images/4.png)

* Sau khi Undo (nét vẽ gần nhất bị xóa):

  !\[Undo sau](images/5.png)

### 4.5. Xóa toàn bộ (`Clear`)

* Trước khi xóa:

  !\[Clear trước](images/6.png)

* Sau khi xóa:

  !\[Clear sau](images/7.png)

### 4.6. Tô màu hình chữ nhật / ellipse (Chức năng nâng cao)

Bật `Tô màu hình` khi chọn `Rectangle` hoặc `Ellipse`:

!\[Tô màu hình](images/8.png)

### 4.7. Đổi màu nền canvas (Chức năng nâng cao)

!\[Đổi màu nền canvas](images/9.png)

### 4.8. Mở ảnh nền để vẽ đè (Chức năng nâng cao)

Ảnh được căn giữa, giữ đúng tỉ lệ; các nét vẽ nằm phía trên ảnh:

!\[Ảnh nền](images/10.png)

### 4.9. Lưu bản vẽ ra file PNG (Chức năng nâng cao)

!\[Lưu ảnh PNG](images/11.png)

### 4.10. Phím tắt `Ctrl + Z` (Chức năng nâng cao)

`Ctrl + Z` có tác dụng giống nút Undo:

!\[Ctrl+Z trước](images/12.png)

!\[Ctrl+Z sau](images/13.png)

### 4.11. Hiển thị tọa độ chuột

Thanh trạng thái hiển thị tọa độ con trỏ trên canvas và số nét đã vẽ:

!\[Hiển thị tọa độ chuột](images/ratio.png)

