namespace StudentCardPrinter
{
    partial class FrmStudentCard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            grpThongTin = new GroupBox();
            lblStudentId = new Label();
            txtStudentId = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblClass = new Label();
            cboClass = new ComboBox();
            lblFaculty = new Label();
            cboFaculty = new ComboBox();
            lblCourse = new Label();
            txtCourse = new TextBox();
            lblBirthDate = new Label();
            dtpBirthDate = new DateTimePicker();
            lblGender = new Label();
            cboGender = new ComboBox();
            picAvatar = new PictureBox();
            btnChoosePhoto = new Button();
            btnCardColor = new Button();
            btnClear = new Button();
            btnPreviewPrint = new Button();
            btnPrint = new Button();
            btnSavePng = new Button();
            btnExit = new Button();
            grpPreview = new GroupBox();
            pnlCardPreview = new DoubleBufferedPanel();
            errorProvider = new ErrorProvider(components);
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printPreviewDialog1 = new PrintPreviewDialog();
            printDialog1 = new PrintDialog();
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblStudentId);
            grpThongTin.Controls.Add(txtStudentId);
            grpThongTin.Controls.Add(lblFullName);
            grpThongTin.Controls.Add(txtFullName);
            grpThongTin.Controls.Add(lblClass);
            grpThongTin.Controls.Add(cboClass);
            grpThongTin.Controls.Add(lblFaculty);
            grpThongTin.Controls.Add(cboFaculty);
            grpThongTin.Controls.Add(lblCourse);
            grpThongTin.Controls.Add(txtCourse);
            grpThongTin.Controls.Add(lblBirthDate);
            grpThongTin.Controls.Add(dtpBirthDate);
            grpThongTin.Controls.Add(lblGender);
            grpThongTin.Controls.Add(cboGender);
            grpThongTin.Controls.Add(picAvatar);
            grpThongTin.Controls.Add(btnChoosePhoto);
            grpThongTin.Controls.Add(btnCardColor);
            grpThongTin.Location = new Point(12, 12);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(458, 448);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin sinh viên";
            // 
            // lblStudentId
            // 
            lblStudentId.AutoSize = true;
            lblStudentId.Location = new Point(20, 33);
            lblStudentId.Name = "lblStudentId";
            lblStudentId.Text = "Mã sinh viên:";
            // 
            // txtStudentId
            // 
            txtStudentId.Location = new Point(130, 30);
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Size = new Size(300, 23);
            txtStudentId.TabIndex = 0;
            txtStudentId.TextChanged += InputChanged;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 73);
            lblFullName.Name = "lblFullName";
            lblFullName.Text = "Họ tên:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(130, 70);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(300, 23);
            txtFullName.TabIndex = 1;
            txtFullName.TextChanged += InputChanged;
            // 
            // lblClass
            // 
            lblClass.AutoSize = true;
            lblClass.Location = new Point(20, 113);
            lblClass.Name = "lblClass";
            lblClass.Text = "Lớp:";
            // 
            // cboClass
            // 
            cboClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClass.FormattingEnabled = true;
            cboClass.Location = new Point(130, 110);
            cboClass.Name = "cboClass";
            cboClass.Size = new Size(300, 23);
            cboClass.TabIndex = 2;
            cboClass.SelectedIndexChanged += InputChanged;
            // 
            // lblFaculty
            // 
            lblFaculty.AutoSize = true;
            lblFaculty.Location = new Point(20, 153);
            lblFaculty.Name = "lblFaculty";
            lblFaculty.Text = "Khoa:";
            // 
            // cboFaculty
            // 
            cboFaculty.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFaculty.FormattingEnabled = true;
            cboFaculty.Location = new Point(130, 150);
            cboFaculty.Name = "cboFaculty";
            cboFaculty.Size = new Size(300, 23);
            cboFaculty.TabIndex = 3;
            cboFaculty.SelectedIndexChanged += InputChanged;
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(20, 193);
            lblCourse.Name = "lblCourse";
            lblCourse.Text = "Khóa học:";
            // 
            // txtCourse
            // 
            txtCourse.Location = new Point(130, 190);
            txtCourse.Name = "txtCourse";
            txtCourse.PlaceholderText = "VD: K49";
            txtCourse.Size = new Size(300, 23);
            txtCourse.TabIndex = 4;
            txtCourse.TextChanged += InputChanged;
            // 
            // lblBirthDate
            // 
            lblBirthDate.AutoSize = true;
            lblBirthDate.Location = new Point(20, 233);
            lblBirthDate.Name = "lblBirthDate";
            lblBirthDate.Text = "Ngày sinh:";
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Format = DateTimePickerFormat.Short;
            dtpBirthDate.Location = new Point(130, 230);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(160, 23);
            dtpBirthDate.TabIndex = 5;
            dtpBirthDate.ValueChanged += InputChanged;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(20, 273);
            lblGender.Name = "lblGender";
            lblGender.Text = "Giới tính:";
            // 
            // cboGender
            // 
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.FormattingEnabled = true;
            cboGender.Location = new Point(130, 270);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(160, 23);
            cboGender.TabIndex = 6;
            cboGender.SelectedIndexChanged += InputChanged;
            // 
            // picAvatar
            // 
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.Location = new Point(130, 315);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(120, 120);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabStop = false;
            // 
            // btnChoosePhoto
            // 
            btnChoosePhoto.Location = new Point(265, 315);
            btnChoosePhoto.Name = "btnChoosePhoto";
            btnChoosePhoto.Size = new Size(165, 34);
            btnChoosePhoto.TabIndex = 7;
            btnChoosePhoto.Text = "Chọn ảnh...";
            btnChoosePhoto.UseVisualStyleBackColor = true;
            btnChoosePhoto.Click += btnChoosePhoto_Click;
            // 
            // btnCardColor
            // 
            btnCardColor.Location = new Point(265, 359);
            btnCardColor.Name = "btnCardColor";
            btnCardColor.Size = new Size(165, 34);
            btnCardColor.TabIndex = 8;
            btnCardColor.Text = "Màu nền thẻ...";
            btnCardColor.UseVisualStyleBackColor = true;
            btnCardColor.Click += btnCardColor_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(12, 474);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(86, 36);
            btnClear.TabIndex = 9;
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnPreviewPrint
            // 
            btnPreviewPrint.Location = new Point(105, 474);
            btnPreviewPrint.Name = "btnPreviewPrint";
            btnPreviewPrint.Size = new Size(86, 36);
            btnPreviewPrint.TabIndex = 10;
            btnPreviewPrint.Text = "Xem trước in";
            btnPreviewPrint.UseVisualStyleBackColor = true;
            btnPreviewPrint.Click += btnPreviewPrint_Click;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(198, 474);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(86, 36);
            btnPrint.TabIndex = 11;
            btnPrint.Text = "In thẻ";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnSavePng
            // 
            btnSavePng.Location = new Point(291, 474);
            btnSavePng.Name = "btnSavePng";
            btnSavePng.Size = new Size(86, 36);
            btnSavePng.TabIndex = 12;
            btnSavePng.Text = "Lưu PNG";
            btnSavePng.UseVisualStyleBackColor = true;
            btnSavePng.Click += btnSavePng_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(384, 474);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(86, 36);
            btnExit.TabIndex = 13;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // grpPreview
            // 
            grpPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpPreview.Controls.Add(pnlCardPreview);
            grpPreview.Location = new Point(480, 12);
            grpPreview.Name = "grpPreview";
            grpPreview.Size = new Size(508, 498);
            grpPreview.TabIndex = 1;
            grpPreview.TabStop = false;
            grpPreview.Text = "Xem trước thẻ sinh viên";
            // 
            // pnlCardPreview
            // 
            pnlCardPreview.BackColor = Color.FromArgb(235, 235, 235);
            pnlCardPreview.Dock = DockStyle.Fill;
            pnlCardPreview.Location = new Point(3, 19);
            pnlCardPreview.Name = "pnlCardPreview";
            pnlCardPreview.Size = new Size(502, 476);
            pnlCardPreview.TabIndex = 0;
            pnlCardPreview.Paint += pnlCardPreview_Paint;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // printDocument1
            // 
            printDocument1.DocumentName = "The sinh vien";
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.ClientSize = new Size(900, 650);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Text = "Xem trước khi in";
            printPreviewDialog1.UseAntiAlias = true;
            printPreviewDialog1.Visible = false;
            // 
            // printDialog1
            // 
            printDialog1.Document = printDocument1;
            printDialog1.UseEXDialog = true;
            // 
            // FrmStudentCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 522);
            Controls.Add(grpThongTin);
            Controls.Add(btnClear);
            Controls.Add(btnPreviewPrint);
            Controls.Add(btnPrint);
            Controls.Add(btnSavePng);
            Controls.Add(btnExit);
            Controls.Add(grpPreview);
            MinimumSize = new Size(900, 560);
            Name = "FrmStudentCard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thiết kế và in thẻ sinh viên";
            FormClosed += FrmStudentCard_FormClosed;
            Load += FrmStudentCard_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            grpPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblStudentId;
        private TextBox txtStudentId;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblClass;
        private ComboBox cboClass;
        private Label lblFaculty;
        private ComboBox cboFaculty;
        private Label lblCourse;
        private TextBox txtCourse;
        private Label lblBirthDate;
        private DateTimePicker dtpBirthDate;
        private Label lblGender;
        private ComboBox cboGender;
        private PictureBox picAvatar;
        private Button btnChoosePhoto;
        private Button btnCardColor;
        private Button btnClear;
        private Button btnPreviewPrint;
        private Button btnPrint;
        private Button btnSavePng;
        private Button btnExit;
        private GroupBox grpPreview;
        private DoubleBufferedPanel pnlCardPreview;
        private ErrorProvider errorProvider;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        private PrintDialog printDialog1;
    }
}
