using Microsoft.Data.SqlClient;

namespace QLSinhVienLab12.DAL
{
    /// <summary>
    /// Noi tap trung cau hinh ket noi.
    /// - Neu ServerName khac rong: dung dung server do.
    /// - Neu de rong: tu dong thu lan luot cac server thuong gap (.\SQLEXPRESS, LocalDB, ...)
    ///   va dung server dau tien ket noi duoc.
    /// </summary>
    public static class Database
    {
        // Muon co dinh server thi dien vao day, vi du: @".\SQLEXPRESS" hoac @"DESKTOP-ABC\SQLEXPRESS"
        public const string ServerName = @".\SQLEXPRESS01";

        public const string DatabaseName = "QLSinhVienLab12";

        private static readonly string[] CandidateServers =
        {
            @".\SQLEXPRESS",
            ".",
            @"(localdb)\MSSQLLocalDB",
            "localhost",
            @"localhost\SQLEXPRESS",
            @".\MSSQLSERVER01"
        };

        private static string? connectionString;

        public static string ConnectionString => connectionString ??= BuildConnectionString();

        private static string BuildConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(ServerName))
                return Build(ServerName, DatabaseName, 15);

            foreach (string server in CandidateServers)
            {
                try
                {
                    // Thu ket noi vao master truoc de xem server co ton tai khong
                    using SqlConnection test = new SqlConnection(Build(server, "master", 3));
                    test.Open();
                    return Build(server, DatabaseName, 15);
                }
                catch (SqlException)
                {
                    // thu server tiep theo
                }
            }

            throw new InvalidOperationException(
                "Không tìm thấy SQL Server nào đang chạy. Đã thử: " + string.Join(", ", CandidateServers) +
                ".\nHãy bật dịch vụ SQL Server trong Services, hoặc điền tên server vào Database.ServerName.");
        }

        private static string Build(string server, string database, int timeoutSeconds)
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                IntegratedSecurity = true,
                TrustServerCertificate = true,
                ConnectTimeout = timeoutSeconds
            }.ConnectionString;
        }
    }
}
