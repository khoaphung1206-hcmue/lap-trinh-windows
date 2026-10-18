using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SimplePaint
{
    public partial class FrmPaint : Form
    {
        // Danh sách dữ liệu gốc của các nét đã vẽ
        private readonly List<DrawItem> _items = new List<DrawItem>();

        private DrawItem _current;          // nét đang kéo chuột (preview)
        private bool _isDrawing;
        private Color _penColor = Color.Black;
        private Image _backgroundImage;     // ảnh nền (nếu có)

        public FrmPaint()
        {
            InitializeComponent();
        }

        // =====================================================
        // LOAD / ĐÓNG FORM
        // =====================================================
        private void FrmPaint_Load(object sender, EventArgs e)
        {
            cboMode.Items.AddRange(new object[] { "Pen", "Line", "Rectangle", "Ellipse" });
            cboMode.SelectedIndex = 0;
            pnlColorPreview.BackColor = _penColor;
            UpdateCoordinates(null);
            UpdateCount();
        }

        private void FrmPaint_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_backgroundImage != null)
            {
                _backgroundImage.Dispose();
                _backgroundImage = null;
            }
        }

        // Phím tắt Ctrl+Z để Undo (xử lý ở mức Form, dùng được cả khi đang focus vào ô khác)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                Undo();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =====================================================
        // TIỆN ÍCH
        // =====================================================
        private ShapeType GetCurrentShape()
        {
            return Enum.TryParse(cboMode.Text, out ShapeType shape) ? shape : ShapeType.Pen;
        }

        private void UpdateCoordinates(Point? point)
        {
            lblStatus.Text = point.HasValue
                ? $"X: {point.Value.X}, Y: {point.Value.Y}"
                : "X: -, Y: -";
        }

        private void UpdateCount()
        {
            lblCount.Text = "Số nét vẽ: " + _items.Count;
        }

        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Chỉ Rectangle và Ellipse mới có chế độ tô màu
            ShapeType shape = GetCurrentShape();
            chkFill.Enabled = shape == ShapeType.Rectangle || shape == ShapeType.Ellipse;
        }

        // =====================================================
        // VẼ LẠI TOÀN BỘ TRONG SỰ KIỆN PAINT
        // =====================================================
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            RenderScene(e.Graphics, canvas.ClientSize);
        }

        /// <summary>
        /// Vẽ ảnh nền, danh sách nét đã vẽ và nét đang kéo.
        /// Dùng chung cho Paint và khi lưu PNG.
        /// </summary>
        private void RenderScene(Graphics g, Size size)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_backgroundImage != null)
            {
                g.DrawImage(_backgroundImage, GetImageRectangle(size));
            }

            foreach (DrawItem item in _items)
            {
                item.Draw(g);
            }

            if (_current != null)
            {
                _current.Draw(g);
            }
        }

        // Căn ảnh nền vào giữa canvas, giữ đúng tỉ lệ
        private Rectangle GetImageRectangle(Size size)
        {
            float scale = Math.Min((float)size.Width / _backgroundImage.Width,
                                   (float)size.Height / _backgroundImage.Height);
            int width = (int)(_backgroundImage.Width * scale);
            int height = (int)(_backgroundImage.Height * scale);
            return new Rectangle((size.Width - width) / 2, (size.Height - height) / 2, width, height);
        }

        // =====================================================
        // CHUỘT TRÊN CANVAS
        // =====================================================
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            ShapeType shape = GetCurrentShape();
            bool fill = chkFill.Enabled && chkFill.Checked;

            _current = new DrawItem(shape, _penColor, (int)nudWidth.Value, fill, e.Location);
            _isDrawing = true;
            canvas.Invalidate();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCoordinates(e.Location);

            if (!_isDrawing || _current == null) return;

            _current.Extend(e.Location);
            canvas.Invalidate();
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isDrawing || _current == null || e.Button != MouseButtons.Left) return;

            _current.Extend(e.Location);
            _items.Add(_current);
            _current = null;
            _isDrawing = false;

            canvas.Invalidate();
            UpdateCount();
        }

        private void canvas_MouseLeave(object sender, EventArgs e)
        {
            UpdateCoordinates(null);
        }

        // =====================================================
        // CÁC NÚT CHỨC NĂNG
        // =====================================================
        private void btnColor_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = _penColor;
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _penColor = dialog.Color;
                    pnlColorPreview.BackColor = _penColor;
                }
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = canvas.BackColor;
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    canvas.BackColor = dialog.Color;
                }
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void Undo()
        {
            if (_items.Count == 0)
            {
                lblStatus.Text = "Không còn nét vẽ nào để hoàn tác.";
                return;
            }

            _items.RemoveAt(_items.Count - 1);
            canvas.Invalidate();
            UpdateCount();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_items.Count == 0)
            {
                lblStatus.Text = "Vùng vẽ đang trống.";
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ nét vẽ?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            _items.Clear();
            canvas.Invalidate();
            UpdateCount();
        }

        // =====================================================
        // ẢNH NỀN VÀ LƯU PNG
        // =====================================================
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh nền";
                dialog.Filter = "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tất cả các tệp|*.*";
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (Image temp = Image.FromFile(dialog.FileName))
                    {
                        // Sao chép sang Bitmap để không khóa file ảnh
                        Image newImage = new Bitmap(temp);
                        if (_backgroundImage != null) _backgroundImage.Dispose();
                        _backgroundImage = newImage;
                    }
                    canvas.Invalidate();
                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể mở file ảnh đã chọn. Vui lòng chọn file ảnh hợp lệ.",
                        "Lỗi mở ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            if (_backgroundImage == null)
            {
                lblStatus.Text = "Chưa có ảnh nền.";
                return;
            }

            _backgroundImage.Dispose();
            _backgroundImage = null;
            canvas.Invalidate();
        }

        private void btnSavePng_Click(object sender, EventArgs e)
        {
            int width = canvas.ClientSize.Width;
            int height = canvas.ClientSize.Height;
            if (width <= 0 || height <= 0)
            {
                MessageBox.Show("Vùng vẽ không hợp lệ để lưu.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Lưu bản vẽ";
                dialog.Filter = "Ảnh PNG (*.png)|*.png";
                dialog.FileName = "BanVe.png";
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var bitmap = new Bitmap(width, height))
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(canvas.BackColor);
                        RenderScene(g, canvas.ClientSize);
                        bitmap.Save(dialog.FileName, ImageFormat.Png);
                    }
                    lblStatus.Text = "Đã lưu: " + dialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi lưu ảnh",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
