namespace StudentReportDemo.Forms
{
    partial class FrmBaoCaoSinhVien
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
            lblKetQua = new Label();
            cboKetQua = new ComboBox();
            lblTuKhoa = new Label();
            txtTuKhoa = new TextBox();
            btnXemBaoCao = new Button();
            btnThongKe = new Button();
            btnXuatFile = new Button();
            dgvBaoCao = new DataGridView();
            statusStrip = new StatusStrip();
            lblThongKe = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).BeginInit();
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
            cboLop.Location = new Point(60, 14);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(200, 23);
            //
            // lblKetQua
            //
            lblKetQua.AutoSize = true;
            lblKetQua.Location = new Point(280, 18);
            lblKetQua.Name = "lblKetQua";
            lblKetQua.Text = "Kết quả:";
            //
            // cboKetQua
            //
            cboKetQua.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKetQua.Location = new Point(340, 14);
            cboKetQua.Name = "cboKetQua";
            cboKetQua.Size = new Size(130, 23);
            //
            // lblTuKhoa
            //
            lblTuKhoa.AutoSize = true;
            lblTuKhoa.Location = new Point(490, 18);
            lblTuKhoa.Name = "lblTuKhoa";
            lblTuKhoa.Text = "Từ khóa:";
            //
            // txtTuKhoa
            //
            txtTuKhoa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTuKhoa.Location = new Point(550, 14);
            txtTuKhoa.Name = "txtTuKhoa";
            txtTuKhoa.PlaceholderText = "Mã SV hoặc họ tên (không phân biệt dấu)";
            txtTuKhoa.Size = new Size(298, 23);
            //
            // btnXemBaoCao
            //
            btnXemBaoCao.Location = new Point(12, 50);
            btnXemBaoCao.Name = "btnXemBaoCao";
            btnXemBaoCao.Size = new Size(130, 30);
            btnXemBaoCao.Text = "Xem báo cáo";
            btnXemBaoCao.UseVisualStyleBackColor = true;
            btnXemBaoCao.Click += btnXemBaoCao_Click;
            //
            // btnThongKe
            //
            btnThongKe.Location = new Point(150, 50);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Size = new Size(130, 30);
            btnThongKe.Text = "Thống kê theo lớp";
            btnThongKe.UseVisualStyleBackColor = true;
            btnThongKe.Click += btnThongKe_Click;
            //
            // btnXuatFile
            //
            btnXuatFile.Location = new Point(288, 50);
            btnXuatFile.Name = "btnXuatFile";
            btnXuatFile.Size = new Size(130, 30);
            btnXuatFile.Text = "Xuất file";
            btnXuatFile.UseVisualStyleBackColor = true;
            btnXuatFile.Click += btnXuatFile_Click;
            //
            // dgvBaoCao
            //
            dgvBaoCao.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBaoCao.AllowUserToAddRows = false;
            dgvBaoCao.AllowUserToDeleteRows = false;
            dgvBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBaoCao.BackgroundColor = SystemColors.Window;
            dgvBaoCao.Location = new Point(12, 92);
            dgvBaoCao.MultiSelect = false;
            dgvBaoCao.Name = "dgvBaoCao";
            dgvBaoCao.ReadOnly = true;
            dgvBaoCao.RowHeadersWidth = 30;
            dgvBaoCao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBaoCao.Size = new Size(836, 432);
            dgvBaoCao.CellFormatting += dgvBaoCao_CellFormatting;
            //
            // statusStrip
            //
            statusStrip.Items.AddRange(new ToolStripItem[] { lblThongKe });
            statusStrip.Name = "statusStrip";
            //
            // lblThongKe
            //
            lblThongKe.Name = "lblThongKe";
            lblThongKe.Text = "Sẵn sàng.";
            //
            // FrmBaoCaoSinhVien
            //
            AcceptButton = btnXemBaoCao;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 560);
            Controls.Add(lblLop);
            Controls.Add(cboLop);
            Controls.Add(lblKetQua);
            Controls.Add(cboKetQua);
            Controls.Add(lblTuKhoa);
            Controls.Add(txtTuKhoa);
            Controls.Add(btnXemBaoCao);
            Controls.Add(btnThongKe);
            Controls.Add(btnXuatFile);
            Controls.Add(dgvBaoCao);
            Controls.Add(statusStrip);
            MinimumSize = new Size(760, 420);
            Name = "FrmBaoCaoSinhVien";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo danh sách sinh viên theo lớp - Lab 13";
            Load += FrmBaoCaoSinhVien_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLop;
        private ComboBox cboLop;
        private Label lblKetQua;
        private ComboBox cboKetQua;
        private Label lblTuKhoa;
        private TextBox txtTuKhoa;
        private Button btnXemBaoCao;
        private Button btnThongKe;
        private Button btnXuatFile;
        private DataGridView dgvBaoCao;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblThongKe;
    }
}
