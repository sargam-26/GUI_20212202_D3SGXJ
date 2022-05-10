namespace BounceBall.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using BounceBall.Data;

    /// <summary>
    /// This class renders the images and implements IGameRenderer.
    /// </summary>
    public class BallRenderer : IGameRenderer
    {
        private BallModel Model;
        private Drawing Background;
        private Drawing Walls;
        private Drawing Exit;
        private Drawing RedBar;
        private Drawing Player;
        private Drawing Lifes;
        private Drawing Ring;
        private Drawing Spiders;
        private Point PlayerPosition;
        private Drawing Checkpoint;
        private Dictionary<string, Brush> Brushes = new Dictionary<string, Brush>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BallRenderer"/> class.
        /// </summary>
        /// <param name="model">BallModel.</param>
        public BallRenderer(BallModel model)
        {
            this.Model = model;
        }

        private Brush PlayerBrush
        {
            get
            {
                var q = this.GetBrush("BounceBall.Renderer.Images.ball.bmp", false);
                return q;
            }
        }

        private Brush ExitBrush
        {
            get
            {
                var q = this.GetBrush("BounceBall.Renderer.Images.exit.png", false);
                return q;
            }
        }

        private Brush SpiderBrush
        {
            get
            {
                return this.GetBrush("BounceBall.Renderer.Images.spider.png", true);
            }
        }

        private Brush WallBrush
        {
            get
            {
                var q = this.GetBrush("BounceBall.Renderer.Images.wall.png", true);
                return q;
            }
        }

        private Brush RingBrush
        {
            get
            {
                return this.GetBrush("BounceBall.Renderer.Images.ring.bmp", true);
            }
        }

        private Brush RedBarBrush
        {
            get
            {
                return this.GetBrush("BounceBall.Renderer.Images.bar.png", true);
            }
        }

        private Brush LifesBrush
        {
            get
            {
                var q = this.GetBrush("BounceBall.Renderer.Images.live.png", true);
                return q;
            }
        }

        private Brush CheckpointBrush
        {
            get
            {
                return this.GetBrush("BounceBall.Renderer.Images.checkpoint.png", true);
            }
        }

        /// <inheritdoc/>
        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetBackground());
            dg.Children.Add(this.GetExit());
            dg.Children.Add(this.GetWalls());
            dg.Children.Add(this.GetPlayer());
            dg.Children.Add(this.GetRing());
            dg.Children.Add(this.GetRedBar());
            dg.Children.Add(this.GetCheckpoint());
            dg.Children.Add(this.GetSpiders());
            dg.Children.Add(this.GetScore());
            dg.Children.Add(this.GetLives());
            dg.Children.Add(this.GetLifes());
            return dg;
        }

        /// <inheritdoc/>
        public Drawing GetBackground()
        {
            if (this.Background == null)
            {
                Geometry geometry = new RectangleGeometry(new Rect(0, 0, this.Model.GameWidth, this.Model.GameHeight));
                this.Background = new GeometryDrawing(System.Windows.Media.Brushes.Black, null, geometry);
            }

            return this.Background;
        }

        /// <inheritdoc/>
        public Brush GetBrush(string fname, bool isTilled)
        {
            if (!this.Brushes.ContainsKey(fname))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);
                image.EndInit();

                ImageBrush ib = new ImageBrush(image);
                if (isTilled)
                {
                    ib.TileMode = TileMode.Tile;
                    ib.Viewport = new Rect(0, 0, this.Model.TileSize, this.Model.TileSize);
                    ib.ViewportUnits = BrushMappingMode.Absolute;
                }

                this.Brushes.Add(fname, ib);
            }

            return this.Brushes[fname];
        }

        /// <inheritdoc/>
        public Drawing GetCheckpoint()
        {
            if (this.Checkpoint == null)
            {
                GeometryGroup g = new GeometryGroup();
                for (int x = 0; x < this.Model.Checkpoint.GetLength(0); x++)
                {
                    for (int y = 0; y < this.Model.Checkpoint.GetLength(1); y++)
                    {
                        if (this.Model.Checkpoint[x, y])
                        {
                            Geometry box = new RectangleGeometry(new Rect(x * this.Model.TileSize, y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                            g.Children.Add(box);
                        }
                    }
                }

                this.Checkpoint = new GeometryDrawing(this.CheckpointBrush, null, g);
            }

            return this.Checkpoint;
        }

        /// <inheritdoc/>
        public Drawing GetExit()
        {
            if (this.Exit == null)
            {
                Geometry geometry = new RectangleGeometry(new Rect(this.Model.Exit.X * this.Model.TileSize, this.Model.Exit.Y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                this.Exit = new GeometryDrawing(this.ExitBrush, null, geometry);
            }

            return this.Exit;
        }

        /// <inheritdoc/>
        public Drawing GetPlayer()
        {
            if (this.Player == null || this.PlayerPosition != this.Model.Ball)
            {
                Geometry geometry = new RectangleGeometry(new Rect(this.Model.Ball.X * this.Model.TileSize, this.Model.Ball.Y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                this.Player = new GeometryDrawing(this.PlayerBrush, null, geometry);
                this.PlayerPosition = this.Model.Ball;
            }

            return this.Player;
        }

        /// <inheritdoc/>
        public Drawing GetWalls()
        {
            if (this.Walls == null)
            {
                GeometryGroup g = new GeometryGroup();
                for (int x = 0; x < this.Model.Walls.GetLength(0); x++)
                {
                    for (int y = 0; y < this.Model.Walls.GetLength(1); y++)
                    {
                        if (this.Model.Walls[x, y])
                        {
                            Geometry box = new RectangleGeometry(new Rect(x * this.Model.TileSize, y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                            g.Children.Add(box);
                        }
                    }
                }

                this.Walls = new GeometryDrawing(this.WallBrush, null, g);
            }

            return this.Walls;
        }

        /// <inheritdoc/>
        public Drawing GetSpiders()
        {
            GeometryGroup g = new GeometryGroup();
            for (int x = 0; x < this.Model.Spiders.Length; x++)
            {
                Geometry box = new RectangleGeometry(new Rect(this.Model.Spiders[x].Position.X * this.Model.TileSize, this.Model.Spiders[x].Position.Y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                g.Children.Add(box);
            }

            this.Spiders = new GeometryDrawing(this.SpiderBrush, null, g);
            return this.Spiders;
        }

        /// <inheritdoc/>
        public Drawing GetRedBar()
        {
            if (this.RedBar == null)
            {
                GeometryGroup g = new GeometryGroup();
                for (int x = 0; x < this.Model.Bars.GetLength(0); x++)
                {
                    for (int y = 0; y < this.Model.Bars.GetLength(1); y++)
                    {
                        if (this.Model.Bars[x, y])
                        {
                            Geometry box = new RectangleGeometry(new Rect(x * this.Model.TileSize, y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                            g.Children.Add(box);
                        }
                    }
                }

                this.RedBar = new GeometryDrawing(this.RedBarBrush, null, g);
            }

            return this.RedBar;
        }

        /// <inheritdoc/>
        public Drawing GetRing()
        {
            GeometryGroup g = new GeometryGroup();
            for (int x = 0; x < this.Model.Ring.GetLength(0); x++)
            {
                for (int y = 0; y < this.Model.Ring.GetLength(1); y++)
                {
                    if (this.Model.Ring[x, y])
                    {
                        Geometry box = new RectangleGeometry(new Rect(x * this.Model.TileSize, y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                        g.Children.Add(box);
                    }
                }
            }

            this.Ring = new GeometryDrawing(this.RingBrush, null, g);
            return this.Ring;
        }

        /// <inheritdoc/>
        public Drawing GetScore()
        {
            FormattedText formattedText = new FormattedText(
                this.Model.Score.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                16,
                System.Windows.Media.Brushes.Black,
                1);

            GeometryDrawing text = new GeometryDrawing(
                null,
                new Pen(System.Windows.Media.Brushes.Yellow, 2),
                formattedText.BuildGeometry(new Point(35, 5)));

            return text;
        }

        /// <inheritdoc/>
        public Drawing GetLives()
        {
            FormattedText formattedText = new FormattedText(
                this.Model.Lives.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                16,
                System.Windows.Media.Brushes.Black,
                1);

            GeometryDrawing text = new GeometryDrawing(
                null,
                new Pen(System.Windows.Media.Brushes.Yellow, 2),
                formattedText.BuildGeometry(new Point(350, 5)));

            return text;
        }

        /// <inheritdoc/>
        public Drawing GetLifes()
        {
            GeometryGroup g = new GeometryGroup();
            for (int x = 0; x < this.Model.Lifes.GetLength(0); x++)
            {
                for (int y = 0; y < this.Model.Lifes.GetLength(1); y++)
                {
                    if (this.Model.Lifes[x, y])
                    {
                        Geometry box = new RectangleGeometry(new Rect(x * this.Model.TileSize, y * this.Model.TileSize, this.Model.TileSize, this.Model.TileSize));
                        g.Children.Add(box);
                    }
                }
            }

            this.Lifes = new GeometryDrawing(this.LifesBrush, null, g);

            return this.Lifes;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            this.Background = null;
            this.Walls = null;
            this.Exit = null;
            this.Player = null;
            this.Ring = null;
            this.Checkpoint = null;
            this.RedBar = null;
            this.Spiders = null;
            this.PlayerPosition = new Point(-1, -1);
            this.Brushes.Clear();
        }
    }
}
