namespace ShopManagerMini.Forms
{
    partial class FrmMain
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
            mnuHeThong = new ToolStripMenuItem();
            mnuDangXuat = new ToolStripMenuItem();
            mnuThoat = new ToolStripMenuItem();
            mnuDanhMuc = new ToolStripMenuItem();
            mnuQuanLySanPham = new ToolStripMenuItem();
            mnuQuanLyKhachHang = new ToolStripMenuItem();
            mnuTroGiup = new ToolStripMenuItem();
            mnuGioiThieu = new ToolStripMenuItem();
            stsMain = new StatusStrip();
            lblUser = new ToolStripStatusLabel();
            lblRole = new ToolStripStatusLabel();
            lblApp = new ToolStripStatusLabel();
            lblTime = new ToolStripStatusLabel();
            tmrClock = new System.Windows.Forms.Timer(components);
            mnsMain.SuspendLayout();
            stsMain.SuspendLayout();
            SuspendLayout();
            // 
            // mnsMain
            // 
            mnsMain.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuDanhMuc, mnuTroGiup });
            mnsMain.Location = new Point(0, 0);
            mnsMain.Name = "mnsMain";
            mnsMain.Size = new Size(1000, 24);
            mnsMain.TabIndex = 0;
            // 
            // mnuHeThong
            // 
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuDangXuat, mnuThoat });
            mnuHeThong.Name = "mnuHeThong";
            mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuDangXuat
            // 
            mnuDangXuat.Name = "mnuDangXuat";
            mnuDangXuat.Text = "Đăng xuất";
            mnuDangXuat.Click += mnuDangXuat_Click;
            // 
            // mnuThoat
            // 
            mnuThoat.Name = "mnuThoat";
            mnuThoat.Text = "Thoát";
            mnuThoat.Click += mnuThoat_Click;
            // 
            // mnuDanhMuc
            // 
            mnuDanhMuc.DropDownItems.AddRange(new ToolStripItem[] { mnuQuanLySanPham, mnuQuanLyKhachHang });
            mnuDanhMuc.Name = "mnuDanhMuc";
            mnuDanhMuc.Text = "&Danh mục";
            // 
            // mnuQuanLySanPham
            // 
            mnuQuanLySanPham.Name = "mnuQuanLySanPham";
            mnuQuanLySanPham.Text = "Quản lý sản phẩm";
            mnuQuanLySanPham.Click += mnuQuanLySanPham_Click;
            // 
            // mnuQuanLyKhachHang
            // 
            mnuQuanLyKhachHang.Name = "mnuQuanLyKhachHang";
            mnuQuanLyKhachHang.Text = "Quản lý khách hàng";
            mnuQuanLyKhachHang.Click += mnuQuanLyKhachHang_Click;
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
            // stsMain
            // 
            stsMain.Items.AddRange(new ToolStripItem[] { lblUser, lblRole, lblApp, lblTime });
            stsMain.Location = new Point(0, 628);
            stsMain.Name = "stsMain";
            stsMain.Size = new Size(1000, 22);
            stsMain.TabIndex = 1;
            // 
            // lblUser
            // 
            lblUser.Name = "lblUser";
            lblUser.Text = "Người dùng:";
            // 
            // lblRole
            // 
            lblRole.Name = "lblRole";
            lblRole.Text = "Vai trò:";
            // 
            // lblApp
            // 
            lblApp.Name = "lblApp";
            lblApp.Spring = true;
            lblApp.Text = "ShopManagerMini";
            // 
            // lblTime
            // 
            lblTime.Name = "lblTime";
            lblTime.Text = "";
            // 
            // tmrClock
            // 
            tmrClock.Interval = 1000;
            tmrClock.Tick += tmrClock_Tick;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(stsMain);
            Controls.Add(mnsMain);
            IsMdiContainer = true;
            MainMenuStrip = mnsMain;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShopManagerMini - Quản lý cửa hàng mini";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            mnsMain.ResumeLayout(false);
            mnsMain.PerformLayout();
            stsMain.ResumeLayout(false);
            stsMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnsMain;
        private ToolStripMenuItem mnuHeThong;
        private ToolStripMenuItem mnuDangXuat;
        private ToolStripMenuItem mnuThoat;
        private ToolStripMenuItem mnuDanhMuc;
        private ToolStripMenuItem mnuQuanLySanPham;
        private ToolStripMenuItem mnuQuanLyKhachHang;
        private ToolStripMenuItem mnuTroGiup;
        private ToolStripMenuItem mnuGioiThieu;
        private StatusStrip stsMain;
        private ToolStripStatusLabel lblUser;
        private ToolStripStatusLabel lblRole;
        private ToolStripStatusLabel lblApp;
        private ToolStripStatusLabel lblTime;
        private System.Windows.Forms.Timer tmrClock;
    }
}
