using System.ComponentModel;
using System.Globalization;
using ShopManagerMini.Data;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    public partial class FrmProduct : Form
    {
        private static readonly CultureInfo ViCulture = new CultureInfo("vi-VN");

        private readonly BindingSource _bindingSource = new BindingSource();
        private string _keyword = "";
        private bool _suppressSelection;

        // Danh sách gốc dùng chung (nằm trong DataStore)
        private List<Product> Products
        {
            get { return DataStore.Products; }
        }

        public FrmProduct()
        {
            InitializeComponent();
        }

        // ================== LOAD ==================
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });
            SetupGrid();
            RefreshGrid();
            ClearInput();
        }

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaSP", HeaderText = "Mã SP", DataPropertyName = "MaSP", FillWeight = 60
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenSP", HeaderText = "Tên sản phẩm", DataPropertyName = "TenSP", FillWeight = 150
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLoaiSP", HeaderText = "Loại sản phẩm", DataPropertyName = "LoaiSP", FillWeight = 80
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDonGia", HeaderText = "Đơn giá (VNĐ)", DataPropertyName = "DonGia", FillWeight = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0", FormatProvider = ViCulture, Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuong", HeaderText = "Số lượng", DataPropertyName = "SoLuong", FillWeight = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvProducts.DataSource = _bindingSource;
        }

        /// <summary>
        /// Nạp lại lưới từ danh sách gốc, có áp dụng bộ lọc tìm kiếm.
        /// </summary>
        private void RefreshGrid()
        {
            _suppressSelection = true;

            IEnumerable<Product> data = Products;
            if (!string.IsNullOrEmpty(_keyword))
            {
                data = Products.Where(p =>
                    p.MaSP.Contains(_keyword, StringComparison.OrdinalIgnoreCase) ||
                    p.TenSP.Contains(_keyword, StringComparison.OrdinalIgnoreCase));
            }

            _bindingSource.DataSource = new BindingList<Product>(data.ToList());
            dgvProducts.ClearSelection();
            dgvProducts.CurrentCell = null;

            _suppressSelection = false;
        }

        private Product GetSelectedProduct()
        {
            if (dgvProducts.SelectedRows.Count == 0) return null;
            return dgvProducts.SelectedRows[0].DataBoundItem as Product;
        }

        private void LoadSelectedToForm()
        {
            Product product = GetSelectedProduct();
            if (product != null) LoadProductToForm(product);
        }

        // ================== NHẬP LIỆU ==================
        private bool ValidateInput(Product editing)
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

            string ma = txtMaSP.Text.Trim();
            if (ma.Length == 0)
            {
                AddError(txtMaSP, "Mã sản phẩm không được để trống.");
            }
            else if (Products.Any(p => !ReferenceEquals(p, editing) &&
                                       string.Equals(p.MaSP, ma, StringComparison.OrdinalIgnoreCase)))
            {
                AddError(txtMaSP, "Mã sản phẩm đã tồn tại, không được trùng.");
            }

            if (txtTenSP.Text.Trim().Length == 0)
                AddError(txtTenSP, "Tên sản phẩm không được để trống.");

            if (cboLoaiSP.SelectedIndex < 0)
                AddError(cboLoaiSP, "Vui lòng chọn loại sản phẩm.");

            if (numDonGia.Value < 0)
                AddError(numDonGia, "Đơn giá không được âm.");

            if (numSoLuong.Value < 0)
                AddError(numSoLuong, "Số lượng không được âm.");

            if (errors.Count > 0)
            {
                MessageBox.Show("Dữ liệu chưa hợp lệ:\n" + string.Join("\n", errors),
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (firstInvalid != null) firstInvalid.Focus();
                return false;
            }
            return true;
        }

        private void CopyInputTo(Product product)
        {
            product.MaSP = txtMaSP.Text.Trim();
            product.TenSP = txtTenSP.Text.Trim();
            product.LoaiSP = cboLoaiSP.SelectedItem.ToString();
            product.DonGia = numDonGia.Value;
            product.SoLuong = (int)numSoLuong.Value;
        }

        private void ClearInput()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
            errorProvider.Clear();
            txtMaSP.Focus();
        }

        private void LoadProductToForm(Product product)
        {
            errorProvider.Clear();
            txtMaSP.Text = product.MaSP;
            txtTenSP.Text = product.TenSP;
            cboLoaiSP.SelectedItem = product.LoaiSP;
            numDonGia.Value = Math.Min(product.DonGia, numDonGia.Maximum);
            numSoLuong.Value = Math.Min(product.SoLuong, (int)numSoLuong.Maximum);
        }

        private bool EnsureSelected(out Product product, string actionName)
        {
            product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show($"Vui lòng chọn một sản phẩm trong danh sách trước khi {actionName}.",
                    "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        // ================== CÁC NÚT ==================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;

            var product = new Product();
            CopyInputTo(product);
            Products.Add(product);

            _keyword = "";
            txtTimKiem.Clear();
            RefreshGrid();
            ClearInput();
            MessageBox.Show("Đã thêm sản phẩm thành công.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Product product, "sửa")) return;
            if (!ValidateInput(product)) return;

            CopyInputTo(product);
            RefreshGrid();
            ClearInput();
            MessageBox.Show("Đã cập nhật sản phẩm.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Product product, "xóa")) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm \"{product.TenSP}\" ({product.MaSP})?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            Products.Remove(product);
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
                MessageBox.Show("Không tìm thấy sản phẩm nào phù hợp.", "Tìm kiếm",
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
        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelection || !dgvProducts.Focused) return;
            LoadSelectedToForm();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) LoadSelectedToForm();
        }
    }
}
