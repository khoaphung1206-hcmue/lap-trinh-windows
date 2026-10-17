using System.ComponentModel;
using System.Text.RegularExpressions;
using ShopManagerMini.Data;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public partial class FrmCustomer : Form
    {
        private readonly BindingSource _bindingSource = new BindingSource();
        private string _keyword = "";
        private bool _suppressSelection;

        private List<Customer> Customers
        {
            get { return DataStore.Customers; }
        }

        public FrmCustomer()
        {
            InitializeComponent();
        }

        // ================== LOAD ==================
        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            cboLoaiKH.Items.AddRange(new object[] { "Thường", "Thân thiết", "VIP" });
            SetupGrid();
            RefreshGrid();
            ClearInput();
        }

        private void SetupGrid()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Clear();
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaKH", HeaderText = "Mã KH", DataPropertyName = "MaKH", FillWeight = 60
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHoTen", HeaderText = "Họ tên", DataPropertyName = "HoTen", FillWeight = 150
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoDienThoai", HeaderText = "Số điện thoại", DataPropertyName = "SoDienThoai", FillWeight = 90
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLoaiKH", HeaderText = "Loại khách hàng", DataPropertyName = "LoaiKH", FillWeight = 80
            });
            dgvCustomers.DataSource = _bindingSource;
        }

        private void RefreshGrid()
        {
            _suppressSelection = true;

            IEnumerable<Customer> data = Customers;
            if (!string.IsNullOrEmpty(_keyword))
            {
                data = Customers.Where(c =>
                    c.MaKH.Contains(_keyword, StringComparison.OrdinalIgnoreCase) ||
                    c.HoTen.Contains(_keyword, StringComparison.OrdinalIgnoreCase) ||
                    c.SoDienThoai.Contains(_keyword, StringComparison.OrdinalIgnoreCase));
            }

            _bindingSource.DataSource = new BindingList<Customer>(data.ToList());
            dgvCustomers.ClearSelection();
            dgvCustomers.CurrentCell = null;

            _suppressSelection = false;
        }

        private Customer GetSelectedCustomer()
        {
            if (dgvCustomers.SelectedRows.Count == 0) return null;
            return dgvCustomers.SelectedRows[0].DataBoundItem as Customer;
        }

        private void LoadSelectedToForm()
        {
            Customer customer = GetSelectedCustomer();
            if (customer != null) LoadCustomerToForm(customer);
        }

        // ================== NHẬP LIỆU ==================
        private bool ValidateInput(Customer editing)
        {
            errorProvider.Clear();
            var errors = new List<string>();
            Control firstInvalid = null;

            void AddError(Control control, string message)
            {
                errorProvider.SetError(control, message);
                errors.Add("- " + message);
                if (firstInvalid == null) firstInvalid = control;
            }

            string ma = txtMaKH.Text.Trim();
            if (ma.Length == 0)
            {
                AddError(txtMaKH, "Mã khách hàng không được để trống.");
            }
            else if (Customers.Any(c => !ReferenceEquals(c, editing) &&
                                        string.Equals(c.MaKH, ma, StringComparison.OrdinalIgnoreCase)))
            {
                AddError(txtMaKH, "Mã khách hàng đã tồn tại, không được trùng.");
            }

            if (txtHoTen.Text.Trim().Length == 0)
                AddError(txtHoTen, "Họ tên không được để trống.");

            string sdt = txtSoDienThoai.Text.Trim();
            if (sdt.Length == 0)
                AddError(txtSoDienThoai, "Số điện thoại không được để trống.");
            else if (!Regex.IsMatch(sdt, @"^\d{9,11}$"))
                AddError(txtSoDienThoai, "Số điện thoại chỉ gồm 9-11 chữ số.");

            if (cboLoaiKH.SelectedIndex < 0)
                AddError(cboLoaiKH, "Vui lòng chọn loại khách hàng.");

            if (errors.Count > 0)
            {
                MessageBox.Show("Dữ liệu chưa hợp lệ:\n" + string.Join("\n", errors),
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (firstInvalid != null) firstInvalid.Focus();
                return false;
            }
            return true;
        }

        private void CopyInputTo(Customer customer)
        {
            customer.MaKH = txtMaKH.Text.Trim();
            customer.HoTen = txtHoTen.Text.Trim();
            customer.SoDienThoai = txtSoDienThoai.Text.Trim();
            customer.LoaiKH = cboLoaiKH.SelectedItem.ToString();
        }

        private void ClearInput()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            cboLoaiKH.SelectedIndex = -1;
            errorProvider.Clear();
            txtMaKH.Focus();
        }

        private void LoadCustomerToForm(Customer customer)
        {
            errorProvider.Clear();
            txtMaKH.Text = customer.MaKH;
            txtHoTen.Text = customer.HoTen;
            txtSoDienThoai.Text = customer.SoDienThoai;
            cboLoaiKH.SelectedItem = customer.LoaiKH;
        }

        private bool EnsureSelected(out Customer customer, string actionName)
        {
            customer = GetSelectedCustomer();
            if (customer == null)
            {
                MessageBox.Show($"Vui lòng chọn một khách hàng trong danh sách trước khi {actionName}.",
                    "Chưa chọn khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        // ================== CÁC NÚT ==================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;

            var customer = new Customer();
            CopyInputTo(customer);
            Customers.Add(customer);

            _keyword = "";
            txtTimKiem.Clear();
            RefreshGrid();
            ClearInput();
            MessageBox.Show("Đã thêm khách hàng thành công.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Customer customer, "sửa")) return;
            if (!ValidateInput(customer)) return;

            CopyInputTo(customer);
            RefreshGrid();
            ClearInput();
            MessageBox.Show("Đã cập nhật khách hàng.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Customer customer, "xóa")) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng \"{customer.HoTen}\" ({customer.MaKH})?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            Customers.Remove(customer);
            RefreshGrid();
            ClearInput();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            _keyword = "";
            txtTimKiem.Clear();
            RefreshGrid();
            ClearInput();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            _keyword = txtTimKiem.Text.Trim();
            RefreshGrid();

            if (_keyword.Length > 0 && _bindingSource.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào phù hợp.", "Tìm kiếm",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // ================== DATAGRIDVIEW ==================
        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelection || !dgvCustomers.Focused) return;
            LoadSelectedToForm();
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) LoadSelectedToForm();
        }
    }
}
