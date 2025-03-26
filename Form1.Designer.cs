using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using Timer = System.Windows.Forms.Timer;

namespace Pacman
{
    partial class PacmanForms
    {
        static readonly Timer pacmanMove = new Timer();

        static readonly Timer ghostMove = new Timer();

        static readonly Timer startMenu = new Timer();

        static readonly Timer startDeathMenu = new Timer();

        static readonly Timer powerUp = new Timer();

        static readonly Timer deathPenalty = new Timer();

        static readonly Timer pacmanAnimation = new Timer();

        static readonly Timer ghostAnimation = new Timer();

        Pacman pacman = new Pacman();

        Ghost[] ghosts = new Ghost[4];

        bool freeMove = false;

        int preMove = 0;

        byte dotsNumber = 0;

        bool showHelp = false;

        bool showMain = true;

        int animationFrame = 0;

        int ghostFrame = 0;

        int currentScore = 0;

        int highScore;

        Random rnd = new Random();

        Bitmap mainMenu = new Bitmap(Properties.Resources.MainMenu);
        Bitmap helpMenu = new Bitmap(Properties.Resources.HelpMenu);

        Tile[,] objectMap = new Tile[31, 28];

        Bitmap[] nums = new Bitmap[10];

        byte[,] digitMap = new byte[31, 28] {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, //1
            {1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //2
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //3
            {1,3,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,3,1 }, //4
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //5
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //6
            {1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1 }, //7
            {1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1 }, //8
            {1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1 }, //9
            {1,1,1,1,1,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,1,1,1,1,1 }, //10
            {0,0,0,0,0,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,0,0,0,0,0 }, //11
            {0,0,0,0,0,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,0,0,0,0,0 }, //12
            {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0 }, //13
            {1,1,1,1,1,1,2,1,1,0,1,0,0,0,0,0,0,1,0,1,1,2,1,1,1,1,1,1 }, //14
            {0,0,0,0,0,0,2,0,0,0,1,0,1,1,1,1,0,1,0,0,0,2,0,0,0,0,0,0 }, //15
            {1,1,1,1,1,1,2,1,1,0,1,0,0,0,0,0,0,1,0,1,1,2,1,1,1,1,1,1 }, //16
            {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0 }, //17
            {0,0,0,0,0,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,0,0,0,0,0 }, //18
            {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0 }, //19
            {1,1,1,1,1,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,1,1,1 }, //20
            {1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //21
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //22
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //23
            {1,3,2,2,1,1,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,1,1,2,2,3,1 }, //24
            {1,1,1,2,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,2,1,1,1 }, //25
            {1,1,1,2,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,2,1,1,1 }, //26
            {1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1 }, //27
            {1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1 }, //28
            {1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1 }, //29
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //30
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, //31
        };

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen wallPen = new Pen(Brushes.DeepSkyBlue);

            Pen dotPen = new Pen(Brushes.Pink);

            SolidBrush dotBrush = new SolidBrush(Color.Pink);

            string score = currentScore.ToString();
            char[] chScore = score.ToCharArray(); ;

            for (int i = 0; i < chScore.Length; i++)
            {
                e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 295 + 12 * i, 20);
            }

            score = highScore.ToString();
            chScore = score.ToCharArray();

            for (int i = 0; i < chScore.Length; i++)
            {
                e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 195 + 12 * i, 20);
            }

            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    //if (digitMap[i, j] == 1)
                    //e.Graphics.DrawRectangle(wallPen, objectMap[i, j].x, objectMap[i, j].y, 15, 15); // wall hitboxes

                    if (digitMap[i, j] == 2)
                    {
                        e.Graphics.FillRectangle(dotBrush, objectMap[i, j].X + 7, objectMap[i, j].Y + 7, 4, 4); // dots
                        //e.Graphics.DrawRectangle(dotPen, objectMap[i, j].x + 4, objectMap[i, j].y + 4, 8, 8);
                    }

