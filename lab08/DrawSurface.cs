namespace SimplePaint
{
    /// <summary>
    /// Panel bật DoubleBuffered để vẽ không bị nhấp nháy.
    /// </summary>
    public class DrawSurface : Panel
    {
        public DrawSurface()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
