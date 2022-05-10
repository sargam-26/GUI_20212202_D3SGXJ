namespace BounceBall.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// This interface have the functions which will help the game to renderer.
    /// </summary>
    public interface IGameRenderer
    {
        /// <summary>
        /// this method reset the screen.
        /// </summary>
        public void Reset();

        /// <summary>
        /// This method get brush from the images.
        /// </summary>
        /// <param name="fname">File name.</param>
        /// <param name="isTilled">Bool.</param>
        /// <returns>Brush.</returns>
        Brush GetBrush(string fname, bool isTilled);

        /// <summary>
        /// This method build the drawings upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing BuildDrawing();

        /// <summary>
        /// This method draw the Exit upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetExit();

        /// <summary>
        /// This method draw the Rings upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetRing();

        /// <summary>
        /// This method draw the Red Bar upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetRedBar();

        /// <summary>
        /// This method draw the checkpoint upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetCheckpoint();

        /// <summary>
        /// This method draw the Player upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetPlayer();

        /// <summary>
        /// This method draw the walls upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetWalls();

        /// <summary>
        /// This method draw the background upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetBackground();

        /// <summary>
        /// This method draw the spider upon rendering.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetSpiders();

        /// <summary>
        /// This method draw the lives remaing to screen.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetLives();

        /// <summary>
        /// This method draw the Score to the screen.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetScore();

        /// <summary>
        /// This method draw the life icon to console.
        /// </summary>
        /// <returns>Drawing.</returns>
        public Drawing GetLifes();
    }
}
