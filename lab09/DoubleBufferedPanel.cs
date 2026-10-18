namespace StudentCardPrinter
{
    /// <summary>
    /// Panel bật DoubleBuffered và tự vẽ lại khi đổi kích thước để preview không nhấp nháy.
    /// </summary>
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
