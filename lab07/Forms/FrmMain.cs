using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public partial class FrmMain : Form
    {
        private readonly User _currentUser;
        private bool _isLoggingOut;

        /// <summary>
        /// True nếu người dùng chọn Đăng xuất (Program sẽ hiện lại FrmLogin).
        /// </summary>
        public bool LogoutRequested
        {
            get { return _isLoggingOut; }
        }

        // Nhận thông tin người dùng qua constructor
        public FrmMain(User user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Người dùng: " + _currentUser.FullName;
            lblRole.Text = "Vai trò: " + _currentUser.Role;
            UpdateClock();
            tmrClock.Start();
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Mở Form con MDI. Nếu Form cùng loại đã mở thì kích hoạt Form đó, không tạo mới.
        /// </summary>
        private void OpenChildForm<T>() where T : Form, new()
        {
            foreach (Form child in MdiChildren)
            {
                if (child is T)
                {
                    if (child.WindowState == FormWindowState.Minimized)
                    {
                        child.WindowState = FormWindowState.Normal;
                    }
                    child.Activate();
                    return;
                }
            }

            T form = new T();
            form.MdiParent = this;
            form.Show();
        }

        private void mnuQuanLySanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm<FrmProduct>();
        }

        private void mnuQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm<FrmCustomer>();
        }

        private void mnuGioiThieu_Click(object sender, EventArgs e)
        {
            using (var about = new FrmAbout())
            {
                about.ShowDialog(this);
            }
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _isLoggingOut = true;
                Close();
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Close(); // việc hỏi xác nhận được xử lý trong FormClosing
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isLoggingOut || e.CloseReason == CloseReason.WindowsShutDown) return;

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
