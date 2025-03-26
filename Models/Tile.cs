namespace Pacman
{
    class Tile : PointStatic
    {
        public Tile(int x, int y): base(x, y)
        {
            X = x * 16;
            Y = y * 16;
        }
    }
}
