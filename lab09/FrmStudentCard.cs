using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace StudentCardPrinter
{
    public partial class FrmStudentCard : Form
    {
        private static readonly Color DefaultCardColor = Color.FromArgb(232, 241, 251);

        private Bitmap _photo;                       // ảnh đại diện (bản sao, không khóa file gốc)
        private Color _cardColor = DefaultCardColor; // màu nền thẻ

        public FrmStudentCard()
        {
            InitializeComponent();
        }

        // =====================================================
        // LOAD / ĐÓNG FORM
        // =====================================================
        private void FrmStudentCard_Load(object sender, EventArgs e)
        {
            cboClass.Items.AddRange(new object[] { "51.01.104", "51.01.103", "51.01.102", "51.01.101" });
            cboFaculty.Items.AddRange(new object[]
            {
                "Công nghệ Thông tin", "Toán - Tin học", "Vật lý", "Hóa học", "Ngữ văn", "Giáo dục Tiểu học"
            });
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });

            dtpBirthDate.MaxDate = DateTime.Today;
            ResetForm();
        }

        private void FrmStudentCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_photo != null)
            {
                _photo.Dispose();
                _photo = null;
            }
        }

        // =====================================================
        // TIỆN ÍCH
        // =====================================================
        private StudentCardInfo BuildInfo()
        {
            return new StudentCardInfo
            {
                StudentId = txtStudentId.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                ClassName = cboClass.Text,
                Faculty = cboFaculty.Text,
                Course = txtCourse.Text.Trim(),
                BirthDate = dtpBirthDate.Value.Date,
                Gender = cboGender.Text,
                Photo = _photo
            };
        }

        private void SetPhoto(Bitmap newPhoto)
        {
            Bitmap oldPhoto = _photo;
            _photo = newPhoto;
            picAvatar.Image = _photo;   // PictureBox dùng SizeMode = Zoom nên ảnh không méo

            if (oldPhoto != null)
            {
                oldPhoto.Dispose();
            }
            pnlCardPreview.Invalidate();
        }

        private void ResetForm()
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtCourse.Clear();
            cboClass.SelectedIndex = 0;
            cboFaculty.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            dtpBirthDate.Value = new DateTime(2005, 1, 1);
            SetPhoto(null);
            _cardColor = DefaultCardColor;
            errorProvider.Clear();
            pnlCardPreview.Invalidate();
            txtStudentId.Focus();
        }

        /// <summary>
        /// Kiểm tra dữ liệu bắt buộc: mã sinh viên, họ tên, ảnh đại diện.
        /// </summary>
        private bool ValidateInput()
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

            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
                AddError(txtStudentId, "Mã sinh viên không được để trống.");

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
                AddError(txtFullName, "Họ tên không được để trống.");

            if (_photo == null)
                AddError(btnChoosePhoto, "Vui lòng chọn ảnh đại diện.");

            if (errors.Count > 0)
            {
                MessageBox.Show("Không thể thực hiện vì thiếu dữ liệu bắt buộc:\n" + string.Join("\n", errors),
                    "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (firstInvalid != null) firstInvalid.Focus();
                return false;
            }
            return true;
        }

        // Mọi thay đổi dữ liệu nhập đều vẽ lại preview
        private void InputChanged(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                errorProvider.SetError(control, "");
            }
            pnlCardPreview.Invalidate();
        }

        // =====================================================
        // PREVIEW THẺ BẰNG GDI+
        // =====================================================
        private void pnlCardPreview_Paint(object sender, PaintEventArgs e)
        {
            Rectangle area = CardRenderer.FitRectangle(pnlCardPreview.ClientSize, 16);
            CardRenderer.Draw(e.Graphics, area, BuildInfo(), _cardColor);
        }

        // =====================================================
        // CHỌN ẢNH, MÀU NỀN, LƯU PNG
        // =====================================================
        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh đại diện";
                dialog.Filter = "Tệp ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (Image temp = Image.FromFile(dialog.FileName))
                    {
                        // Sao chép sang Bitmap mới để không khóa file ảnh trên ổ đĩa
                        SetPhoto(new Bitmap(temp));
                    }
                    errorProvider.SetError(btnChoosePhoto, "");
                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể mở file ảnh đã chọn. Vui lòng chọn file ảnh hợp lệ.",
                        "Lỗi mở ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCardColor_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = _cardColor;
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _cardColor = dialog.Color;
                    pnlCardPreview.Invalidate();
                }
            }
        }

        private void btnSavePng_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Lưu thẻ sinh viên";
                dialog.Filter = "Ảnh PNG (*.png)|*.png";
                dialog.FileName = "TheSinhVien.png";
                if (dialog.ShowDialog() != DialogResult.OK) return;

                try
                {
                    int width = 1000;
                    int height = (int)(width / CardRenderer.AspectRatio);
                    using (var bitmap = new Bitmap(width, height))
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                        CardRenderer.Draw(g, new Rectangle(0, 0, width, height), BuildInfo(), _cardColor);
                        bitmap.Save(dialog.FileName, ImageFormat.Png);
                    }
                    MessageBox.Show("Đã lưu thẻ sinh viên:\n" + dialog.FileName, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi lưu ảnh",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =====================================================
        // LÀM MỚI / THOÁT
        // =====================================================
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        // =====================================================
        // IN ẤN
        // =====================================================
        private void btnPreviewPrint_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                printPreviewDialog1.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở xem trước khi in: " + ex.Message, "Lỗi in ấn",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (printDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể in thẻ: " + ex.Message, "Lỗi in ấn",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Đơn vị in mặc định là 1/100 inch. Thẻ in rộng tối đa 6 inch, đặt giữa trang, đúng tỉ lệ.
            Rectangle margin = e.MarginBounds;
            int cardWidth = Math.Min(margin.Width, 600);
            int cardHeight = (int)(cardWidth / CardRenderer.AspectRatio);
            int x = margin.Left + (margin.Width - cardWidth) / 2;
            int y = margin.Top;

            CardRenderer.Draw(e.Graphics, new Rectangle(x, y, cardWidth, cardHeight), BuildInfo(), _cardColor);
            e.HasMorePages = false;
        }
    }
}
