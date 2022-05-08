namespace BounceBall.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// This class have the elements and methods needed for the game.
    /// </summary>
    public class BallModel : IGameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BallModel"/> class.
        /// </summary>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        public BallModel(double w, double h)
        {
            this.GameWidth = w;
            this.GameHeight = h;
            this.Lives = 3;
        }

        /// <summary>
        /// Gets or Sets Lives.
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// Gets or Sets Walls.
        /// </summary>
        public bool[,] Walls { get; set; }

        /// <summary>
        /// Gets or Sets Ball.
        /// </summary>
        public Point Ball { get; set; }

        /// <summary>
        /// Gets or sets lifes.
        /// </summary>
        public bool[,] Lifes { get; set; }

        /// <summary>
        /// Gets or Sets Exit.
        /// </summary>
        public Point Exit { get; set; }

        /// <summary>
        /// Gets or Sets RIng.
        /// </summary>
        public bool[,] Ring { get; set; }

        /// <summary>
        /// Gets or Sets Bars.
        /// </summary>
        public bool[,] Bars { get; set; }

        /// <summary>
        /// Gets or Sets Checkpoint.
        /// </summary>
        public bool[,] Checkpoint { get; set; }

        /// <summary>
        /// Gets or Sets Spiders.
        /// </summary>
        public Spiders[] Spiders { get; set; }

        /// <summary>
        /// Gets or Sets GameWidth.
        /// </summary>
        public double GameWidth { get; set; }

        /// <summary>
        /// Gets or Sets GameHeight.
        /// </summary>
        public double GameHeight { get; set; }

        /// <summary>
        /// Gets or Sets Lives.
        /// </summary>
        public double TileSize { get; set; }

        /// <summary>
        /// Gets or Sets Lives.
        /// </summary>
        public int Score { get; set; }

        /// <inheritdoc/>
        public void MoveY(Point item, int dx)
        {
            item = new Point(item.X, item.Y + dx);
        }
    }
}
