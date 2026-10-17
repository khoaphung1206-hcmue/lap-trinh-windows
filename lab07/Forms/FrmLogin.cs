using ShopManagerMini.Data;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public partial class FrmLogin : Form
    {
        /// <summary>
        /// Người dùng đăng nhập thành công (Program đọc qua property này).
        /// </summary>
        public User LoggedInUser { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (password.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            User user = DataStore.Authenticate(username, password);
            if (user == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Đăng nhập thất bại",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            LoggedInUser = user;
            DialogResult = DialogResult.OK; // đóng form và trả về OK cho ShowDialog()
        }
    }
}
