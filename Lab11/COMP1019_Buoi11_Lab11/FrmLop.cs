using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace COMP1019_Buoi11_Lab11
{
    public partial class FrmLop : Form
    {
        private bool _hasMaKhoa;            // bảng LOP có cột MaKhoa (database Lab 10) hay không
        private bool _suppressSelection;    // tạm tắt sự kiện chọn dòng khi nạp lại lưới
        private string _selectedMaLop;      // mã lớp đang được chọn để sửa/xóa
        private string _keyword = "";       // từ khóa tìm kiếm hiện tại

        public FrmLop()
        {
            InitializeComponent();
        }

        // =====================================================
        // KẾT NỐI VÀ TRUY CẬP DỮ LIỆU (ADO.NET)
        // =====================================================
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(DbConfig.ConnectionString);
        }

        /// <summary>
        /// Kiểm tra bảng LOP có cột MaKhoa không (database Lab 10 có, script tối thiểu của đề thì không).
        /// </summary>
        private bool DetectSchema()
        {
            const string sql =
                "SELECT COUNT(*) FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.LOP') AND name = N'MaKhoa'";

            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private DataTable GetKhoaTable()
        {
            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand("SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY TenKhoa", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        /// <summary>
        /// Lấy danh sách lớp kèm số sinh viên. Có từ khóa thì lọc theo mã hoặc tên lớp bằng LIKE + parameter.
        /// </summary>
        private DataTable GetLopTable(string keyword)
        {
            bool hasKeyword = !string.IsNullOrWhiteSpace(keyword);

            // Phần ghép chuỗi bên dưới chỉ là cấu trúc câu lệnh cố định, dữ liệu người dùng luôn đi qua parameter
            var sql = new StringBuilder();
            sql.Append("SELECT l.MaLop, l.TenLop, ");
            if (_hasMaKhoa) sql.Append("l.MaKhoa, ");
            sql.Append("COUNT(sv.MaSV) AS SoSinhVien ");
            sql.Append("FROM LOP AS l LEFT JOIN SINHVIEN AS sv ON sv.MaLop = l.MaLop ");
            if (hasKeyword) sql.Append("WHERE l.MaLop LIKE @kw OR l.TenLop LIKE @kw ");
            sql.Append("GROUP BY l.MaLop, l.TenLop");
            if (_hasMaKhoa) sql.Append(", l.MaKhoa");
            sql.Append(" ORDER BY l.MaLop");

            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand(sql.ToString(), connection))
            {
                if (hasKeyword)
                {
                    command.Parameters.Add("@kw", SqlDbType.NVarChar, 200).Value =
                        "%" + EscapeLike(keyword.Trim()) + "%";
                }

                using (var adapter = new SqlDataAdapter(command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // Ký tự đặc biệt của LIKE (% _ [) được xem là chữ thường khi tìm kiếm
        private static string EscapeLike(string text)
        {
            return text.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
        }

        private bool IsMaLopExists(string maLop)
        {
            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand("SELECT COUNT(1) FROM LOP WHERE MaLop = @ma", connection))
            {
                command.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private int CountSinhVienByLop(string maLop)
        {
            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand("SELECT COUNT(1) FROM SINHVIEN WHERE MaLop = @ma", connection))
            {
                command.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private void InsertLop(string maLop, string tenLop, string maKhoa)
        {
            string sql = _hasMaKhoa
                ? "INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES (@ma, @ten, @khoa)"
                : "INSERT INTO LOP (MaLop, TenLop) VALUES (@ma, @ten)";

            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;
                command.Parameters.Add("@ten", SqlDbType.NVarChar, 100).Value = tenLop;
                if (_hasMaKhoa)
                {
                    command.Parameters.Add("@khoa", SqlDbType.VarChar, 10).Value = maKhoa;
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private int UpdateLop(string maLop, string tenLop, string maKhoa)
        {
            string sql = _hasMaKhoa
                ? "UPDATE LOP SET TenLop = @ten, MaKhoa = @khoa WHERE MaLop = @ma"
                : "UPDATE LOP SET TenLop = @ten WHERE MaLop = @ma";

            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;
                command.Parameters.Add("@ten", SqlDbType.NVarChar, 100).Value = tenLop;
                if (_hasMaKhoa)
                {
                    command.Parameters.Add("@khoa", SqlDbType.VarChar, 10).Value = maKhoa;
                }
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        private int DeleteLop(string maLop)
        {
            using (SqlConnection connection = CreateConnection())
            using (var command = new SqlCommand("DELETE FROM LOP WHERE MaLop = @ma", connection))
            {
                command.Parameters.Add("@ma", SqlDbType.VarChar, 20).Value = maLop;
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        // =====================================================
        // FORM LOAD (F1)
        // =====================================================
        private void FrmLop_Load(object sender, EventArgs e)
        {
            try
            {
                _hasMaKhoa = DetectSchema();
                ApplySchemaToUi();
                SetupGrid();
                if (_hasMaKhoa) LoadKhoa();
                LoadData();
                UpdateStatus("Đã tải danh sách lớp từ SQL Server.");
            }
            catch (SqlException ex)
            {
                if (dgvLop.Columns.Count == 0)
                {
                    ApplySchemaToUi();
                    SetupGrid();
                }
                ShowDbError(ex, "kết nối và tải dữ liệu");
                UpdateStatus("Không thể tải dữ liệu từ SQL Server.");
            }
        }

        // Ẩn ô chọn khoa nếu bảng LOP không có cột MaKhoa
        private void ApplySchemaToUi()
        {
            lblKhoa.Visible = _hasMaKhoa;
            cboKhoa.Visible = _hasMaKhoa;
        }

        private void LoadKhoa()
        {
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
            cboKhoa.DataSource = GetKhoaTable();
            cboKhoa.SelectedIndex = -1;
        }

        private void SetupGrid()
        {
            dgvLop.AutoGenerateColumns = false;
            dgvLop.Columns.Clear();
            dgvLop.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaLop", HeaderText = "Mã lớp", DataPropertyName = "MaLop", FillWeight = 70
            });
            dgvLop.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenLop", HeaderText = "Tên lớp", DataPropertyName = "TenLop", FillWeight = 160
            });
            if (_hasMaKhoa)
            {
                dgvLop.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colMaKhoa", HeaderText = "Mã khoa", DataPropertyName = "MaKhoa", FillWeight = 70
                });
            }
            dgvLop.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoSinhVien", HeaderText = "Số sinh viên", DataPropertyName = "SoSinhVien", FillWeight = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        /// <summary>
        /// Nạp lại lưới từ SQL Server theo từ khóa tìm kiếm hiện tại.
        /// </summary>
        private void LoadData()
        {
            DataTable table = GetLopTable(_keyword);

            _suppressSelection = true;
            dgvLop.DataSource = table;
            dgvLop.ClearSelection();
            dgvLop.CurrentCell = null;
            _suppressSelection = false;

            lblCount.Text = "Số lớp: " + table.Rows.Count;
        }

        // =====================================================
        // CHỌN DÒNG (F2)
        // =====================================================
        private void dgvLop_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelection || !dgvLop.Focused) return;
            LoadSelectedRowToInputs();
        }

        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) LoadSelectedRowToInputs();
        }

        private void LoadSelectedRowToInputs()
        {
            if (dgvLop.SelectedRows.Count == 0) return;

            var rowView = dgvLop.SelectedRows[0].DataBoundItem as DataRowView;
            if (rowView == null) return;

            _selectedMaLop = Convert.ToString(rowView["MaLop"]);
            txtMaLop.Text = _selectedMaLop;
            txtTenLop.Text = Convert.ToString(rowView["TenLop"]);
            txtMaLop.ReadOnly = true;   // mã lớp là khóa chính nên khóa lại khi đang sửa

            if (_hasMaKhoa)
            {
                cboKhoa.SelectedValue = rowView["MaKhoa"];
            }
        }

        // =====================================================
        // TIỆN ÍCH GIAO DIỆN
        // =====================================================
        private void ClearInputs()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtMaLop.ReadOnly = false;
            _selectedMaLop = null;
            if (_hasMaKhoa) cboKhoa.SelectedIndex = -1;
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
            lblCount.Text = "Số lớp: " + dgvLop.Rows.Count;
        }

        private void ShowWarning(string message, Control focusControl)
        {
            MessageBox.Show(message, "Thiếu hoặc sai dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (focusControl != null) focusControl.Focus();
        }

        /// <summary>
        /// Kiểm tra dữ liệu nhập. checkMaLop = true khi thêm mới (khi sửa dùng mã đang chọn).
        /// </summary>
        private bool ValidateInput(bool checkMaLop, out string maLop, out string tenLop, out string maKhoa)
        {
            maLop = checkMaLop ? txtMaLop.Text.Trim() : _selectedMaLop;
            tenLop = txtTenLop.Text.Trim();
            maKhoa = null;

            if (checkMaLop)
            {
                if (maLop.Length == 0)
                {
                    ShowWarning("Mã lớp không được để trống.", txtMaLop);
                    return false;
                }
                if (maLop.Length > 20 || !Regex.IsMatch(maLop, @"^[A-Za-z0-9_\-]+$"))
                {
                    ShowWarning("Mã lớp chỉ gồm chữ không dấu, số, '_' hoặc '-' (tối đa 20 ký tự).", txtMaLop);
                    return false;
                }
            }

            if (tenLop.Length == 0)
            {
                ShowWarning("Tên lớp không được để trống.", txtTenLop);
                return false;
            }
            if (tenLop.Length > 100)
            {
                ShowWarning("Tên lớp tối đa 100 ký tự.", txtTenLop);
                return false;
            }

            if (_hasMaKhoa)
            {
                if (cboKhoa.SelectedValue == null)
                {
                    ShowWarning("Vui lòng chọn khoa của lớp.", cboKhoa);
                    return false;
                }
                maKhoa = Convert.ToString(cboKhoa.SelectedValue);
            }
            return true;
        }

        /// <summary>
        /// Hiển thị lỗi SQL Server bằng thông báo dễ hiểu.
        /// </summary>
        private void ShowDbError(SqlException ex, string action)
        {
            string message;
            switch (ex.Number)
            {
                case 2627:
                case 2601:
                    message = "Mã lớp đã tồn tại trong cơ sở dữ liệu.";
                    break;
                case 547:
                    message = "Thao tác vi phạm ràng buộc khóa ngoại: lớp đang có sinh viên hoặc khoa không tồn tại.";
                    break;
                case 208:
                    message = "Không tìm thấy bảng trong database. Hãy chạy script tạo database (Lab 10 hoặc EduManagerDB.sql).";
                    break;
                case 4060:
                    message = "Không mở được database EduManagerDB. Hãy kiểm tra database đã được tạo chưa.";
                    break;
                case 18456:
                    message = "Đăng nhập SQL Server thất bại. Hãy kiểm tra tài khoản trong chuỗi kết nối.";
                    break;
                case -1:
                case 2:
                case 53:
                case 258:
                    message = "Không kết nối được tới SQL Server. Hãy kiểm tra dịch vụ SQL Server đã chạy chưa "
                            + "và tên server trong DbConfig.cs.\nChuỗi kết nối: " + DbConfig.ConnectionString;
                    break;
                default:
                    message = ex.Message;
                    break;
            }

            MessageBox.Show("Lỗi khi " + action + ":\n" + message, "Lỗi cơ sở dữ liệu",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // =====================================================
        // CÁC NÚT CHỨC NĂNG (F3 - F7)
        // =====================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(true, out string maLop, out string tenLop, out string maKhoa)) return;

            try
            {
                if (IsMaLopExists(maLop))
                {
                    ShowWarning("Mã lớp \"" + maLop + "\" đã tồn tại.", txtMaLop);
                    return;
                }

                InsertLop(maLop, tenLop, maKhoa);

                _keyword = "";
                txtTimKiem.Clear();
                ClearInputs();
                LoadData();
                UpdateStatus("Đã thêm lớp " + maLop + ".");
            }
            catch (SqlException ex)
            {
                ShowDbError(ex, "thêm lớp");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaLop))
            {
                MessageBox.Show("Vui lòng chọn một lớp trong danh sách trước khi sửa.",
                    "Chưa chọn lớp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateInput(false, out string maLop, out string tenLop, out string maKhoa)) return;

            try
            {
                int affected = UpdateLop(maLop, tenLop, maKhoa);
                if (affected == 0)
                {
                    MessageBox.Show("Không tìm thấy lớp cần sửa (có thể đã bị xóa).", "Không có thay đổi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    UpdateStatus("Đã cập nhật lớp " + maLop + ".");
                }

                ClearInputs();
                LoadData();
                if (affected > 0) lblStatus.Text = "Đã cập nhật lớp " + maLop + ".";
            }
            catch (SqlException ex)
            {
                ShowDbError(ex, "sửa lớp");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaLop))
            {
                MessageBox.Show("Vui lòng chọn một lớp trong danh sách trước khi xóa.",
                    "Chưa chọn lớp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maLop = _selectedMaLop;

            try
            {
                int studentCount = CountSinhVienByLop(maLop);
                if (studentCount > 0)
                {
                    MessageBox.Show("Lớp " + maLop + " đang có " + studentCount + " sinh viên nên không thể xóa.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp " + maLop + "?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                DeleteLop(maLop);
                ClearInputs();
                LoadData();
                UpdateStatus("Đã xóa lớp " + maLop + ".");
            }
            catch (SqlException ex)
            {
                ShowDbError(ex, "xóa lớp");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                _keyword = "";
                txtTimKiem.Clear();
                ClearInputs();
                LoadData();
                txtMaLop.Focus();
                UpdateStatus("Đã làm mới, hiển thị toàn bộ danh sách lớp.");
            }
            catch (SqlException ex)
            {
                ShowDbError(ex, "làm mới dữ liệu");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                _keyword = txtTimKiem.Text.Trim();
                ClearInputs();
                LoadData();

                if (_keyword.Length == 0)
                    UpdateStatus("Hiển thị toàn bộ danh sách lớp.");
                else
                    UpdateStatus("Tìm thấy " + dgvLop.Rows.Count + " lớp khớp với \"" + _keyword + "\".");
            }
            catch (SqlException ex)
            {
                ShowDbError(ex, "tìm kiếm");
            }
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem_Click(sender, EventArgs.Empty);
            }
        }
    }
}
