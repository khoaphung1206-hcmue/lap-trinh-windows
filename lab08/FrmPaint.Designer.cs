namespace SimplePaint
{
    partial class FrmPaint
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
            pnlTool = new Panel();
            lblMode = new Label();
            cboMode = new ComboBox();
            lblWidth = new Label();
            nudWidth = new NumericUpDown();
            btnColor = new Button();
            pnlColorPreview = new Panel();
            chkFill = new CheckBox();
            btnUndo = new Button();
            btnClear = new Button();
            btnBackColor = new Button();
            btnOpenImage = new Button();
            btnRemoveImage = new Button();
            btnSavePng = new Button();
            stsMain = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCount = new ToolStripStatusLabel();
            canvas = new DrawSurface();
            pnlTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWidth).BeginInit();
            stsMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTool
            // 
            pnlTool.Controls.Add(lblMode);
            pnlTool.Controls.Add(cboMode);
            pnlTool.Controls.Add(lblWidth);
            pnlTool.Controls.Add(nudWidth);
            pnlTool.Controls.Add(btnColor);
            pnlTool.Controls.Add(pnlColorPreview);
            pnlTool.Controls.Add(chkFill);
            pnlTool.Controls.Add(btnUndo);
            pnlTool.Controls.Add(btnClear);
            pnlTool.Controls.Add(btnBackColor);
            pnlTool.Controls.Add(btnOpenImage);
            pnlTool.Controls.Add(btnRemoveImage);
            pnlTool.Controls.Add(btnSavePng);
            pnlTool.Dock = DockStyle.Top;
            pnlTool.Location = new Point(0, 0);
            pnlTool.Name = "pnlTool";
            pnlTool.Size = new Size(920, 98);
            pnlTool.TabIndex = 0;
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Location = new Point(12, 19);
            lblMode.Name = "lblMode";
            lblMode.Text = "Chế độ vẽ:";
            // 
            // cboMode
            // 
            cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMode.FormattingEnabled = true;
            cboMode.Location = new Point(85, 16);
            cboMode.Name = "cboMode";
            cboMode.Size = new Size(120, 23);
            cboMode.TabIndex = 0;
            cboMode.SelectedIndexChanged += cboMode_SelectedIndexChanged;
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(225, 19);
            lblWidth.Name = "lblWidth";
            lblWidth.Text = "Độ dày:";
            // 
            // nudWidth
            // 
            nudWidth.Location = new Point(282, 16);
            nudWidth.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudWidth.Name = "nudWidth";
            nudWidth.Size = new Size(60, 23);
            nudWidth.TabIndex = 1;
            nudWidth.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnColor
            // 
            btnColor.Location = new Point(362, 12);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(110, 30);
            btnColor.TabIndex = 2;
            btnColor.Text = "Chọn màu...";
            btnColor.UseVisualStyleBackColor = true;
            btnColor.Click += btnColor_Click;
            // 
            // pnlColorPreview
            // 
            pnlColorPreview.BackColor = Color.Black;
            pnlColorPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlColorPreview.Location = new Point(480, 14);
            pnlColorPreview.Name = "pnlColorPreview";
            pnlColorPreview.Size = new Size(28, 26);
            pnlColorPreview.TabIndex = 3;
            // 
            // chkFill
            // 
            chkFill.AutoSize = true;
            chkFill.Enabled = false;
            chkFill.Location = new Point(530, 17);
            chkFill.Name = "chkFill";
            chkFill.TabIndex = 4;
            chkFill.Text = "Tô màu hình";
            chkFill.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(12, 54);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(100, 32);
            btnUndo.TabIndex = 5;
            btnUndo.Text = "Undo";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(120, 54);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 32);
            btnClear.TabIndex = 6;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnBackColor
            // 
            btnBackColor.Location = new Point(228, 54);
            btnBackColor.Name = "btnBackColor";
            btnBackColor.Size = new Size(110, 32);
            btnBackColor.TabIndex = 7;
            btnBackColor.Text = "Màu nền";
            btnBackColor.UseVisualStyleBackColor = true;
            btnBackColor.Click += btnBackColor_Click;
            // 
            // btnOpenImage
            // 
            btnOpenImage.Location = new Point(346, 54);
            btnOpenImage.Name = "btnOpenImage";
            btnOpenImage.Size = new Size(120, 32);
            btnOpenImage.TabIndex = 8;
            btnOpenImage.Text = "Mở ảnh nền...";
            btnOpenImage.UseVisualStyleBackColor = true;
            btnOpenImage.Click += btnOpenImage_Click;
            // 
            // btnRemoveImage
            // 
            btnRemoveImage.Location = new Point(474, 54);
            btnRemoveImage.Name = "btnRemoveImage";
            btnRemoveImage.Size = new Size(120, 32);
            btnRemoveImage.TabIndex = 9;
            btnRemoveImage.Text = "Bỏ ảnh nền";
            btnRemoveImage.UseVisualStyleBackColor = true;
            btnRemoveImage.Click += btnRemoveImage_Click;
            // 
            // btnSavePng
            // 
            btnSavePng.Location = new Point(602, 54);
            btnSavePng.Name = "btnSavePng";
            btnSavePng.Size = new Size(110, 32);
            btnSavePng.TabIndex = 10;
            btnSavePng.Text = "Lưu PNG...";
            btnSavePng.UseVisualStyleBackColor = true;
            btnSavePng.Click += btnSavePng_Click;
            // 
            // stsMain
            // 
            stsMain.Items.AddRange(new ToolStripItem[] { lblStatus, lblCount });
            stsMain.Location = new Point(0, 598);
            stsMain.Name = "stsMain";
            stsMain.Size = new Size(920, 22);
            stsMain.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Spring = true;
            lblStatus.Text = "X: -, Y: -";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCount
            // 
            lblCount.Name = "lblCount";
            lblCount.Text = "Số nét vẽ: 0";
            // 
            // canvas
            // 
            canvas.BackColor = Color.White;
            canvas.BorderStyle = BorderStyle.FixedSingle;
            canvas.Cursor = Cursors.Cross;
            canvas.Dock = DockStyle.Fill;
            canvas.Location = new Point(0, 98);
            canvas.Name = "canvas";
            canvas.Size = new Size(920, 500);
            canvas.TabIndex = 1;
            canvas.Paint += canvas_Paint;
            canvas.MouseDown += canvas_MouseDown;
            canvas.MouseLeave += canvas_MouseLeave;
            canvas.MouseMove += canvas_MouseMove;
            canvas.MouseUp += canvas_MouseUp;
            // 
            // FrmPaint
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 620);
            Controls.Add(canvas);
            Controls.Add(stsMain);
            Controls.Add(pnlTool);
            MinimumSize = new Size(780, 480);
            Name = "FrmPaint";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Simple Paint";
            FormClosed += FrmPaint_FormClosed;
            Load += FrmPaint_Load;
            pnlTool.ResumeLayout(false);
            pnlTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudWidth).EndInit();
            stsMain.ResumeLayout(false);
            stsMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTool;
        private Label lblMode;
        private ComboBox cboMode;
        private Label lblWidth;
        private NumericUpDown nudWidth;
        private Button btnColor;
        private Panel pnlColorPreview;
        private CheckBox chkFill;
        private Button btnUndo;
        private Button btnClear;
        private Button btnBackColor;
        private Button btnOpenImage;
        private Button btnRemoveImage;
        private Button btnSavePng;
        private StatusStrip stsMain;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCount;
        private DrawSurface canvas;
    }
}
