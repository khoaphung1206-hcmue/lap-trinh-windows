using System.Data;
using System.Text;
using QLSinhVienLab12.DAL;

namespace QLSinhVienLab12.Forms
{
    public partial class FrmNhapDiemTheoLop : Form
    {
        private readonly KetQuaDAL ketQuaDAL = new KetQuaDAL();
        private readonly BindingSource ketQuaSource = new BindingSource();

        public FrmNhapDiemTheoLop()
        {
            InitializeComponent();
        }

        // ===================== LOAD FORM =====================

        private void FrmNhapDiemTheoLop_Load(object? sender, EventArgs e)
        {
            try
            {
                LoadComboBox();
                ConfigureGrid();
                SetStatus("Chọn lớp, môn học rồi bấm \"Tải danh sách\".");
            }
            catch (Exception ex)
            {
                ShowError("Không thể kết nối cơ sở dữ liệu. Kiểm tra ConnectionString trong DAL/Database.cs.\n\n" + ex.Message);
            }
        }

        private void LoadComboBox()
        {
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            cboLop.DataSource = ketQuaDAL.GetLop();

            cboMonHoc.DisplayMember = "TenMon";
            cboMonHoc.ValueMember = "MaMon";
            cboMonHoc.DataSource = ketQuaDAL.GetMonHoc();
        }

