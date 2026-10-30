namespace QLSinhVienLab12.Forms
{
    partial class FrmNhapDiemTheoLop
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
            lblLop = new Label();
            cboLop = new ComboBox();
            lblMonHoc = new Label();
            cboMonHoc = new ComboBox();
            btnTaiDanhSach = new Button();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            dgvKetQua = new DataGridView();
            btnLuuTatCa = new Button();
            statusStrip = new StatusStrip();
            lblTrangThai = new ToolStripStatusLabel();
            lblThongKe = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            //
            // lblLop
            //
            lblLop.AutoSize = true;
            lblLop.Location = new Point(12, 18);
            lblLop.Name = "lblLop";
            lblLop.Text = "Lớp:";
            //
            // cboLop
            //
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(80, 14);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(210, 23);
            //
            // lblMonHoc
            //
            lblMonHoc.AutoSize = true;
            lblMonHoc.Location = new Point(305, 18);
            lblMonHoc.Name = "lblMonHoc";
            lblMonHoc.Text = "Môn học:";
            //
            // cboMonHoc
            //
            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.Location = new Point(375, 14);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(235, 23);
            //
            // btnTaiDanhSach
            //
            btnTaiDanhSach.Location = new Point(628, 12);
            btnTaiDanhSach.Name = "btnTaiDanhSach";
            btnTaiDanhSach.Size = new Size(120, 28);
            btnTaiDanhSach.Text = "Tải danh sách";
            btnTaiDanhSach.UseVisualStyleBackColor = true;
            btnTaiDanhSach.Click += btnTaiDanhSach_Click;
            //
            // lblTimKiem
            //
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(12, 56);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Text = "Tìm kiếm:";
            //
            // txtTimKiem
            //
            txtTimKiem.Location = new Point(80, 52);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập mã SV hoặc họ tên để lọc";
            txtTimKiem.Size = new Size(260, 23);
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            //
            // dgvKetQua
            //
            dgvKetQua.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKetQua.AllowUserToAddRows = false;
            dgvKetQua.AllowUserToDeleteRows = false;
            dgvKetQua.BackgroundColor = SystemColors.Window;
            dgvKetQua.Location = new Point(12, 88);
            dgvKetQua.MultiSelect = false;
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.RowHeadersWidth = 30;
            dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKetQua.Size = new Size(736, 340);
            dgvKetQua.CellEndEdit += dgvKetQua_CellEndEdit;
            dgvKetQua.DataError += dgvKetQua_DataError;
            //
            // btnLuuTatCa
            //
            btnLuuTatCa.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLuuTatCa.Location = new Point(610, 438);
            btnLuuTatCa.Name = "btnLuuTatCa";
            btnLuuTatCa.Size = new Size(138, 36);
            btnLuuTatCa.Text = "Lưu tất cả";
            btnLuuTatCa.UseVisualStyleBackColor = true;
            btnLuuTatCa.Click += btnLuuTatCa_Click;
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new ToolStripItem[] { lblTrangThai, lblThongKe });
            statusStrip.Name = "statusStrip";
            //
            // lblTrangThai
            //
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Spring = true;
            lblTrangThai.Text = "Sẵn sàng.";
            lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblThongKe
            //
            lblThongKe.Name = "lblThongKe";
            lblThongKe.Text = "";
            //
            // FrmNhapDiemTheoLop
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 510);
            Controls.Add(lblLop);
            Controls.Add(cboLop);
            Controls.Add(lblMonHoc);
            Controls.Add(cboMonHoc);
            Controls.Add(btnTaiDanhSach);
            Controls.Add(lblTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(dgvKetQua);
            Controls.Add(btnLuuTatCa);
            Controls.Add(statusStrip);
            MinimumSize = new Size(640, 400);
            Name = "FrmNhapDiemTheoLop";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nhập điểm theo lớp - Lab 12";
            Load += FrmNhapDiemTheoLop_Load;
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLop;
        private ComboBox cboLop;
        private Label lblMonHoc;
        private ComboBox cboMonHoc;
        private Button btnTaiDanhSach;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private DataGridView dgvKetQua;
        private Button btnLuuTatCa;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblTrangThai;
        private ToolStripStatusLabel lblThongKe;
    }
}
