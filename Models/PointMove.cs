namespace Pacman
{
    public class PointMove : PointStatic
    {
        public int Speed { get; protected set; }
        public int XDir { get; protected set; }
        public int YDir { get; protected set; }

        public PointMove(int x, int y, int speed) : base(x, y)
        {
            Speed = speed;

        }

        public void Move()
        {
            if (X == 448)
            {
                X = -10;
            }

            else if (X == -16)
            {
                X = 446;
            }
            else
            {
                X += Speed * XDir;
                Y += Speed * YDir;
            }
        }

        public void ChangeDirection(int xDir, int yDir)
        {
            XDir = xDir;
            YDir = yDir;
        }

        public bool CheckCollision(int tx1, int ty1, int dx, int dy)
        {
            int tx2 = tx1 + 16;
            int ty2 = ty1 + 16;

            int x2 = X + 16 + Speed * dx;
            int y2 = Y + 16 + Speed * dy;

            int x1 = X + Speed * dx;
            int y1 = Y + Speed * dy;

            if (((x2 > tx1) && (y2 > ty1)) && ((x2 < tx2) && (y2 <= ty2)) ||
                ((x2 > tx1) && (y1 > ty1)) && ((x2 <= tx2) && (y1 < ty2)) ||
                ((x1 > tx1) && (y1 >= ty1)) && ((x1 < tx2) && (y1 < ty2)) ||
                ((x1 >= tx1) && (y2 > ty1)) && ((x1 < tx2) && (y2 < ty2))
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckCollision(int tx1, int ty1, int size)
        {
            int tx2 = tx1 + size;
            int ty2 = ty1 + size;

            int x2 = X + size + Speed * XDir;
            int y2 = Y + size + Speed * YDir;

            int x1 = X + Speed * XDir;
            int y1 = Y + Speed * YDir;

            if (((x2 > tx1) && (y2 > ty1)) && ((x2 < tx2) && (y2 <= ty2)) ||
                  ((x2 > tx1) && (y1 > ty1)) && ((x2 <= tx2) && (y1 < ty2)) ||
                  ((x1 > tx1) && (y1 >= ty1)) && ((x1 < tx2) && (y1 < ty2)) ||
                  ((x1 >= tx1) && (y2 > ty1)) && ((x1 < tx2) && (y2 < ty2))
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
