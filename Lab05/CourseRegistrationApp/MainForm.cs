using System.Globalization;

namespace CourseRegistrationApp
{
    public partial class MainForm : Form
    {
        private static readonly CultureInfo ViCulture = new CultureInfo("vi-VN");

        public MainForm()
        {
            InitializeComponent();
        }

        // ================== FORM LOAD ==================
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Nạp danh sách khóa học
            cboKhoaHoc.Items.Clear();
            cboKhoaHoc.Items.Add(new Course("C# WinForms cơ bản", 800000));
            cboKhoaHoc.Items.Add(new Course("SQL Server cơ bản", 700000));
            cboKhoaHoc.Items.Add(new Course("Web Frontend cơ bản", 750000));
            cboKhoaHoc.Items.Add(new Course("Lập trình Python cơ bản", 650000));

            // Thiết lập số tháng 1 - 12
            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;

            // Giá trị mặc định
            cboKhoaHoc.SelectedIndex = 0;
            radOnline.Checked = true;

            CapNhatTongTien();
        }

        // ================== TÍNH HỌC PHÍ ==================
        private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private void numSoThang_ValueChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private decimal TinhTongTien()
        {
            if (cboKhoaHoc.SelectedItem is Course course)
            {
                return course.PricePerMonth * numSoThang.Value;
            }
            return 0;
        }

        private void CapNhatTongTien()
        {
            lblTongTien.Text = DinhDangTien(TinhTongTien());
        }

        private static string DinhDangTien(decimal soTien)
        {
            return soTien.ToString("N0", ViCulture) + " VNĐ";
        }

        // ================== NÚT ĐĂNG KÝ ==================
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên học viên!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return;
            }

            if (cboKhoaHoc.SelectedItem is not Course course)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return;
            }

            // 2. Tính tổng học phí = học phí 1 tháng x số tháng
            decimal tongTien = course.PricePerMonth * numSoThang.Value;

            // 3. Chuẩn bị nội dung phiếu
            string hinhThuc = radOnline.Checked ? "Online" : "Trực tiếp (Offline)";
            string nhanEmail = chkNhanEmail.Checked ? "Có" : "Không";

            string phieu =
                "PHIẾU ĐĂNG KÝ KHÓA HỌC\n" +
                "-----------------------------------\n" +
                $"Họ tên:            {txtHoTen.Text.Trim()}\n" +
                $"Số điện thoại:  {txtSoDienThoai.Text.Trim()}\n" +
                $"Ngày sinh:       {dtpNgaySinh.Value:dd/MM/yyyy}\n" +
                $"Khóa học:         {course.Name}\n" +
                $"Hình thức học: {hinhThuc}\n" +
                $"Số tháng:          {numSoThang.Value:0}\n" +
                $"Tổng tiền:         {DinhDangTien(tongTien)}\n" +
                $"Nhận email:      {nhanEmail}";

            MessageBox.Show(phieu, "Đăng ký thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================== NÚT LÀM MỚI ==================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            chkNhanEmail.Checked = false;
            cboKhoaHoc.SelectedIndex = 0;
            radOnline.Checked = true;
            numSoThang.Value = 1;
            CapNhatTongTien();
            txtHoTen.Focus();
        }

        // ================== NÚT THOÁT ==================
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void lblSoThang_Click(object sender, EventArgs e)
        {

        }
    }
}
