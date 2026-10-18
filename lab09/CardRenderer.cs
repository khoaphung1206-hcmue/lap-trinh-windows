using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace StudentCardPrinter
{
    /// <summary>
    /// Vẽ thẻ sinh viên bằng GDI+ trong không gian thiết kế 400 x 252.
    /// Cùng một hàm Draw được dùng cho preview, in ấn và lưu PNG.
    /// </summary>
    public static class CardRenderer
    {
        public const float DesignWidth = 400f;
        public const float DesignHeight = 252f;
        public const float AspectRatio = DesignWidth / DesignHeight;

        /// <summary>
        /// Tính hình chữ nhật đặt vừa thẻ (đúng tỉ lệ) vào giữa vùng chứa.
        /// </summary>
        public static Rectangle FitRectangle(Size container, int margin)
        {
            int availableWidth = Math.Max(1, container.Width - 2 * margin);
            int availableHeight = Math.Max(1, container.Height - 2 * margin);

            int width = availableWidth;
            int height = (int)(width / AspectRatio);
            if (height > availableHeight)
            {
                height = availableHeight;
                width = (int)(height * AspectRatio);
            }

            int x = (container.Width - width) / 2;
            int y = (container.Height - height) / 2;
            return new Rectangle(x, y, width, height);
        }

        public static void Draw(Graphics g, Rectangle bounds, StudentCardInfo info, Color cardColor)
        {
            GraphicsState state = g.Save();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Mọi tọa độ bên dưới tính theo không gian 400 x 252, tự co giãn theo bounds
            g.TranslateTransform(bounds.X, bounds.Y);
            float scale = bounds.Width / DesignWidth;
            g.ScaleTransform(scale, scale);

            Color headerColor = Color.FromArgb(31, 78, 121);
            Color textColor = cardColor.GetBrightness() < 0.5f ? Color.White : Color.FromArgb(30, 30, 30);

            // Nền thẻ
            using (var backBrush = new SolidBrush(cardColor))
            {
                g.FillRectangle(backBrush, 0f, 0f, DesignWidth, DesignHeight);
            }

            // Dải tiêu đề
            using (var headerBrush = new SolidBrush(headerColor))
            {
                g.FillRectangle(headerBrush, 0f, 0f, DesignWidth, 58f);
            }
            using (var schoolFont = CreateFont(12f, FontStyle.Bold))
            using (var titleFont = CreateFont(21f, FontStyle.Bold))
            {
                DrawCentered(g, "ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH", schoolFont, Color.White,
                    new RectangleF(0f, 7f, DesignWidth, 18f));
                DrawCentered(g, "THẺ SINH VIÊN", titleFont, Color.Gold,
                    new RectangleF(0f, 25f, DesignWidth, 29f));
            }

            // Khung ngoài
            using (var borderPen = new Pen(headerColor, 4f))
            {
                g.DrawRectangle(borderPen, 2f, 2f, DesignWidth - 4f, DesignHeight - 4f);
            }

            DrawPhoto(g, info.Photo, new Rectangle(20, 68, 90, 120), headerColor);
            DrawFakeQr(g, info.StudentId, new RectangleF(40f, 194f, 49f, 49f), headerColor);

            // Thông tin sinh viên
            string[,] rows =
            {
                { "Mã SV:", info.StudentId },
                { "Họ tên:", info.FullName },
                { "Lớp:", info.ClassName },
                { "Khoa:", info.Faculty },
                { "Khóa:", info.Course },
                { "Ngày sinh:", info.BirthDate.ToString("dd/MM/yyyy") },
                { "Giới tính:", info.Gender }
            };

            using (var labelFont = CreateFont(12f, FontStyle.Bold))
            using (var valueFont = CreateFont(13f, FontStyle.Regular))
            {
                for (int i = 0; i < rows.GetLength(0); i++)
                {
                    float y = 68f + i * 25f;
                    DrawLeft(g, rows[i, 0], labelFont, textColor, new RectangleF(125f, y, 68f, 22f));
                    DrawLeft(g, rows[i, 1], valueFont, textColor, new RectangleF(193f, y, 192f, 22f));
                }
            }

            g.Restore(state);
        }

        private static Font CreateFont(float pixelSize, FontStyle style)
        {
            return new Font("Segoe UI", pixelSize, style, GraphicsUnit.Pixel);
        }

        private static void DrawCentered(Graphics g, string text, Font font, Color color, RectangleF area)
        {
            using (var brush = new SolidBrush(color))
            using (var format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                format.FormatFlags = StringFormatFlags.NoWrap;
                g.DrawString(text, font, brush, area, format);
            }
        }

        private static void DrawLeft(Graphics g, string text, Font font, Color color, RectangleF area)
        {
            using (var brush = new SolidBrush(color))
            using (var format = new StringFormat())
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
                format.FormatFlags = StringFormatFlags.NoWrap;
                format.Trimming = StringTrimming.EllipsisCharacter;
                g.DrawString(text ?? "", font, brush, area, format);
            }
        }

        /// <summary>
        /// Vẽ ảnh vào khung theo tỉ lệ gốc (Zoom) nên ảnh không bị méo.
        /// </summary>
        private static void DrawPhoto(Graphics g, Image photo, Rectangle box, Color borderColor)
        {
            g.FillRectangle(Brushes.White, box);

            if (photo != null)
            {
                Rectangle inner = Rectangle.Inflate(box, -2, -2);
                float scale = Math.Min((float)inner.Width / photo.Width, (float)inner.Height / photo.Height);
                float width = photo.Width * scale;
                float height = photo.Height * scale;
                float x = inner.X + (inner.Width - width) / 2f;
                float y = inner.Y + (inner.Height - height) / 2f;
                g.DrawImage(photo, x, y, width, height);
            }
            else
            {
                using (var font = CreateFont(12f, FontStyle.Regular))
                {
                    DrawCentered(g, "Ảnh 3x4", font, Color.Gray, box);
                }
            }

            using (var pen = new Pen(borderColor, 2f))
            {
                g.DrawRectangle(pen, box);
            }
        }

        /// <summary>
        /// Vẽ mã QR giả lập đơn giản (ô vuông) dựa trên mã sinh viên.
        /// </summary>
        private static void DrawFakeQr(Graphics g, string seedText, RectangleF area, Color color)
        {
            const int cells = 7;
            float cellSize = area.Width / cells;

            int seed = 17;
            unchecked
            {
                foreach (char c in seedText ?? "")
                {
                    seed = seed * 31 + c;
                }
            }
            var random = new Random(seed);

            g.FillRectangle(Brushes.White, area);
            using (var brush = new SolidBrush(color))
            {
                for (int row = 0; row < cells; row++)
                {
                    for (int col = 0; col < cells; col++)
                    {
                        bool topLeft = row < 3 && col < 3;
                        bool topRight = row < 3 && col >= cells - 3;
                        bool bottomLeft = row >= cells - 3 && col < 3;

                        bool filled;
                        if (topLeft || topRight || bottomLeft)
                        {
                            // Ô định vị ở 3 góc: vòng ngoài đặc, ô giữa trống
                            filled = !(row == 1 && col == 1) &&
                                     !(row == 1 && col == cells - 2) &&
                                     !(row == cells - 2 && col == 1);
                        }
                        else
                        {
                            filled = random.Next(2) == 0;
                        }

                        if (filled)
                        {
                            g.FillRectangle(brush, area.X + col * cellSize, area.Y + row * cellSize,
                                cellSize, cellSize);
                        }
                    }
                }
            }
        }
    }
}
