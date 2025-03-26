namespace Pacman
{
    class Pacman : PointMove
    {
        public int Lives { get; set; } = 3;
        public bool IsPowered { get; set; } = false;

        public Pacman(int x = 216, int y = 416, int speed = 1) : base(x, y, speed)
        {
            Sprite = new Bitmap(Properties.Resources.pacman);
        }

        public void Die()
        {
            X = 216;
            Y = 416;
            Lives--;
        }
    }
}
