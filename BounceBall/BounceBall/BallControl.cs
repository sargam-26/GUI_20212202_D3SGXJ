namespace BounceBall
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using BounceBall.Data;
    using BounceBall.Logic;
    using BounceBall.Renderer;

    /// <summary>
    /// This class inherits from FrameworkElement and control the funcationly of the game.
    /// </summary>
    public class BallControl : FrameworkElement
    {
        private BallModel model;
        private ScoreLogic scoreLogic;
        private BallLogic logic;
        private BallRenderer renderer;
        private Stopwatch stopwatch;
        private Window window;
        private DispatcherTimer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BallControl"/> class.
        /// </summary>
        public BallControl()
        {
            this.Loaded += this.BallControl_Loaded;
        }

        /// <summary>
        /// This method renders the objects and images on the screen.
        /// </summary>
        /// <param name="drawingContext">DrawingContext.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (drawingContext == null)
            {
                throw new ArgumentNullException(nameof(drawingContext));
            }

            if (this.renderer != null)
            {
                drawingContext.DrawDrawing(this.renderer.BuildDrawing());
            }
        }

        private void BallControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.stopwatch = new Stopwatch();
            this.model = new BallModel(this.ActualWidth, this.ActualHeight);
            this.logic = new BallLogic(this.model, "BounceBall.Logic.levels.level2.lvl");
            this.renderer = new BallRenderer(this.model);
            this.window = Window.GetWindow(this);
            this.scoreLogic = new ScoreLogic();
            if (this.window != null)
            {
                this.timer = new DispatcherTimer();
                this.timer.Interval = TimeSpan.FromMilliseconds(40);
                this.timer.Tick += this.TickTimer_Tick;
                this.timer.Start();
                this.window.KeyDown += this.Window_KeyDown;
            }

            this.logic.RefreshScreen += (obj, args) => this.InvalidateVisual();
            this.InvalidateVisual();
            this.stopwatch.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool finished = false;
            switch (e.Key)
            {
                case Key.A:
                    finished = this.logic.Move(-1, 0);
                    break;
                case Key.D:
                    finished = this.logic.Move(1, 0);
                    break;
                case Key.Space:
                    this.logic.IsFalling = false;
                    this.logic.Jump();
                    break;
                case Key.Escape:
                    this.timer.IsEnabled = !this.timer.IsEnabled;
                    MessageBox.Show("Click Ok to resume the game", "Paused", MessageBoxButton.OK);
                    BallLogic.GamePaused(this.timer);
                    break;
            }

            this.logic.ExtraLive(this.model.Ball.X, this.model.Ball.Y);
            this.logic.RingFound(this.model.Ball.X, this.model.Ball.Y);
            this.logic.ReachedCheckpoint(this.model.Ball.X, this.model.Ball.Y);
            if (this.logic.BarFound(this.model.Ball.X, this.model.Ball.Y)
                || this.logic.SpiderFound(this.model.Ball.X, this.model.Ball.Y))
            {
                this.logic.MoveToCheckpoint();
            }

            this.InvalidateVisual();
            TimeSpan time = new TimeSpan(0, 3, 0);
            if (this.logic.IsGameOver())
            {
                this.stopwatch.Stop();
                MessageBox.Show("You have used all your Lives", "Game Over", MessageBoxButton.OK);
                this.window.Close();
            }

            if (finished)
            {
                this.stopwatch.Stop();
                BallLogic.GamePaused(this.timer);
                int points = this.logic.CalculateScore(this.stopwatch.Elapsed, time);
                MessageBox.Show($"{points}", "Score", MessageBoxButton.OK);
                if (this.scoreLogic.IsThisAHighScore(points))
                {
                    var dialog = new ScoreDialog();
                    dialog.ShowDialog();
                    if (dialog.DialogResult == true)
                    {
                        string name = dialog.NameText;
                        this.scoreLogic.SaveScore(points, name);
                    }
                }

                this.window.Close();
            }
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            if (this.logic.IsFalling)
            {
                this.logic.Move(0, 1);
            }

            this.logic.MoveSpider();
            this.InvalidateVisual();
        }

        /*private void GamePaused()
        {
            var m = MessageBox.Show("Click Ok to resume the game", "Paused", MessageBoxButton.OK);
            this.timer.IsEnabled = !this.timer.IsEnabled;
        }*/
    }
}
