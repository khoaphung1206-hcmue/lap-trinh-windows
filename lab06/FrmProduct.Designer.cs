namespace ProductManager
{
    partial class FrmProduct
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
            components = new System.ComponentModel.Container();
            mnsMain = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuLamMoi = new ToolStripMenuItem();
            mnuXuat = new ToolStripMenuItem();
            sepFile = new ToolStripSeparator();
            mnuThoat = new ToolStripMenuItem();
            mnuQuanLy = new ToolStripMenuItem();
            mnuThem = new ToolStripMenuItem();
            mnuSua = new ToolStripMenuItem();
            mnuXoa = new ToolStripMenuItem();
            mnuTimKiem = new ToolStripMenuItem();
            mnuTroGiup = new ToolStripMenuItem();
            mnuGioiThieu = new ToolStripMenuItem();
            tlsMain = new ToolStrip();
            tsbThem = new ToolStripButton();
            tsbSua = new ToolStripButton();
            tsbXoa = new ToolStripButton();
            tsbLamMoi = new ToolStripButton();
            sepTool = new ToolStripSeparator();
            lblTimKiem = new ToolStripLabel();
            txtTimKiem = new ToolStripTextBox();
            tsbTimKiem = new ToolStripButton();
            stsMain = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCount = new ToolStripStatusLabel();
            lblTongGiaTri = new ToolStripStatusLabel();
            grpThongTin = new GroupBox();
            lblMaSP = new Label();
            txtMaSP = new TextBox();
            lblTenSP = new Label();
            txtTenSP = new TextBox();
            lblLoaiSP = new Label();
            cboLoaiSP = new ComboBox();
            lblDonGia = new Label();
            numDonGia = new NumericUpDown();
            lblSoLuong = new Label();
            numSoLuong = new NumericUpDown();
            lblNgayNhap = new Label();
            dtpNgayNhap = new DateTimePicker();
            chkConKinhDoanh = new CheckBox();
            picAnh = new PictureBox();
            btnChonAnh = new Button();
            dgvProducts = new DataGridView();
            cmsProduct = new ContextMenuStrip(components);
            mnuCtxSua = new ToolStripMenuItem();
            mnuCtxXoa = new ToolStripMenuItem();
            mnuCtxXemChiTiet = new ToolStripMenuItem();
            errorProvider = new ErrorProvider(components);
            dlgOpenImage = new OpenFileDialog();
            dlgSaveFile = new SaveFileDialog();
            mnsMain.SuspendLayout();
            tlsMain.SuspendLayout();
            stsMain.SuspendLayout();
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDonGia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAnh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            cmsProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // mnsMain
            // 
            mnsMain.Items.AddRange(new ToolStripItem[] { mnuFile, mnuQuanLy, mnuTroGiup });
            mnsMain.Location = new Point(0, 0);
            mnsMain.Name = "mnsMain";
            mnsMain.Size = new Size(1000, 24);
            mnsMain.TabIndex = 4;
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuLamMoi, mnuXuat, sepFile, mnuThoat });
            mnuFile.Name = "mnuFile";
            mnuFile.Text = "&File";
            // 
            // mnuLamMoi
            // 
            mnuLamMoi.Name = "mnuLamMoi";
            mnuLamMoi.ShortcutKeys = Keys.F5;
            mnuLamMoi.Text = "Làm mới";
            mnuLamMoi.Click += LamMoi_Click;
            // 
            // mnuXuat
            // 
            mnuXuat.Name = "mnuXuat";
            mnuXuat.Text = "Xuất danh sách ra file .txt...";
            mnuXuat.Click += mnuXuat_Click;
            // 
            // sepFile
            // 
            sepFile.Name = "sepFile";
            // 
            // mnuThoat
            // 
            mnuThoat.Name = "mnuThoat";
            mnuThoat.Text = "Thoát";
            mnuThoat.Click += mnuThoat_Click;
            // 
            // mnuQuanLy
            // 
            mnuQuanLy.DropDownItems.AddRange(new ToolStripItem[] { mnuThem, mnuSua, mnuXoa, mnuTimKiem });
            mnuQuanLy.Name = "mnuQuanLy";
            mnuQuanLy.Text = "&Quản lý";
            // 
            // mnuThem
            // 
            mnuThem.Name = "mnuThem";
            mnuThem.ShortcutKeys = Keys.Control | Keys.N;
            mnuThem.Text = "Thêm sản phẩm";
            mnuThem.Click += Them_Click;
            // 
            // mnuSua
            // 
            mnuSua.Name = "mnuSua";
            mnuSua.ShortcutKeys = Keys.Control | Keys.E;
            mnuSua.Text = "Sửa sản phẩm";
            mnuSua.Click += Sua_Click;
            // 
            // mnuXoa
            // 
            mnuXoa.Name = "mnuXoa";
            mnuXoa.ShortcutKeys = Keys.Control | Keys.D;
            mnuXoa.Text = "Xóa sản phẩm";
            mnuXoa.Click += Xoa_Click;
            // 
            // mnuTimKiem
            // 
            mnuTimKiem.Name = "mnuTimKiem";
            mnuTimKiem.ShortcutKeys = Keys.Control | Keys.F;
            mnuTimKiem.Text = "Tìm kiếm";
            mnuTimKiem.Click += mnuTimKiem_Click;
            // 
            // mnuTroGiup
            // 
            mnuTroGiup.DropDownItems.AddRange(new ToolStripItem[] { mnuGioiThieu });
            mnuTroGiup.Name = "mnuTroGiup";
            mnuTroGiup.Text = "&Trợ giúp";
            // 
            // mnuGioiThieu
            // 
            mnuGioiThieu.Name = "mnuGioiThieu";
            mnuGioiThieu.Text = "Giới thiệu";
            mnuGioiThieu.Click += mnuGioiThieu_Click;
            // 
            // tlsMain
            // 
            tlsMain.Items.AddRange(new ToolStripItem[] { tsbThem, tsbSua, tsbXoa, tsbLamMoi, sepTool, lblTimKiem, txtTimKiem, tsbTimKiem });
            tlsMain.Location = new Point(0, 24);
            tlsMain.Name = "tlsMain";
            tlsMain.Size = new Size(1000, 27);
            tlsMain.TabIndex = 3;
            // 
            // tsbThem
            // 
            tsbThem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbThem.Name = "tsbThem";
            tsbThem.Text = "Thêm";
            tsbThem.Click += Them_Click;
            // 
            // tsbSua
            // 
            tsbSua.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbSua.Name = "tsbSua";
            tsbSua.Text = "Sửa";
            tsbSua.Click += Sua_Click;
            // 
            // tsbXoa
            // 
            tsbXoa.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbXoa.Name = "tsbXoa";
            tsbXoa.Text = "Xóa";
            tsbXoa.Click += Xoa_Click;
            // 
            // tsbLamMoi
            // 
            tsbLamMoi.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbLamMoi.Name = "tsbLamMoi";
            tsbLamMoi.Text = "Làm mới";
            tsbLamMoi.Click += LamMoi_Click;
            // 
            // sepTool
            // 
            sepTool.Name = "sepTool";
            // 
            // lblTimKiem
            // 
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Text = "Tìm theo mã/tên:";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(200, 27);
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            // 
            // tsbTimKiem
            // 
            tsbTimKiem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbTimKiem.Name = "tsbTimKiem";
            tsbTimKiem.Text = "Tìm kiếm";
            tsbTimKiem.Click += TimKiem_Click;
            // 
            // stsMain
            // 
            stsMain.Items.AddRange(new ToolStripItem[] { lblStatus, lblCount, lblTongGiaTri });
            stsMain.Location = new Point(0, 628);
            stsMain.Name = "stsMain";
            stsMain.Size = new Size(1000, 22);
            stsMain.TabIndex = 2;
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
            lblCount.Text = "Số sản phẩm: 0";
            // 
            // lblTongGiaTri
            // 
            lblTongGiaTri.Name = "lblTongGiaTri";
            lblTongGiaTri.Text = "Tổng giá trị tồn kho: 0 VNĐ";
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblMaSP);
            grpThongTin.Controls.Add(txtMaSP);
            grpThongTin.Controls.Add(lblTenSP);
            grpThongTin.Controls.Add(txtTenSP);
            grpThongTin.Controls.Add(lblLoaiSP);
            grpThongTin.Controls.Add(cboLoaiSP);
            grpThongTin.Controls.Add(lblDonGia);
            grpThongTin.Controls.Add(numDonGia);
            grpThongTin.Controls.Add(lblSoLuong);
            grpThongTin.Controls.Add(numSoLuong);
            grpThongTin.Controls.Add(lblNgayNhap);
            grpThongTin.Controls.Add(dtpNgayNhap);
            grpThongTin.Controls.Add(chkConKinhDoanh);
            grpThongTin.Controls.Add(picAnh);
            grpThongTin.Controls.Add(btnChonAnh);
            grpThongTin.Dock = DockStyle.Top;
            grpThongTin.Location = new Point(0, 51);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(1000, 250);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin sản phẩm";
            // 
            // lblMaSP
            // 
            lblMaSP.AutoSize = true;
            lblMaSP.Location = new Point(20, 33);
            lblMaSP.Name = "lblMaSP";
            lblMaSP.Text = "Mã sản phẩm:";
            // 
            // txtMaSP
            // 
            txtMaSP.Location = new Point(130, 30);
            txtMaSP.Name = "txtMaSP";
            txtMaSP.Size = new Size(210, 23);
            txtMaSP.TabIndex = 0;
            // 
            // lblTenSP
            // 
            lblTenSP.AutoSize = true;
            lblTenSP.Location = new Point(20, 73);
            lblTenSP.Name = "lblTenSP";
            lblTenSP.Text = "Tên sản phẩm:";
            // 
            // txtTenSP
            // 
            txtTenSP.Location = new Point(130, 70);
            txtTenSP.Name = "txtTenSP";
            txtTenSP.Size = new Size(210, 23);
            txtTenSP.TabIndex = 1;
            // 
            // lblLoaiSP
            // 
            lblLoaiSP.AutoSize = true;
            lblLoaiSP.Location = new Point(20, 113);
            lblLoaiSP.Name = "lblLoaiSP";
            lblLoaiSP.Text = "Loại sản phẩm:";
            // 
            // cboLoaiSP
            // 
            cboLoaiSP.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiSP.FormattingEnabled = true;
            cboLoaiSP.Location = new Point(130, 110);
            cboLoaiSP.Name = "cboLoaiSP";
            cboLoaiSP.Size = new Size(210, 23);
            cboLoaiSP.TabIndex = 2;
            // 
            // lblDonGia
            // 
            lblDonGia.AutoSize = true;
            lblDonGia.Location = new Point(370, 33);
            lblDonGia.Name = "lblDonGia";
            lblDonGia.Text = "Đơn giá (VNĐ):";
            // 
            // numDonGia
            // 
            numDonGia.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numDonGia.Location = new Point(480, 30);
            numDonGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numDonGia.Name = "numDonGia";
            numDonGia.Size = new Size(150, 23);
            numDonGia.TabIndex = 3;
            numDonGia.ThousandsSeparator = true;
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(370, 73);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Text = "Số lượng:";
            // 
            // numSoLuong
            // 
            numSoLuong.Location = new Point(480, 70);
            numSoLuong.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numSoLuong.Name = "numSoLuong";
            numSoLuong.Size = new Size(150, 23);
            numSoLuong.TabIndex = 4;
            numSoLuong.ThousandsSeparator = true;
            // 
            // lblNgayNhap
            // 
            lblNgayNhap.AutoSize = true;
            lblNgayNhap.Location = new Point(370, 113);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Text = "Ngày nhập:";
            // 
            // dtpNgayNhap
            // 
            dtpNgayNhap.Format = DateTimePickerFormat.Short;
            dtpNgayNhap.Location = new Point(480, 110);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(150, 23);
            dtpNgayNhap.TabIndex = 5;
            // 
            // chkConKinhDoanh
            // 
            chkConKinhDoanh.AutoSize = true;
            chkConKinhDoanh.Checked = true;
            chkConKinhDoanh.CheckState = CheckState.Checked;
            chkConKinhDoanh.Location = new Point(130, 155);
            chkConKinhDoanh.Name = "chkConKinhDoanh";
            chkConKinhDoanh.TabIndex = 6;
            chkConKinhDoanh.Text = "Còn kinh doanh";
            chkConKinhDoanh.UseVisualStyleBackColor = true;
            // 
            // picAnh
            // 
            picAnh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picAnh.BorderStyle = BorderStyle.FixedSingle;
            picAnh.Location = new Point(780, 25);
            picAnh.Name = "picAnh";
            picAnh.Size = new Size(170, 170);
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picAnh.TabStop = false;
            // 
            // btnChonAnh
            // 
            btnChonAnh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnChonAnh.Location = new Point(780, 205);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(170, 32);
            btnChonAnh.TabIndex = 7;
            btnChonAnh.Text = "Chọn ảnh...";
            btnChonAnh.UseVisualStyleBackColor = true;
            btnChonAnh.Click += ChonAnh_Click;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = SystemColors.Window;
            dgvProducts.ContextMenuStrip = cmsProduct;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(0, 301);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1000, 327);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellClick += dgvProducts_CellClick;
            dgvProducts.CellDoubleClick += dgvProducts_CellDoubleClick;
            dgvProducts.CellMouseDown += dgvProducts_CellMouseDown;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            dgvProducts.KeyDown += dgvProducts_KeyDown;
            // 
            // cmsProduct
            // 
            cmsProduct.Items.AddRange(new ToolStripItem[] { mnuCtxSua, mnuCtxXoa, mnuCtxXemChiTiet });
            cmsProduct.Name = "cmsProduct";
            // 
            // mnuCtxSua
            // 
            mnuCtxSua.Name = "mnuCtxSua";
            mnuCtxSua.Text = "Sửa";
            mnuCtxSua.Click += mnuCtxSua_Click;
            // 
            // mnuCtxXoa
            // 
            mnuCtxXoa.Name = "mnuCtxXoa";
            mnuCtxXoa.Text = "Xóa";
            mnuCtxXoa.Click += Xoa_Click;
            // 
            // mnuCtxXemChiTiet
            // 
            mnuCtxXemChiTiet.Name = "mnuCtxXemChiTiet";
            mnuCtxXemChiTiet.Text = "Xem chi tiết";
            mnuCtxXemChiTiet.Click += XemChiTiet_Click;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // dlgOpenImage
            // 
            dlgOpenImage.FileName = "";
            // 
            // FrmProduct
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(dgvProducts);
            Controls.Add(grpThongTin);
            Controls.Add(stsMain);
            Controls.Add(tlsMain);
            Controls.Add(mnsMain);
            MainMenuStrip = mnsMain;
            MinimumSize = new Size(900, 600);
            Name = "FrmProduct";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sản phẩm";
            FormClosing += FrmProduct_FormClosing;
            Load += FrmProduct_Load;
            mnsMain.ResumeLayout(false);
            mnsMain.PerformLayout();
            tlsMain.ResumeLayout(false);
            tlsMain.PerformLayout();
            stsMain.ResumeLayout(false);
            stsMain.PerformLayout();
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDonGia).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAnh).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            cmsProduct.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnsMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuLamMoi;
        private ToolStripMenuItem mnuXuat;
        private ToolStripSeparator sepFile;
        private ToolStripMenuItem mnuThoat;
        private ToolStripMenuItem mnuQuanLy;
        private ToolStripMenuItem mnuThem;
        private ToolStripMenuItem mnuSua;
        private ToolStripMenuItem mnuXoa;
        private ToolStripMenuItem mnuTimKiem;
        private ToolStripMenuItem mnuTroGiup;
        private ToolStripMenuItem mnuGioiThieu;
        private ToolStrip tlsMain;
        private ToolStripButton tsbThem;
        private ToolStripButton tsbSua;
        private ToolStripButton tsbXoa;
        private ToolStripButton tsbLamMoi;
        private ToolStripSeparator sepTool;
        private ToolStripLabel lblTimKiem;
        private ToolStripTextBox txtTimKiem;
        private ToolStripButton tsbTimKiem;
        private StatusStrip stsMain;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCount;
        private ToolStripStatusLabel lblTongGiaTri;
        private GroupBox grpThongTin;
        private Label lblMaSP;
        private TextBox txtMaSP;
        private Label lblTenSP;
        private TextBox txtTenSP;
        private Label lblLoaiSP;
        private ComboBox cboLoaiSP;
        private Label lblDonGia;
        private NumericUpDown numDonGia;
        private Label lblSoLuong;
        private NumericUpDown numSoLuong;
        private Label lblNgayNhap;
        private DateTimePicker dtpNgayNhap;
        private CheckBox chkConKinhDoanh;
        private PictureBox picAnh;
        private Button btnChonAnh;
        private DataGridView dgvProducts;
        private ContextMenuStrip cmsProduct;
        private ToolStripMenuItem mnuCtxSua;
        private ToolStripMenuItem mnuCtxXoa;
        private ToolStripMenuItem mnuCtxXemChiTiet;
        private ErrorProvider errorProvider;
        private OpenFileDialog dlgOpenImage;
        private SaveFileDialog dlgSaveFile;
    }
}