                    if (digitMap[i, j] == 3)
                    {
                        e.Graphics.FillEllipse(dotBrush, objectMap[i, j].X + 2, objectMap[i, j].Y + 2, 12, 12); // energizer
                    }
                }
            }


            e.Graphics.DrawImage(pacman.Sprite, pacman.X - 5, pacman.Y - 5); // pacman
            //e.Graphics.FillRectangle(dotBrush, pacman.x, pacman.y, 16, 16); // pacman hitbox 

            e.Graphics.DrawImage(ghosts[0].Sprite, ghosts[0].X - 5, ghosts[0].Y - 5); // pink 
            //e.Graphics.FillRectangle(dotBrush, ghost[0].x, ghost[0].y, 16, 16);

            e.Graphics.DrawImage(ghosts[1].Sprite, ghosts[1].X - 5, ghosts[1].Y - 5); // red

            e.Graphics.DrawImage(ghosts[2].Sprite, ghosts[2].X - 5, ghosts[2].Y - 5); // blue
            //e.Graphics.FillRectangle(dotBrush, ghost[2].x, ghost[2].y, 16, 16);

            e.Graphics.DrawImage(ghosts[3].Sprite, ghosts[3].X - 5, ghosts[3].Y - 5); // orange
            //e.Graphics.FillRectangle(dotBrush, ghost[3].x, ghost[3].y, 16, 16);


            for (int i = 0; i < pacman.Lives; i++)
            {
                e.Graphics.DrawImage(Properties.Resources.LivesMeter, 24 + 24 * i, 548);
            }

            if (showMain == true)
            {
                e.Graphics.DrawImage(mainMenu, 0, 0);

                score = highScore.ToString();
                chScore = score.ToCharArray();
                if (pacman.Lives >= 0)
                {
                    for (int i = 0; i < chScore.Length; i++)
                    {
                        e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 195 + 12 * i, 20);
                    }
                }

                if (pacman.Lives < 0)
                {
                    score = currentScore.ToString();
                    chScore = score.ToCharArray(); ;

                    for (int i = 0; i < chScore.Length; i++)
                    {
                        e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 205 + 12 * i, 240);
                    }

                    score = highScore.ToString();
                    chScore = score.ToCharArray();

                    for (int i = 0; i < chScore.Length; i++)
                    {
                        e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 205 + 12 * i, 360);
                    }

                }
            }
            if (showHelp == true)
            {
                e.Graphics.DrawImage(helpMenu, 0, 0);
            }
        }

        private void PowerUp(object sender, EventArgs e)
        {
            pacman.IsPowered = false;
        }

        private void DeathPenalty(object sender, EventArgs e)
        {
            int target = -1;

            for (int i = 0; i < ghosts.Length; i++)
            {
                if (ghosts[i].IsDead == true)
                {
                    target = i;
                }
            }

            if (target >= 0)
            {
                ghosts[target].Respawn();
            }
            else
            {
                deathPenalty.Stop();
            }
        }

        private void StartMenu(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Keys.Space))
            {
                if (showHelp == false)
                {
                    showMain = false;
                    startMenu.Stop();
                    ghostMove.Start();
                    pacmanMove.Start();
                    pacmanAnimation.Start();
                    ghostAnimation.Start();

                    mainMenu = new Bitmap(Properties.Resources.PauseMenu, 448, 576);

                    this.Invalidate();
                }
            }
            if (Keyboard.IsKeyDown(Keys.Tab))
            {
                showHelp = true;
                showMain = false;
            }

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                if (showHelp == true)
                {
                    showMain = true;
                    showHelp = false;
                }
            }

            this.Invalidate();

        }

        private void StartDeathMenu(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Keys.Space))
            {

                mainMenu = new Bitmap(Properties.Resources.MainMenu, 448, 576);

                pacman.Lives = 3;

                currentScore = 0;

                ResetGame();

                startDeathMenu.Stop();

                startMenu.Start();

                this.Invalidate();
            }

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                Application.Exit();
            }

            this.Invalidate();

        }

        private void GhostTick(object sender, EventArgs e)
        {
            foreach (var ghost in ghosts)
            {
                GhostMove(ghost);
            }
        }

        private void PacmanTick(object sender, EventArgs e)
        {

            if (Keyboard.IsKeyDown(Keys.W))
            {
                preMove = 1;
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                preMove = 2;
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                preMove = 3;
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                preMove = 4;
            }

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                ghostMove.Stop();
                pacmanMove.Stop();
                showMain = true;
                startMenu.Start();
                this.Invalidate();
            }

            switch (preMove)
            {
                case 0:
                    if (MapCollision(pacman))
                    {
                        pacman.Move();
                    }
                    break;

                case 1:
                    MoveGen(0, -1);
                    break;

                case 2:
                    MoveGen(0, 1);
                    break;

                case 3:
                    MoveGen(-1, 0);
                    break;

                case 4:
                    MoveGen(1, 0);
                    break;
            }

            PacmanEat();
            
            /*Check for win*/
            if (dotsNumber == 0)
            {
                ResetGame();
            }

            this.Invalidate(); // нужна для перерисовки
        }

        public void PacmanEat()
        {
            /*Check collision for dots*/
            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    if (digitMap[i, j] == 2)
                    {
                        if (pacman.CheckCollision(objectMap[i, j].X + 4, objectMap[i, j].Y + 4, 8) == true)
                        {
                            digitMap[i, j] = 4;
                            objectMap[i, j] = new Tile(j, i);
                            dotsNumber--;
                            currentScore += 10;
                        }
                    }

                    if (digitMap[i, j] == 3)
                        if (pacman.CheckCollision(objectMap[i, j].X + 4, objectMap[i, j].Y + 4, 8) == true)
                        {
                            digitMap[i, j] = 5;
                            objectMap[i, j] = new Tile(j, i);
                            currentScore += 50;

                            powerUp.Stop();

                            pacman.IsPowered = true;

                            powerUp.Start();
                        }
                }
            }


            /*Check collision for Ghosts*/
            foreach (var ghost in ghosts)
            {
                if (pacman.CheckCollision(ghost.X, ghost.Y, 15) == true)
                {
                    if (pacman.IsPowered == false)
                    {
                        Reset();
                    }
                    else
                    {
                        Reset(ghost);
                    }
                }
            }
        }

        /// <summary>
        /// Resets round after pacman dies
        /// </summary>
        public void Reset()
        {
            System.Threading.Thread.Sleep(1000);

            foreach (var ghost in ghosts)
            {
                ghost.Respawn();
            }

            pacman.Die();

            if (pacman.Lives < 0)
            {
                if (currentScore > highScore)
                {
                    Properties.Settings.Default.HighScore = currentScore;
                    highScore = currentScore;
                    Properties.Settings.Default.Save();
                }
                ghostMove.Stop();
                pacmanMove.Stop();
                mainMenu = new Bitmap(Properties.Resources.DeathMenu);
                startDeathMenu.Start();
                showMain = true;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Sends killed ghost to prison and starts penalty time
        /// </summary>
        /// <param name="ghost">Ghost you want to send to prison</param>
        public void Reset(Ghost ghost)
        {
            currentScore += 200;

            ghost.X = 216;
            ghost.Y = 256;
            ghost.IsDead = true;

            deathPenalty.Start();
        }

        /// <summary>
        /// Chooses the direction of a moving point
        /// </summary>
        /// <param name="entity">Moving entity</param>
        public void GhostMove(PointMove entity)
        {
            freeMove = true;

            if (MapCollision(entity) && !(MapCollision(entity, entity.YDir, entity.XDir) || MapCollision(entity, -entity.YDir, -entity.XDir)))
            {
                entity.Move();
            }
            else
            {
                freeMove = true;

                if (rnd.Next(0, 2) == 1)
                {
                    if (entity.XDir == 0)
                    {
                        entity.ChangeDirection(1, 0);

                        if (entity.X > pacman.X)
                        {
                            entity.ChangeDirection(-1, 0);
                        }

                        if (pacman.IsPowered == true)
                        {
                            entity.ChangeDirection(-entity.XDir, 0);
                        }

                        if (!MapCollision(entity))
                        {
                            entity.ChangeDirection(entity.XDir * -1, 0);
                        }

                        entity.Move();

                    }
                    else
                    {
                        entity.ChangeDirection(0, 1);

                        if (entity.Y > pacman.Y)
                        {
                            entity.ChangeDirection(0, -1);
                        }

                        if (pacman.IsPowered == true)
                        {
                            entity.ChangeDirection(0, -entity.YDir);
                        }

                        if (!MapCollision(entity))
                        {
                            entity.ChangeDirection(0, entity.YDir * -1);
                        }

                        entity.Move();
                    }
                }

                else if (MapCollision(entity))
                {
                    entity.Move();
                }
            }

        }

        /// <summary>
        /// Check the collisions and decide whether to change direction of movement or not
        /// </summary>
        /// <param name="dx">x axis direction</param>
        /// <param name="dy">y axis direction</param>
        public void MoveGen(int dx, int dy)
        {
            if (!MapCollision(pacman, dx, dy))
            {
                freeMove = false;
            }

            if (freeMove == true)
            {
                pacman.ChangeDirection(dx, dy);
                pacman.Move();
                preMove = 0;
            }
            else if (MapCollision(pacman))
            {
                pacman.Move();
            }
        }


        /// <summary>
        /// Check if there are any collisions for the current direction
        /// </summary>
        /// <param name="entity">A moving entity with direction</param>
        /// <returns>True if the object collides; 
        /// otherwise False.</returns>
        public bool MapCollision(PointMove entity)
        {
            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    if (digitMap[i, j] == 1)
                    {
                        if (entity.CheckCollision(objectMap[i, j].X, objectMap[i, j].Y, 16) == true)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Check if there are any collisions for the chosen direction
        /// </summary>
        /// <param name="entity">A moving entity</param>
        /// <param name="dx">x axis direction</param>
        /// <param name="dy">y axis direction</param>
        /// <returns>True if object collides, otherwise False</returns>
        public bool MapCollision(PointMove entity, int dx, int dy)
        {
            
            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    if (digitMap[i, j] == 1)
                    {
                        if (entity.CheckCollision(objectMap[i, j].X, objectMap[i, j].Y, dx, dy) == true)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static class Keyboard
        {
            private static readonly HashSet<Keys> keys = new HashSet<Keys>();

            public static void OnKeyDown(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode) == false)
                {
                    keys.Add(e.KeyCode);
                }
            }

            public static void OnKeyUp(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode))
                {
                    keys.Remove(e.KeyCode);
                }
            }

            public static bool IsKeyDown(Keys key)
            {
                return keys.Contains(key);
            }
        }

        private void PacmanAnimation(object sender, EventArgs e)
        {
            switch (animationFrame)
            {
                case 0:
                    if (pacman.XDir == 1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pRight1);
                    }
                    if (pacman.XDir == -1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pLeft1);
                    }
                    if (pacman.YDir == 1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pDown1);
                    }
                    if (pacman.YDir == -1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pUp1);
                    }

                    animationFrame++;
                    break;

                case 1:
                    if (pacman.XDir == 1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pRight);
                    }
                    if (pacman.XDir == -1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pLeft);
                    }
                    if (pacman.YDir == 1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pDown);
                    }
                    if (pacman.YDir == -1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pUp);
                    }

                    animationFrame++;
                    break;

                case 2:
                    if (pacman.XDir == 1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pRight1);
                    }
                    if (pacman.XDir == -1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pLeft1);
                    }
                    if (pacman.YDir == 1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pDown1);
                    }
                    if (pacman.YDir == -1)
                    {
                        pacman.Sprite = new Bitmap(Properties.Resources.pUp1);
                    }

                    animationFrame++;
                    break;

                case 3:
                    pacman.Sprite = new Bitmap(Properties.Resources.pacman);
                    animationFrame = 0;
                    break;

            }

        }

        private void GhostAnimation(object sender, EventArgs e)
        {
            switch (ghostFrame)
            {
                case 0:

                    if (pacman.IsPowered == true)
                    {
                        for (int i = 0; i <= 3; i++)
                        {
                            ghosts[i].Sprite = new Bitmap(Properties.Resources.frightened);
                        }
                    }
                    else
                    {
                        if (ghosts[0].XDir == 1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkRight1);
                        }
                        if (ghosts[0].XDir == -1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkLeft1);
                        }
                        if (ghosts[0].YDir == 1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkDown1);
                        }
                        if (ghosts[0].YDir == -1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkUp1);
                        }
                        if (ghosts[1].XDir == 1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redRight1);
                        }
                        if (ghosts[1].XDir == -1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redLeft1);
                        }
                        if (ghosts[1].YDir == 1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redDown1);
                        }
                        if (ghosts[1].YDir == -1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redUp1);
                        }

                        if (ghosts[2].XDir == 1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueRight1);
                        }
                        if (ghosts[2].XDir == -1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueLeft1);
                        }
                        if (ghosts[2].YDir == 1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueDown1);
                        }
                        if (ghosts[2].YDir == -1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueUp1);
                        }

                        if (ghosts[3].XDir == 1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeRight1);
                        }
                        if (ghosts[3].XDir == -1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeLeft1);
                        }
                        if (ghosts[3].YDir == 1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeDown1);
                        }
                        if (ghosts[3].YDir == -1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeUp1);
                        }
                    }
                    ghostFrame++;
                    break;

                case 1:

                    if (pacman.IsPowered == true)
                    {
                        for (int i = 0; i <= 3; i++)
                        {
                            ghosts[i].Sprite = new Bitmap(Properties.Resources.frightened1);
                        }
                    }
                    else
                    {
                        if (ghosts[0].XDir == 1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkRight);
                        }
                        if (ghosts[0].XDir == -1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkLeft);
                        }
                        if (ghosts[0].YDir == 1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkDown);
                        }
                        if (ghosts[0].YDir == -1)
                        {
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkUp);
                        }

                        if (ghosts[1].XDir == 1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redRight);
                        }
                        if (ghosts[1].XDir == -1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redLeft);
                        }
                        if (ghosts[1].YDir == 1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redDown);
                        }
                        if (ghosts[1].YDir == -1)
                        {
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redUp);
                        }

                        if (ghosts[2].XDir == 1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueRight);
                        }
                        if (ghosts[2].XDir == -1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueLeft);
                        }
                        if (ghosts[2].YDir == 1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueDown);
                        }
                        if (ghosts[2].YDir == -1)
                        {
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueUp);
                        }

                        if (ghosts[3].XDir == 1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeRight);
                        }
                        if (ghosts[3].XDir == -1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeLeft);
                        }
                        if (ghosts[3].YDir == 1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeDown);
                        }
                        if (ghosts[3].YDir == -1)
                        {
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeUp);
                        }
                    }
                    ghostFrame--;
                    break;
            }
        }


        /// <summary>
        /// Resets dots, energizers, ghosts and pacman
        /// </summary>
        public void ResetGame()
        {
            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    if (digitMap[i, j] == 4)
                    {
                        digitMap[i, j] = 2;
                        objectMap[i, j] = new Dot(j, i + 3);
                        dotsNumber++;
                    }
                    else if (digitMap[i, j] == 5)
                    {
                        digitMap[i, j] = 3;
                        objectMap[i, j] = new Energizer(j, i + 3);
                    }
                }
            }

            foreach (var ghost in ghosts)
            {
                ghost.Respawn();
            }

            pacman.X = 216;
            pacman.Y = 416;

            pacman.ChangeDirection(0, 0);
        }

        public PacmanForms()
        {
            InitializeComponent();
            DoubleBuffered = true; // fixes flickering
            KeyPreview = true;

            ghosts[0] = new Ghost(192, 224);
            ghosts[1] = new Ghost(208, 224);
            ghosts[2] = new Ghost(224, 224);
            ghosts[3] = new Ghost(240, 224);

            foreach (var ghost in ghosts)
            {
                ghost.ChangeDirection(1 - 2 * rnd.Next(0, 2), 0);
            }

            rnd = new Random();

            KeyDown += Keyboard.OnKeyDown;
            KeyUp += Keyboard.OnKeyUp;

            highScore = Properties.Settings.Default.HighScore;

            /* timer creation */

            pacmanMove.Tick += new EventHandler(PacmanTick);
            pacmanMove.Interval = 1;

            ghostMove.Tick += new EventHandler(GhostTick);
            ghostMove.Interval = 1;

            startMenu.Tick += new EventHandler(StartMenu);
            startMenu.Interval = 1;
            startMenu.Start();

            startDeathMenu.Tick += new EventHandler(StartDeathMenu);
            startDeathMenu.Interval = 1;

            powerUp.Tick += new EventHandler(PowerUp);
            powerUp.Interval = 10000;

            deathPenalty.Tick += new EventHandler(DeathPenalty);
            deathPenalty.Interval = 10000;

            pacmanAnimation.Tick += new EventHandler(PacmanAnimation);
            pacmanAnimation.Interval = 60;

            ghostAnimation.Tick += new EventHandler(GhostAnimation);
            ghostAnimation.Interval = 60;


            /* tile map init */
            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    if (digitMap[i, j] == 1)
                    {
                        objectMap[i, j] = new Wall(j, i + 3);
                    }

                    else if (digitMap[i, j] == 2)
                    {
                        objectMap[i, j] = new Dot(j, i + 3);
                        dotsNumber++;
                    }
                    else if (digitMap[i, j] == 3)
                    {
                        objectMap[i, j] = new Energizer(j, i + 3);
                    }

                    else
                    {
                        objectMap[i, j] = new Tile(j, i + 3);
                    }
                }
            }

            //Load digits
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = (Bitmap)Properties.Resources.ResourceManager.GetObject( "_" + i);
            }

            /*
            nums[0] = Properties.Resources._0;
            nums[1] = Properties.Resources._1;
            nums[2] = Properties.Resources._2;
            nums[3] = Properties.Resources._3;
            nums[4] = Properties.Resources._4;
            nums[5] = Properties.Resources._5;
            nums[6] = Properties.Resources._6;
            nums[7] = Properties.Resources._7;
            nums[8] = Properties.Resources._8;
            nums[9] = Properties.Resources._9;
            */
        }
    }
}