        private void ConfigureGrid()
        {
            dgvKetQua.AutoGenerateColumns = false;
            dgvKetQua.Columns.Clear();
            dgvKetQua.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "MaSV", DataPropertyName = "MaSV", HeaderText = "Mã SV",
                    ReadOnly = true, Width = 90
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "HoTen", DataPropertyName = "HoTen", HeaderText = "Họ tên",
                    ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 40
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Diem", DataPropertyName = "Diem", HeaderText = "Điểm",
                    Width = 80, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "GhiChu", DataPropertyName = "GhiChu", HeaderText = "Ghi chú",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 60
                });
            dgvKetQua.DataSource = ketQuaSource;
        }

        // ===================== TẢI DANH SÁCH =====================

        private void btnTaiDanhSach_Click(object? sender, EventArgs e)
        {
            string maLop = Convert.ToString(cboLop.SelectedValue) ?? "";
            string maMon = Convert.ToString(cboMonHoc.SelectedValue) ?? "";
            if (maLop.Length == 0 || maMon.Length == 0)
            {
                ShowWarning("Vui lòng chọn lớp và môn học.");
                return;
            }

            try
            {
                txtTimKiem.Clear();
                ketQuaSource.Filter = null;
                ketQuaSource.DataSource = ketQuaDAL.GetKetQuaByLopMon(maLop, maMon);
                ClearErrorHighlight();
                SetStatus($"Đã tải {ketQuaSource.Count} sinh viên - lớp {cboLop.Text}, môn {cboMonHoc.Text}.");
                UpdateThongKe();
            }
            catch (Exception ex)
            {
                ShowError("Không thể tải danh sách: " + ex.Message);
            }
        }

        // ===================== LƯU TẤT CẢ =====================

        private void btnLuuTatCa_Click(object? sender, EventArgs e)
        {
            if (ketQuaSource.DataSource is not DataTable table || table.Rows.Count == 0)
            {
                ShowWarning("Chưa có dữ liệu để lưu. Hãy tải danh sách trước.");
                return;
            }

            // Đẩy giá trị ô đang gõ dở về DataTable
            dgvKetQua.EndEdit();
            ketQuaSource.EndEdit();

            List<string> errors = ValidateData(table, out HashSet<string> invalidMaSV);
            HighlightInvalidRows(invalidMaSV);
            if (errors.Count > 0)
            {
                SetStatus($"Có {errors.Count} lỗi - chưa lưu gì xuống cơ sở dữ liệu.");
                ShowWarning("Dữ liệu không hợp lệ, toàn bộ thao tác lưu đã bị hủy:\n\n" + FormatErrors(errors));
                return;
            }

            string maMon = Convert.ToString(cboMonHoc.SelectedValue) ?? "";
            try
            {
                int count = ketQuaDAL.SaveAll(maMon, table);   // Commit bên trong DAL
                SetStatus($"Đã lưu thành công {count} dòng.");
                MessageBox.Show($"Đã lưu thành công {count} dòng.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)                               // DAL đã Rollback toàn bộ
            {
                SetStatus("Lưu thất bại - đã rollback toàn bộ.");
                ShowError("Không thể lưu dữ liệu, toàn bộ thay đổi đã được rollback.\n\n" + ex.Message);
            }
        }

        // ===================== VALIDATION =====================

        private static List<string> ValidateData(DataTable table, out HashSet<string> invalidMaSV)
        {
            List<string> errors = new List<string>();
            invalidMaSV = new HashSet<string>();

            foreach (DataRow row in table.Rows)
            {
                string maSV = Convert.ToString(row["MaSV"]) ?? "";
                string? loi = null;

                if (row.IsNull("Diem"))
                {
                    loi = "điểm không được để trống.";
                }
                else
                {
                    double diem = Convert.ToDouble(row["Diem"]);
                    if (double.IsNaN(diem) || diem < 0 || diem > 10)
                        loi = $"điểm {diem} nằm ngoài khoảng 0 - 10.";
                }

                if (loi != null)
                {
                    invalidMaSV.Add(maSV);
                    errors.Add($"- {maSV} ({Convert.ToString(row["HoTen"])}): {loi}");
                }
            }
            return errors;
        }

        private static string FormatErrors(List<string> errors)
        {
            const int maxLines = 12;
            StringBuilder sb = new StringBuilder();
            foreach (string line in errors.Take(maxLines))
                sb.AppendLine(line);
            if (errors.Count > maxLines)
                sb.AppendLine($"... và {errors.Count - maxLines} lỗi khác.");
            return sb.ToString();
        }

        private void HighlightInvalidRows(HashSet<string> invalidMaSV)
        {
            foreach (DataGridViewRow gridRow in dgvKetQua.Rows)
            {
                string maSV = Convert.ToString(gridRow.Cells["MaSV"].Value) ?? "";
                gridRow.Cells["Diem"].Style.BackColor =
                    invalidMaSV.Contains(maSV) ? Color.LightCoral : dgvKetQua.DefaultCellStyle.BackColor;
            }
        }

        private void ClearErrorHighlight()
        {
            foreach (DataGridViewRow gridRow in dgvKetQua.Rows)
                gridRow.Cells["Diem"].Style.BackColor = dgvKetQua.DefaultCellStyle.BackColor;
        }

        // Người dùng gõ chữ (không phải số) vào ô Diem -> DataGridView báo DataError
        private void dgvKetQua_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
            SetStatus("Điểm phải là một số (ví dụ 8 hoặc 7.5). Giá trị vừa nhập bị bỏ qua.");
            MessageBox.Show("Điểm phải là một số trong khoảng 0 đến 10.", "Sai định dạng",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dgvKetQua.CancelEdit();
        }

        private void dgvKetQua_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            ketQuaSource.EndEdit();
            if (e.RowIndex >= 0 && dgvKetQua.Columns[e.ColumnIndex].Name == "Diem")
                dgvKetQua.Rows[e.RowIndex].Cells["Diem"].Style.BackColor = dgvKetQua.DefaultCellStyle.BackColor;
            UpdateThongKe();
        }

        // ===================== MỞ RỘNG: TÌM KIẾM / THỐNG KÊ =====================

        private void txtTimKiem_TextChanged(object? sender, EventArgs e)
        {
            if (ketQuaSource.DataSource is not DataTable) return;

            string keyword = EscapeFilterValue(txtTimKiem.Text.Trim());
            // Đây là biểu thức lọc của DataView trong RAM, không phải câu lệnh SQL gửi xuống DB
            ketQuaSource.Filter = keyword.Length == 0
                ? null
                : $"MaSV LIKE '%{keyword}%' OR HoTen LIKE '%{keyword}%'";
        }

        private static string EscapeFilterValue(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c == '\'') sb.Append("''");
                else if (c is '[' or ']' or '%' or '*') sb.Append('[').Append(c).Append(']');
                else sb.Append(c);
            }
            return sb.ToString();
        }

        private void UpdateThongKe()
        {
            if (ketQuaSource.DataSource is not DataTable table || table.Rows.Count == 0)
            {
                lblThongKe.Text = "";
                return;
            }

            int total = table.Rows.Count;
            int daNhap = table.Select("Diem IS NOT NULL").Length;
            object avg = table.Compute("AVG(Diem)", "");
            string tb = avg == DBNull.Value ? "-" : Convert.ToDouble(avg).ToString("0.00");
            lblThongKe.Text = $"Đã nhập {daNhap}/{total} | Điểm TB: {tb}";
        }

        // ===================== TIỆN ÍCH GIAO DIỆN =====================

        private void SetStatus(string message) => lblTrangThai.Text = message;

        private static void ShowWarning(string message) =>
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static void ShowError(string message) =>
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
