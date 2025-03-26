namespace Pacman
{
    public class PointStatic
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Bitmap? Sprite { get; set; } = null;

        public PointStatic(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
