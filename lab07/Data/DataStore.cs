using ShopManagerMini.Models;

namespace ShopManagerMini.Data
{
    /// <summary>
    /// Nơi lưu dữ liệu tạm trong bộ nhớ, dùng chung cho tất cả các Form.
    /// Nhờ vậy khi đóng rồi mở lại Form con, dữ liệu vẫn còn.
    /// </summary>
    public static class DataStore
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User("admin", "123", "Quản trị viên", "Admin"),
            new User("user", "123", "Nhân viên bán hàng", "User")
        };

        public static List<Product> Products { get; } = new List<Product>
        {
            new Product("SP001", "Laptop Dell Inspiron 15", "Điện tử", 15500000, 10),
            new Product("SP002", "Bút bi Thiên Long", "Văn phòng", 5000, 200),
            new Product("SP003", "Nồi cơm điện Sharp", "Gia dụng", 1200000, 25),
            new Product("SP004", "Áo thun nam", "Thời trang", 180000, 60)
        };

        public static List<Customer> Customers { get; } = new List<Customer>
        {
            new Customer("KH001", "Nguyễn Văn An", "0901234567", "VIP"),
            new Customer("KH002", "Trần Thị Bình", "0912345678", "Thân thiết"),
            new Customer("KH003", "Lê Hoàng Cường", "0987654321", "Thường")
        };

        /// <summary>
        /// Kiểm tra tài khoản. Trả về User nếu đúng, null nếu sai.
        /// </summary>
        public static User Authenticate(string username, string password)
        {
            return Users.FirstOrDefault(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
    }
}
