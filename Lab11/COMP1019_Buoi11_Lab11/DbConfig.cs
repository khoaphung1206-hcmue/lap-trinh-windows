namespace COMP1019_Buoi11_Lab11
{
    /// <summary>
    /// Cấu hình kết nối SQL Server. Đây là nơi duy nhất cần sửa khi đổi máy.
    /// </summary>
    public static class DbConfig
    {
        // Chuỗi kết nối mặc định (SQL Server Express, instance SQLEXPRESS01, đăng nhập bằng Windows)
        public const string DefaultConnectionString =
            "Data Source=localhost\\SQLEXPRESS01;Initial Catalog=EduManagerDB;Integrated Security=True;TrustServerCertificate=True";

        /// <summary>
        /// Chuỗi kết nối đang dùng. Có thể ghi đè bằng biến môi trường EDUMANAGER_CONNSTR.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string fromEnvironment = Environment.GetEnvironmentVariable("EDUMANAGER_CONNSTR");
                return string.IsNullOrWhiteSpace(fromEnvironment) ? DefaultConnectionString : fromEnvironment;
            }
        }
    }
}
