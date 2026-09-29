namespace CourseRegistrationApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpHocVien = new GroupBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblSoDienThoai = new Label();
            txtSoDienThoai = new TextBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            chkNhanEmail = new CheckBox();
            grpKhoaHoc = new GroupBox();
            lblKhoaHoc = new Label();
            cboKhoaHoc = new ComboBox();
            lblHinhThuc = new Label();
            radOnline = new RadioButton();
            radOffline = new RadioButton();
            lblSoThang = new Label();
            numSoThang = new NumericUpDown();
            lblTongTienTieuDe = new Label();
            lblTongTien = new Label();
            btnDangKy = new Button();
            btnLamMoi = new Button();
            btnThoat = new Button();
            grpHocVien.SuspendLayout();
            grpKhoaHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSoThang).BeginInit();
            SuspendLayout();
            // 
            // grpHocVien
            // 
            grpHocVien.Controls.Add(lblHoTen);
            grpHocVien.Controls.Add(txtHoTen);
            grpHocVien.Controls.Add(lblSoDienThoai);
            grpHocVien.Controls.Add(txtSoDienThoai);
            grpHocVien.Controls.Add(lblNgaySinh);
            grpHocVien.Controls.Add(dtpNgaySinh);
            grpHocVien.Controls.Add(chkNhanEmail);
            grpHocVien.Location = new Point(12, 12);
            grpHocVien.Name = "grpHocVien";
            grpHocVien.Size = new Size(520, 170);
            grpHocVien.TabIndex = 0;
            grpHocVien.TabStop = false;
            grpHocVien.Text = "Thông tin học viên";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(20, 33);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(57, 20);
            lblHoTen.TabIndex = 0;
            lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(140, 30);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(360, 27);
            txtHoTen.TabIndex = 0;
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Location = new Point(20, 68);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(100, 20);
            lblSoDienThoai.TabIndex = 1;
            lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Location = new Point(140, 65);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.Size = new Size(360, 27);
            txtSoDienThoai.TabIndex = 1;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(20, 103);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(77, 20);
            lblNgaySinh.TabIndex = 2;
            lblNgaySinh.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(140, 100);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(160, 27);
            dtpNgaySinh.TabIndex = 2;
            // 
            // chkNhanEmail
            // 
            chkNhanEmail.AutoSize = true;
            chkNhanEmail.Location = new Point(140, 135);
            chkNhanEmail.Name = "chkNhanEmail";
            chkNhanEmail.Size = new Size(180, 24);
            chkNhanEmail.TabIndex = 3;
            chkNhanEmail.Text = "Nhận email thông báo";
            chkNhanEmail.UseVisualStyleBackColor = true;
            // 
            // grpKhoaHoc
            // 
            grpKhoaHoc.Controls.Add(lblKhoaHoc);
            grpKhoaHoc.Controls.Add(cboKhoaHoc);
            grpKhoaHoc.Controls.Add(lblHinhThuc);
            grpKhoaHoc.Controls.Add(radOnline);
            grpKhoaHoc.Controls.Add(radOffline);
            grpKhoaHoc.Controls.Add(lblSoThang);
            grpKhoaHoc.Controls.Add(numSoThang);
            grpKhoaHoc.Controls.Add(lblTongTienTieuDe);
            grpKhoaHoc.Controls.Add(lblTongTien);
            grpKhoaHoc.Location = new Point(12, 192);
            grpKhoaHoc.Name = "grpKhoaHoc";
            grpKhoaHoc.Size = new Size(520, 190);
            grpKhoaHoc.TabIndex = 1;
            grpKhoaHoc.TabStop = false;
            grpKhoaHoc.Text = "Thông tin khóa học";
            // 
            // lblKhoaHoc
            // 
            lblKhoaHoc.AutoSize = true;
            lblKhoaHoc.Location = new Point(20, 33);
            lblKhoaHoc.Name = "lblKhoaHoc";
            lblKhoaHoc.Size = new Size(74, 20);
            lblKhoaHoc.TabIndex = 0;
            lblKhoaHoc.Text = "Khóa học:";
            // 
            // cboKhoaHoc
            // 
            cboKhoaHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoaHoc.FormattingEnabled = true;
            cboKhoaHoc.Location = new Point(140, 30);
            cboKhoaHoc.Name = "cboKhoaHoc";
            cboKhoaHoc.Size = new Size(360, 28);
            cboKhoaHoc.TabIndex = 0;
            cboKhoaHoc.SelectedIndexChanged += cboKhoaHoc_SelectedIndexChanged;
            // 
            // lblHinhThuc
            // 
            lblHinhThuc.AutoSize = true;
            lblHinhThuc.Location = new Point(20, 70);
            lblHinhThuc.Name = "lblHinhThuc";
            lblHinhThuc.Size = new Size(104, 20);
            lblHinhThuc.TabIndex = 1;
            lblHinhThuc.Text = "Hình thức học:";
            // 
            // radOnline
            // 
            radOnline.AutoSize = true;
            radOnline.Checked = true;
            radOnline.Location = new Point(140, 68);
            radOnline.Name = "radOnline";
            radOnline.Size = new Size(73, 24);
            radOnline.TabIndex = 1;
            radOnline.TabStop = true;
            radOnline.Text = "Online";
            radOnline.UseVisualStyleBackColor = true;
            // 
            // radOffline
            // 
            radOffline.AutoSize = true;
            radOffline.Location = new Point(260, 68);
            radOffline.Name = "radOffline";
            radOffline.Size = new Size(88, 24);
            radOffline.TabIndex = 2;
            radOffline.Text = "Trực tiếp";
            radOffline.UseVisualStyleBackColor = true;
            // 
            // lblSoThang
            // 
            lblSoThang.AutoSize = true;
            lblSoThang.Location = new Point(20, 108);
            lblSoThang.Name = "lblSoThang";
            lblSoThang.Size = new Size(109, 20);
            lblSoThang.TabIndex = 3;
            lblSoThang.Text = "Tháng đăng ký:";
            lblSoThang.Click += lblSoThang_Click;
            // 
            // numSoThang
            // 
            numSoThang.Location = new Point(140, 106);
            numSoThang.Name = "numSoThang";
            numSoThang.Size = new Size(80, 27);
            numSoThang.TabIndex = 3;
            numSoThang.ValueChanged += numSoThang_ValueChanged;
            // 
            // lblTongTienTieuDe
            // 
            lblTongTienTieuDe.AutoSize = true;
            lblTongTienTieuDe.Location = new Point(20, 148);
            lblTongTienTieuDe.Name = "lblTongTienTieuDe";
            lblTongTienTieuDe.Size = new Size(99, 20);
            lblTongTienTieuDe.TabIndex = 4;
            lblTongTienTieuDe.Text = "Tổng học phí:";
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Firebrick;
            lblTongTien.Location = new Point(140, 145);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(74, 28);
            lblTongTien.TabIndex = 5;
            lblTongTien.Text = "0 VNĐ";
            // 
            // btnDangKy
            // 
            btnDangKy.Location = new Point(92, 395);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(110, 38);
            btnDangKy.TabIndex = 2;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(217, 395);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 38);
            btnLamMoi.TabIndex = 3;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(342, 395);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(110, 38);
            btnThoat.TabIndex = 4;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(544, 450);
            Controls.Add(grpHocVien);
            Controls.Add(grpKhoaHoc);
            Controls.Add(btnDangKy);
            Controls.Add(btnLamMoi);
            Controls.Add(btnThoat);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ĐĂNG KÝ KHÓA HỌC";
            Load += MainForm_Load;
            grpHocVien.ResumeLayout(false);
            grpHocVien.PerformLayout();
            grpKhoaHoc.ResumeLayout(false);
            grpKhoaHoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSoThang).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpHocVien;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblSoDienThoai;
        private TextBox txtSoDienThoai;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private CheckBox chkNhanEmail;
        private GroupBox grpKhoaHoc;
        private Label lblKhoaHoc;
        private ComboBox cboKhoaHoc;
        private Label lblHinhThuc;
        private RadioButton radOnline;
        private RadioButton radOffline;
        private Label lblSoThang;
        private NumericUpDown numSoThang;
        private Label lblTongTienTieuDe;
        private Label lblTongTien;
        private Button btnDangKy;
        private Button btnLamMoi;
        private Button btnThoat;
    }
}
