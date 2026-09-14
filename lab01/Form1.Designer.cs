namespace Lab01_YeuCau
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            label1 = new Label();
            txtHoTen = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtNamSinh = new TextBox();
            txtEmail = new TextBox();
            groupBox1 = new GroupBox();
            radNu = new RadioButton();
            radNam = new RadioButton();
            label4 = new Label();
            cboKhoa = new ComboBox();
            btnHienThi = new Button();
            btnXoa = new Button();
            btnThoat = new Button();
            txtKetQua = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(157, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN CÁ NHÂN";
            lblTitle.Click += label1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 35);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 1;
            label1.Text = "Họ và Tên:";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(98, 32);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(206, 27);
            txtHoTen.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 68);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 3;
            label2.Text = "Năm sinh:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 98);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 4;
            label3.Text = "Email:";
            // 
            // txtNamSinh
            // 
            txtNamSinh.Location = new Point(98, 65);
            txtNamSinh.Name = "txtNamSinh";
            txtNamSinh.Size = new Size(206, 27);
            txtNamSinh.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(98, 98);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(206, 27);
            txtEmail.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radNu);
            groupBox1.Controls.Add(radNam);
            groupBox1.Location = new Point(14, 131);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(290, 92);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Giới tính";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // radNu
            // 
            radNu.AutoSize = true;
            radNu.Location = new Point(6, 56);
            radNu.Name = "radNu";
            radNu.Size = new Size(50, 24);
            radNu.TabIndex = 1;
            radNu.TabStop = true;
            radNu.Text = "Nữ";
            radNu.UseVisualStyleBackColor = true;
            radNu.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radNam
            // 
            radNam.AutoSize = true;
            radNam.Location = new Point(6, 26);
            radNam.Name = "radNam";
            radNam.Size = new Size(62, 24);
            radNam.TabIndex = 0;
            radNam.TabStop = true;
            radNam.Text = "Nam";
            radNam.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 242);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 8;
            label4.Text = "Khoa/Lớp";
            // 
            // cboKhoa
            // 
            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoa.FormattingEnabled = true;
            cboKhoa.Location = new Point(92, 239);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new Size(210, 28);
            cboKhoa.TabIndex = 9;
            // 
            // btnHienThi
            // 
            btnHienThi.Location = new Point(14, 287);
            btnHienThi.Name = "btnHienThi";
            btnHienThi.Size = new Size(75, 29);
            btnHienThi.TabIndex = 10;
            btnHienThi.Text = "Hiển Thị";
            btnHienThi.UseVisualStyleBackColor = true;
            btnHienThi.Click += btnHienThi_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(117, 287);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 29);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(229, 287);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(75, 29);
            btnThoat.TabIndex = 12;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // txtKetQua
            // 
            txtKetQua.Location = new Point(310, 34);
            txtKetQua.Multiline = true;
            txtKetQua.Name = "txtKetQua";
            txtKetQua.Size = new Size(359, 189);
            txtKetQua.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(681, 328);
            Controls.Add(txtKetQua);
            Controls.Add(btnThoat);
            Controls.Add(btnXoa);
            Controls.Add(btnHienThi);
            Controls.Add(cboKhoa);
            Controls.Add(label4);
            Controls.Add(groupBox1);
            Controls.Add(txtEmail);
            Controls.Add(txtNamSinh);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtHoTen);
            Controls.Add(label1);
            Controls.Add(lblTitle);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label label1;
        private TextBox txtHoTen;
        private Label label2;
        private Label label3;
        private TextBox txtNamSinh;
        private TextBox txtEmail;
        private GroupBox groupBox1;
        private RadioButton radNu;
        private RadioButton radNam;
        private Label label4;
        private ComboBox cboKhoa;
        private Button btnHienThi;
        private Button btnXoa;
        private Button btnThoat;
        private TextBox txtKetQua;
    }
}
