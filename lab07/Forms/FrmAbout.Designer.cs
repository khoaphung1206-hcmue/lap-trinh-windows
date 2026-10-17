namespace ShopManagerMini.Forms
{
    partial class FrmAbout
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
            lblTenUngDung = new Label();
            lblPhienBan = new Label();
            lblMonHoc = new Label();
            lblSinhVien = new Label();
            lblLop = new Label();
            btnDong = new Button();
            SuspendLayout();
            // 
            // lblTenUngDung
            // 
            lblTenUngDung.AutoSize = true;
            lblTenUngDung.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTenUngDung.Location = new Point(30, 20);
            lblTenUngDung.Name = "lblTenUngDung";
            lblTenUngDung.Text = "ShopManagerMini";
            // 
            // lblPhienBan
            // 
            lblPhienBan.AutoSize = true;
            lblPhienBan.Location = new Point(32, 58);
            lblPhienBan.Name = "lblPhienBan";
            lblPhienBan.Text = "Ứng dụng quản lý cửa hàng mini - Phiên bản 1.0";
            // 
            // lblMonHoc
            // 
            lblMonHoc.AutoSize = true;
            lblMonHoc.Location = new Point(32, 95);
            lblMonHoc.Name = "lblMonHoc";
            lblMonHoc.Text = "Học phần: COMP1019 - Lập trình trên Windows";
            // 
            // lblSinhVien
            // 
            lblSinhVien.AutoSize = true;
            lblSinhVien.Location = new Point(32, 125);
            lblSinhVien.Name = "lblSinhVien";
            lblSinhVien.Text = "Sinh viên thực hiện: Phùng Đăng Khoa - 51.01.104.046";
            // 
            // lblLop
            // 
            lblLop.AutoSize = true;
            lblLop.Location = new Point(32, 155);
            lblLop.Name = "lblLop";
            lblLop.Text = "Lớp: 51.01.CNTT.A";
            // 
            // btnDong
            // 
            btnDong.DialogResult = DialogResult.OK;
            btnDong.Location = new Point(150, 200);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(100, 34);
            btnDong.TabIndex = 0;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // FrmAbout
            // 
            AcceptButton = btnDong;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnDong;
            ClientSize = new Size(400, 255);
            Controls.Add(lblTenUngDung);
            Controls.Add(lblPhienBan);
            Controls.Add(lblMonHoc);
            Controls.Add(lblSinhVien);
            Controls.Add(lblLop);
            Controls.Add(btnDong);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAbout";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Giới thiệu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTenUngDung;
        private Label lblPhienBan;
        private Label lblMonHoc;
        private Label lblSinhVien;
        private Label lblLop;
        private Button btnDong;
    }
}
