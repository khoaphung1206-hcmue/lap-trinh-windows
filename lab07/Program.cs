using ShopManagerMini.Forms;

namespace ShopManagerMini
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Vòng lặp cho phép đăng xuất rồi đăng nhập lại
            while (true)
            {
                using (var login = new FrmLogin())
                {
                    // Form đăng nhập hiển thị dạng modal bằng ShowDialog()
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        break; // Người dùng bấm Thoát
                    }

                    // Truyền thông tin người dùng sang FrmMain qua constructor
                    using (var main = new FrmMain(login.LoggedInUser))
                    {
                        Application.Run(main);
                        if (!main.LogoutRequested)
                        {
                            break; // Thoát chương trình
                        }
                    }
                }
            }
        }
    }
}
