namespace COMP1019_Buoi11_Lab11
{
    partial class FrmLop
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
            grpThongTin = new GroupBox();
            lblMaLop = new Label();
            txtMaLop = new TextBox();
            lblTenLop = new Label();
            txtTenLop = new TextBox();
            lblKhoa = new Label();
            cboKhoa = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            dgvLop = new DataGridView();
            stsMain = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCount = new ToolStripStatusLabel();
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLop).BeginInit();
            stsMain.SuspendLayout();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblMaLop);
            grpThongTin.Controls.Add(txtMaLop);
            grpThongTin.Controls.Add(lblTenLop);
            grpThongTin.Controls.Add(txtTenLop);
            grpThongTin.Controls.Add(lblKhoa);
            grpThongTin.Controls.Add(cboKhoa);
            grpThongTin.Location = new Point(12, 12);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(876, 120);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin lớp học";
            // 
            // lblMaLop
            // 
            lblMaLop.AutoSize = true;
            lblMaLop.Location = new Point(20, 33);
            lblMaLop.Name = "lblMaLop";
            lblMaLop.Text = "Mã lớp:";
            // 
            // txtMaLop
            // 
            txtMaLop.Location = new Point(110, 30);
            txtMaLop.MaxLength = 20;
            txtMaLop.Name = "txtMaLop";
            txtMaLop.Size = new Size(200, 23);
            txtMaLop.TabIndex = 0;
            // 
            // lblTenLop
            // 
            lblTenLop.AutoSize = true;
            lblTenLop.Location = new Point(350, 33);
            lblTenLop.Name = "lblTenLop";
            lblTenLop.Text = "Tên lớp:";
            // 
            // txtTenLop
            // 
            txtTenLop.Location = new Point(420, 30);
            txtTenLop.MaxLength = 100;
            txtTenLop.Name = "txtTenLop";
            txtTenLop.Size = new Size(430, 23);
            txtTenLop.TabIndex = 1;
            // 
            // lblKhoa
            // 
            lblKhoa.AutoSize = true;
            lblKhoa.Location = new Point(20, 73);
            lblKhoa.Name = "lblKhoa";
            lblKhoa.Text = "Khoa:";
            // 
            // cboKhoa
            // 
            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoa.FormattingEnabled = true;
            cboKhoa.Location = new Point(110, 70);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new Size(300, 23);
            cboKhoa.TabIndex = 2;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(12, 144);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 32);
            btnThem.TabIndex = 1;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(110, 144);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 32);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(208, 144);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 32);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(306, 144);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 32);
            btnLamMoi.TabIndex = 4;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(480, 152);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Text = "Tìm mã/tên lớp:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(580, 149);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(200, 23);
            txtTimKiem.TabIndex = 5;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(790, 144);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(98, 32);
            btnTimKiem.TabIndex = 6;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvLop
            // 
            dgvLop.AllowUserToAddRows = false;
            dgvLop.AllowUserToDeleteRows = false;
            dgvLop.AllowUserToResizeRows = false;
            dgvLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLop.BackgroundColor = SystemColors.Window;
            dgvLop.Location = new Point(12, 190);
            dgvLop.MultiSelect = false;
            dgvLop.Name = "dgvLop";
            dgvLop.ReadOnly = true;
            dgvLop.RowHeadersVisible = false;
            dgvLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLop.Size = new Size(876, 340);
            dgvLop.TabIndex = 7;
            dgvLop.CellClick += dgvLop_CellClick;
            dgvLop.SelectionChanged += dgvLop_SelectionChanged;
            // 
            // stsMain
            // 
            stsMain.Items.AddRange(new ToolStripItem[] { lblStatus, lblCount });
            stsMain.Location = new Point(0, 538);
            stsMain.Name = "stsMain";
            stsMain.Size = new Size(900, 22);
            stsMain.TabIndex = 8;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Spring = true;
            lblStatus.Text = "Sẵn sàng.";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCount
            // 
            lblCount.Name = "lblCount";
            lblCount.Text = "Số lớp: 0";
            // 
            // FrmLop
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 560);
            Controls.Add(grpThongTin);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnLamMoi);
            Controls.Add(lblTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(dgvLop);
            Controls.Add(stsMain);
            MinimumSize = new Size(820, 480);
            Name = "FrmLop";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý lớp học - EduManagerDB";
            Load += FrmLop_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLop).EndInit();
            stsMain.ResumeLayout(false);
            stsMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblMaLop;
        private TextBox txtMaLop;
        private Label lblTenLop;
        private TextBox txtTenLop;
        private Label lblKhoa;
        private ComboBox cboKhoa;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private DataGridView dgvLop;
        private StatusStrip stsMain;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCount;
    }
}
