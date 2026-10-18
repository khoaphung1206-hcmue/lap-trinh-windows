using System.Drawing.Drawing2D;

namespace SimplePaint
{
    /// <summary>
    /// Lưu dữ liệu gốc của một nét vẽ (loại hình, màu, độ dày, các điểm).
    /// Canvas sẽ vẽ lại toàn bộ danh sách DrawItem trong sự kiện Paint.
    /// </summary>
    public class DrawItem
    {
        public ShapeType Shape { get; }
        public Color StrokeColor { get; }
        public int StrokeWidth { get; }
        public bool Fill { get; }

        public Point Start { get; private set; }
        public Point End { get; private set; }

        // Chỉ dùng cho chế độ Pen (vẽ tự do)
        public List<Point> Points { get; } = new List<Point>();

        public DrawItem(ShapeType shape, Color strokeColor, int strokeWidth, bool fill, Point start)
        {
            Shape = shape;
            StrokeColor = strokeColor;
            StrokeWidth = strokeWidth;
            Fill = fill;
            Start = start;
            End = start;

            if (shape == ShapeType.Pen)
            {
                Points.Add(start);
            }
        }

        /// <summary>
        /// Cập nhật điểm cuối khi kéo chuột.
        /// </summary>
        public void Extend(Point point)
        {
            End = point;
            if (Shape == ShapeType.Pen)
            {
                Points.Add(point);
            }
        }

        /// <summary>
        /// Hình chữ nhật bao quanh Start-End, xử lý được kéo chuột theo mọi hướng.
        /// </summary>
        public Rectangle GetBounds()
        {
            int x = Math.Min(Start.X, End.X);
            int y = Math.Min(Start.Y, End.Y);
            int width = Math.Abs(End.X - Start.X);
            int height = Math.Abs(End.Y - Start.Y);
            return new Rectangle(x, y, width, height);
        }

        public void Draw(Graphics g)
        {
            using (var pen = new Pen(StrokeColor, StrokeWidth))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                switch (Shape)
                {
                    case ShapeType.Pen:
                        DrawFreehand(g, pen);
                        break;

                    case ShapeType.Line:
                        g.DrawLine(pen, Start, End);
                        break;

                    case ShapeType.Rectangle:
                        Rectangle rect = GetBounds();
                        if (Fill)
                        {
                            using (var brush = new SolidBrush(StrokeColor))
                            {
                                g.FillRectangle(brush, rect);
                            }
                        }
                        g.DrawRectangle(pen, rect);
                        break;

                    case ShapeType.Ellipse:
                        Rectangle bounds = GetBounds();
                        if (Fill)
                        {
                            using (var brush = new SolidBrush(StrokeColor))
                            {
                                g.FillEllipse(brush, bounds);
                            }
                        }
                        g.DrawEllipse(pen, bounds);
                        break;
                }
            }
        }

        private void DrawFreehand(Graphics g, Pen pen)
        {
            if (Points.Count >= 2)
            {
                g.DrawLines(pen, Points.ToArray());
            }
            else if (Points.Count == 1)
            {
                // Chỉ click một lần: vẽ một chấm tròn có đường kính bằng độ dày nét
                using (var brush = new SolidBrush(StrokeColor))
                {
                    float half = StrokeWidth / 2f;
                    g.FillEllipse(brush, Points[0].X - half, Points[0].Y - half, StrokeWidth, StrokeWidth);
                }
            }
        }
    }
}
