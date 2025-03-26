namespace Pacman
{
    public class Ghost : PointMove
    {
        public bool IsDead { get; set; }

        public Ghost(int x, int y, int speed = 1): base(x, y, speed)
        {
            IsDead = false;
            Sprite = new Bitmap(Properties.Resources.redDown);
        }

        public void Respawn()
        {
            X = 216;

            Y = 224;

            IsDead = false;
        }
    }
}
