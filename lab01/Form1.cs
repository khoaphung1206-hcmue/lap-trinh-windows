namespace Lab01_YeuCau
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thêm các lựa chọn vào ComboBox
            cboKhoa.Items.Add("Công nghệ thông tin");
            cboKhoa.Items.Add("Quản trị kinh doanh");
            cboKhoa.Items.Add("Ngôn ngữ Anh");
            cboKhoa.Items.Add("Thiết kế đồ họa");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại hỏi người dùng
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu bấm Yes thì đóng form
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xóa trắng các TextBox
            txtHoTen.Clear();
            txtNamSinh.Clear();
            txtEmail.Clear();
            txtKetQua.Clear();

            // Bỏ chọn RadioButton
            radNam.Checked = false;
            radNu.Checked = false;

            // Đưa ComboBox về trạng thái không chọn (-1)
            cboKhoa.SelectedIndex = -1;
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            // 1. KIỂM TRA DỮ LIỆU ĐẦU VÀO
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return; // Dừng lại, không chạy code phía dưới nữa
            }

            int namHienTai = DateTime.Now.Year;
            int namSinh;
            // Kiểm tra năm sinh có phải số nguyên và nằm trong khoảng 1900 -> năm hiện tại không
            if (!int.TryParse(txtNamSinh.Text, out namSinh) || namSinh < 1900 || namSinh > namHienTai)
            {
                MessageBox.Show($"Năm sinh phải là số nguyên từ 1900 đến {namHienTai}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email không được rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (radNam.Checked == false && radNu.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khoa/Lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoa.Focus();
                return;
            }

            // 2. XỬ LÝ VÀ HIỂN THỊ KẾT QUẢ
            string hoTen = txtHoTen.Text.Trim();
            int tuoi = namHienTai - namSinh;
            string email = txtEmail.Text.Trim();
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ"; // Nếu radNam được chọn thì lấy chữ "Nam", ngược lại lấy chữ "Nữ"
            string khoa = cboKhoa.SelectedItem.ToString();

            // Ghép chuỗi và in ra txtKetQua
            txtKetQua.Text = "THÔNG TIN SINH VIÊN\r\n" +
                             "Họ tên: " + hoTen + "\r\n" +
                             "Tuổi: " + tuoi + "\r\n" +
                             "Email: " + email + "\r\n" +
                             "Giới tính: " + gioiTinh + "\r\n" +
                             "Khoa/Lớp: " + khoa;
        }
    }
}
