namespace BounceBall.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;
    using BounceBall.Data;

    /// <summary>
    /// This class implements the method of gameLogic interface.
    /// </summary>
    public class BallLogic : IGameLogic
    {
        /// <summary>
        /// This keeps track of whether ball is falling or not.
        /// </summary>
        public bool IsFalling;

        /// <summary>
        /// This keeps track of whether ball is in jumping state.
        /// </summary>
        public bool IsJumping;

        private BallModel model;
        private Point start;
        private bool isGameOver;
        private int counter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BallLogic"/> class.
        /// </summary>
        /// <param name="model">Ball Model.</param>
        /// <param name="fname">String file.</param>
        public BallLogic(BallModel model, string fname)
        {
            this.IsFalling = true;
            this.model = model;
            this.IsJumping = false;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(fname));
            }

            StreamReader sr = new StreamReader(stream);

            string[] lines = sr.ReadToEnd().Replace("\r", "").Split("\n");

            int width = int.Parse(lines[0]);
            int height = int.Parse(lines[1]);
            this.counter = 0;
            this.isGameOver = false;

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.Walls = new bool[width, height];
            model.Ring = new bool[width, height];
            model.Bars = new bool[width, height];
            model.Lifes = new bool[width, height];
            model.Checkpoint = new bool[width, height];
            model.Lifes = new bool[width, height];
            model.Spiders = new Spiders[3];
            model.Score = 0;
            model.TileSize = Math.Min(model.GameWidth / width, model.GameHeight / height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    char current = lines[y + 2][x];
                    if (current == 'e')
                    {
                        model.Walls[x, y] = true;
                    }

                    if (current == 'S')
                    {
                        this.start = new Point(x, y);
                        model.Ball = this.start;
                    }

                    if (current == 'F')
                    {
                        model.Exit = new Point(x, y);
                    }

                    if (current == 'r')
                    {
                        model.Ring[x, y] = true;
                    }

                    if (current == 'b')
                    {
                        model.Bars[x, y] = true;
                    }

                    if (current == 'c')
                    {
                        model.Checkpoint[x, y] = true;
                    }

                    if (current == 's')
                    {
                        model.Spiders[this.counter] = new Spiders() { Position = new Point(x, y), IsMovingUp = true };
                        this.counter++;
                    }

                    if (current == 'L')
                    {
                        model.Lifes[x, y] = true;
                    }
                }
            }
        }

        /// <summary>
        /// This event invoke the method to refresh screen.
        /// </summary>
        public event EventHandler RefreshScreen;

        /// <summary>
        /// This method will pause the game.
        /// </summary>
        /// <param name="timer">Dispatcher timer.</param>
        public static void GamePaused(DispatcherTimer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            timer.IsEnabled = !timer.IsEnabled;
        }

        /// <inheritdoc/>
        public bool Move(int dx, int dy)
        {
            int newX = (int)(this.model.Ball.X + dx);
            int newY = (int)(this.model.Ball.Y + dy);

            if (newX >= 0 && newY >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) &&
                !this.model.Walls[newX, newY])
            {
                this.model.Ball = new Point(newX, newY);
            }

            return this.model.Ball.Equals(this.model.Exit);
        }

        /// <inheritdoc/>
        public int CalculateScore(TimeSpan timeSpan, TimeSpan maxTime)
        {
            int score = 50;
            TimeSpan extra = maxTime - timeSpan;
            if (extra.TotalSeconds > 0)
            {
                score += (int)(extra.TotalSeconds * .5);
            }

            score += this.model.Score;
            return score;
        }

        /// <inheritdoc/>
        public bool RingFound(double x, double y)
        {
            if (this.model.Ring[(int)x, (int)y])
            {
                this.model.Ring[(int)x, (int)y] = false;
                this.model.Score += 200;
                return true;
            }

            this.RefreshScreen.Invoke(this, EventArgs.Empty);
            return false;
        }

        /// <inheritdoc/>
        public bool BarFound(double x, double y)
        {
            if (this.model.Bars[(int)x, (int)y])
            {
                this.model.Lives -= 1;
                if (this.model.Lives <= 0)
                {
                    this.isGameOver = true;
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool SpiderFound(double x, double y)
        {
            foreach (Spiders spider in this.model.Spiders)
            {
                if (spider.Position == new Point(x, y))
                {
                    this.model.Lives -= 1;
                    if (this.model.Lives <= 0)
                    {
                        this.isGameOver = true;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public bool ReachedCheckpoint(double x, double y)
        {
            if (this.model.Checkpoint[(int)x, (int)y])
            {
                this.start = new Point(x, y);
                this.model.Checkpoint[(int)x, (int)y] = false;
                this.model.Score += 50;
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void MoveToCheckpoint()
        {
            this.model.Ball = this.start;
        }

        /// <inheritdoc/>
        public void MoveSpider()
        {
            int newX, newY;
            for (int i = 0; i < this.model.Spiders.Length; i++)
            {
                Point oneSpider = this.model.Spiders[i].Position;
                if (this.model.Spiders[i].IsMovingUp)
                {
                    newX = (int)(oneSpider.X + 0);
                    newY = (int)(oneSpider.Y - 1);
                }
                else
                {
                    newX = (int)(oneSpider.X + 0);
                    newY = (int)(oneSpider.Y + 1);
                }

                if (newX >= 0 && newY >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) &&
                    !this.model.Walls[newX, newY])
                {
                    this.model.Spiders[i].Position = new Point(newX, newY);
                }
                else
                {
                    this.model.Spiders[i].IsMovingUp = !this.model.Spiders[i].IsMovingUp;
                }

                this.RefreshScreen.Invoke(this, EventArgs.Empty);
            }
        }

        /// <inheritdoc/>
        public bool IsGameOver()
        {
            return this.isGameOver;
        }

        /// <inheritdoc/>
        public void Jump()
        {
            this.IsFalling = false;
            this.IsJumping = true;
            for (int i = 0; i < 3; i++)
            {
                int newX = (int)(this.model.Ball.X + 0);
                int newY = (int)(this.model.Ball.Y - 1);
                if (newX >= 0 && newY >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) &&
                    !this.model.Walls[newX, newY])
                {
                    this.model.Ball = new Point(newX, newY);
                }
            }

            this.IsFalling = true;
            this.IsJumping = false;
        }

        /// <inheritdoc/>
        public bool ExtraLive(double x, double y)
        {
            if (this.model.Lifes[(int)x, (int)y])
            {
                this.model.Lifes[(int)x, (int)y] = false;
                this.model.Lives += 1;
                if (this.model.Lives > 5)
                {
                    this.model.Lives = 5;
                }

                this.model.Score += 50;
                this.RefreshScreen.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }
    }
}
