namespace Pacman
{
    class Dot : Tile
    {
        public int Score { get; }

        public Dot(int x, int y): base(x, y)
        {
            Score = 10;
        }

        protected Dot(int x, int y, int score) : base(x, y)
        {
            Score = score;
        }
    }
}
