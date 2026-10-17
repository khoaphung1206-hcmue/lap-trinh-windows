using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace ProductManager
{
    public partial class FrmProduct : Form
    {
        private static readonly CultureInfo ViCulture = new CultureInfo("vi-VN");

        // Danh sách gốc chứa toàn bộ sản phẩm
        private readonly List<Product> _products = new List<Product>();
        // BindingSource gắn với DataGridView (hiển thị danh sách đã lọc)
        private readonly BindingSource _bindingSource = new BindingSource();

        private string _keyword = "";
        private string _duongDanAnh = "";
        private bool _suppressSelection;

        public FrmProduct()
        {
            InitializeComponent();
        }

        // =====================================================
        // LOAD / ĐÓNG FORM
        // =====================================================
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });

            SetupGrid();
            SeedData();
            RefreshGrid();
            ClearInput();
            UpdateStatus("Sẵn sàng.");
        }

        private void FrmProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void SeedData()
        {
            _products.Add(new Product("SP001", "Laptop Dell Inspiron 15", "Điện tử", 15500000, 10, new DateTime(2026, 8, 15), true, ""));
            _products.Add(new Product("SP002", "Bút bi Thiên Long", "Văn phòng", 5000, 200, new DateTime(2026, 9, 1), true, ""));
            _products.Add(new Product("SP003", "Nồi cơm điện Sharp", "Gia dụng", 1200000, 25, new DateTime(2026, 7, 20), true, ""));
            _products.Add(new Product("SP004", "Áo thun nam", "Thời trang", 180000, 60, new DateTime(2026, 6, 10), false, ""));
            _products.Add(new Product("SP005", "Tai nghe Bluetooth", "Điện tử", 650000, 40, new DateTime(2026, 9, 25), true, ""));
        }

        // =====================================================
        // DATAGRIDVIEW
        // =====================================================
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
                Name = "colTenSP", HeaderText = "Tên sản phẩm", DataPropertyName = "TenSP", FillWeight = 140
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLoaiSP", HeaderText = "Loại", DataPropertyName = "LoaiSP", FillWeight = 80
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
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNgayNhap", HeaderText = "Ngày nhập", DataPropertyName = "NgayNhap", FillWeight = 75,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            dgvProducts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colConKinhDoanh", HeaderText = "Còn kinh doanh", DataPropertyName = "ConKinhDoanh", FillWeight = 70
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDuongDanAnh", HeaderText = "Đường dẫn ảnh", DataPropertyName = "DuongDanAnh", FillWeight = 120
            });

            dgvProducts.DataSource = _bindingSource;
        }

        /// <summary>
        /// Nạp lại DataGridView từ danh sách gốc, áp dụng bộ lọc tìm kiếm hiện tại.
        /// </summary>
        private void RefreshGrid()
        {
            _suppressSelection = true;

            IEnumerable<Product> data = _products;
            if (!string.IsNullOrEmpty(_keyword))
            {
                data = _products.Where(p =>
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

        private void SelectProduct(Product product)
        {
            _suppressSelection = true;
            dgvProducts.ClearSelection();
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (ReferenceEquals(row.DataBoundItem, product))
                {
                    row.Selected = true;
                    dgvProducts.CurrentCell = row.Cells[0];
                    break;
                }
            }
            _suppressSelection = false;
        }

        private void LoadSelectedToForm()
        {
            Product product = GetSelectedProduct();
            if (product != null)
            {
                LoadProductToForm(product);
            }
        }

        // =====================================================
        // CÁC HÀM XỬ LÝ DỮ LIỆU NHẬP
        // =====================================================
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
            else if (_products.Any(p => !ReferenceEquals(p, editing) &&
                                        string.Equals(p.MaSP, ma, StringComparison.OrdinalIgnoreCase)))
            {
                AddError(txtMaSP, "Mã sản phẩm đã tồn tại.");
            }

            if (txtTenSP.Text.Trim().Length == 0)
            {
                AddError(txtTenSP, "Tên sản phẩm không được để trống.");
            }

            if (cboLoaiSP.SelectedIndex < 0)
            {
                AddError(cboLoaiSP, "Vui lòng chọn loại sản phẩm.");
            }

            if (numDonGia.Value <= 0)
            {
                AddError(numDonGia, "Đơn giá phải lớn hơn 0.");
            }

            if (numSoLuong.Value < 0)
            {
                AddError(numSoLuong, "Số lượng phải lớn hơn hoặc bằng 0.");
            }

            if (dtpNgayNhap.Value.Date > DateTime.Today)
            {
                AddError(dtpNgayNhap, "Ngày nhập không được lớn hơn ngày hiện tại.");
            }

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
            product.NgayNhap = dtpNgayNhap.Value.Date;
            product.ConKinhDoanh = chkConKinhDoanh.Checked;
            product.DuongDanAnh = _duongDanAnh;
        }

        private Product ReadProductFromInput()
        {
            var product = new Product();
            CopyInputTo(product);
            return product;
        }

        private void ClearInput()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
            dtpNgayNhap.Value = DateTime.Today;
            chkConKinhDoanh.Checked = true;
            _duongDanAnh = "";
            ShowImage("");
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
            dtpNgayNhap.Value = product.NgayNhap;
            chkConKinhDoanh.Checked = product.ConKinhDoanh;
            _duongDanAnh = product.DuongDanAnh ?? "";
            ShowImage(_duongDanAnh);
        }

        private void ShowImage(string path)
        {
            if (picAnh.Image != null)
            {
                picAnh.Image.Dispose();
                picAnh.Image = null;
            }

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;

            try
            {
                using (Image temp = Image.FromFile(path))
                {
                    // Sao chép sang Bitmap để không khóa file ảnh
                    picAnh.Image = new Bitmap(temp);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể mở file ảnh đã chọn.", "Lỗi ảnh",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _duongDanAnh = "";
            }
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;

            if (string.IsNullOrEmpty(_keyword))
                lblCount.Text = "Số sản phẩm: " + _products.Count;
            else
                lblCount.Text = $"Số sản phẩm: {_products.Count} (đang hiển thị {_bindingSource.Count})";

            decimal total = _products.Sum(p => p.DonGia * p.SoLuong);
            lblTongGiaTri.Text = "Tổng giá trị tồn kho: " + total.ToString("N0", ViCulture) + " VNĐ";
        }

        private bool EnsureSelected(out Product product, string actionName)
        {
            product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show($"Vui lòng chọn một sản phẩm trong danh sách trước khi {actionName}.",
                    "Chưa chọn sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateStatus($"Chưa chọn sản phẩm cần {actionName}.");
                return false;
            }
            return true;
        }

        // =====================================================
        // CÁC NÚT CHỨC NĂNG (Menu / ToolStrip dùng chung handler)
        // =====================================================
        private void Them_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;

            Product product = ReadProductFromInput();
            _products.Add(product);

            _keyword = "";
            txtTimKiem.Clear();
            RefreshGrid();
            ClearInput();
            UpdateStatus($"Đã thêm sản phẩm {product.MaSP}.");
        }

        private void Sua_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Product product, "sửa")) return;
            if (!ValidateInput(product)) return;

            CopyInputTo(product);
            RefreshGrid();
            SelectProduct(product);
            UpdateStatus($"Đã cập nhật sản phẩm {product.MaSP}.");
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Product product, "xóa")) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm \"{product.TenSP}\" ({product.MaSP})?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            _products.Remove(product);
            RefreshGrid();
            ClearInput();
            UpdateStatus($"Đã xóa sản phẩm {product.MaSP}.");
        }

        private void LamMoi_Click(object sender, EventArgs e)
        {
            _keyword = "";
            txtTimKiem.Clear();
            RefreshGrid();
            ClearInput();
            UpdateStatus("Đã làm mới, hiển thị toàn bộ danh sách.");
        }

        private void TimKiem_Click(object sender, EventArgs e)
        {
            _keyword = txtTimKiem.Text.Trim();
            RefreshGrid();

            if (_keyword.Length == 0)
                UpdateStatus("Hiển thị toàn bộ danh sách.");
            else
                UpdateStatus($"Tìm thấy {_bindingSource.Count} sản phẩm khớp với \"{_keyword}\".");
        }

        private void mnuTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.Focus();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TimKiem_Click(sender, EventArgs.Empty);
            }
        }

        // =====================================================
        // CHỌN ẢNH
        // =====================================================
        private void ChonAnh_Click(object sender, EventArgs e)
        {
            dlgOpenImage.Title = "Chọn ảnh sản phẩm";
            dlgOpenImage.Filter = "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tất cả các tệp|*.*";

            if (dlgOpenImage.ShowDialog() == DialogResult.OK)
            {
                _duongDanAnh = dlgOpenImage.FileName;
                ShowImage(_duongDanAnh);
                UpdateStatus("Đã chọn ảnh: " + Path.GetFileName(_duongDanAnh));
            }
        }

        // =====================================================
        // DATAGRIDVIEW: SỰ KIỆN CHUỘT / BÀN PHÍM / CONTEXT MENU
        // =====================================================
        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelection || !dgvProducts.Focused) return;
            LoadSelectedToForm();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) LoadSelectedToForm();
        }

        private void dgvProducts_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Nhấn chuột phải: chọn dòng dưới con trỏ rồi mới hiện ContextMenuStrip
            if (e.Button != MouseButtons.Right || e.RowIndex < 0) return;

            dgvProducts.ClearSelection();
            dgvProducts.Rows[e.RowIndex].Selected = true;
            dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            LoadSelectedToForm();
        }

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) XemChiTiet_Click(sender, EventArgs.Empty);
        }

        private void dgvProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                Xoa_Click(sender, EventArgs.Empty);
            }
        }

        private void mnuCtxSua_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Product product, "sửa")) return;

            LoadProductToForm(product);
            txtTenSP.Focus();
            UpdateStatus("Chỉnh sửa thông tin ở khung bên trên rồi nhấn nút Sửa (Ctrl+E) để lưu.");
        }

        private void XemChiTiet_Click(object sender, EventArgs e)
        {
            if (!EnsureSelected(out Product product, "xem chi tiết")) return;

            string detail =
                $"Mã SP: {product.MaSP}\n" +
                $"Tên SP: {product.TenSP}\n" +
                $"Loại: {product.LoaiSP}\n" +
                $"Đơn giá: {product.DonGia.ToString("N0", ViCulture)} VNĐ\n" +
                $"Số lượng: {product.SoLuong}\n" +
                $"Ngày nhập: {product.NgayNhap:dd/MM/yyyy}\n" +
                $"Tình trạng: {(product.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh")}\n" +
                $"Ảnh: {(string.IsNullOrEmpty(product.DuongDanAnh) ? "(chưa có)" : product.DuongDanAnh)}";

            MessageBox.Show(detail, "Chi tiết sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =====================================================
        // MENU: FILE / TRỢ GIÚP
        // =====================================================
        private void mnuXuat_Click(object sender, EventArgs e)
        {
            dlgSaveFile.Title = "Xuất danh sách sản phẩm";
            dlgSaveFile.Filter = "Tệp văn bản (*.txt)|*.txt";
            dlgSaveFile.FileName = "DanhSachSanPham.txt";
            if (dlgSaveFile.ShowDialog() != DialogResult.OK) return;

            var lines = new List<string>
            {
                "MaSP\tTenSP\tLoaiSP\tDonGia\tSoLuong\tNgayNhap\tTinhTrang"
            };
            foreach (Product p in _products)
            {
                lines.Add(string.Join("\t", new object[]
                {
                    p.MaSP, p.TenSP, p.LoaiSP, p.DonGia.ToString("N0", ViCulture), p.SoLuong,
                    p.NgayNhap.ToString("dd/MM/yyyy"), p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh"
                }));
            }

            try
            {
                File.WriteAllLines(dlgSaveFile.FileName, lines, Encoding.UTF8);
                UpdateStatus("Đã xuất danh sách ra: " + dlgSaveFile.FileName);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Không thể ghi file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuGioiThieu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ProductManager - Quản lý sản phẩm\nHọc phần: COMP1019 - Lập trình trên Windows\nWinForms, dữ liệu lưu trong bộ nhớ.",
                "Giới thiệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
