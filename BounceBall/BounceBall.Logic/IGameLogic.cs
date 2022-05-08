namespace BounceBall.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This interface have all the methods needed for the game.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// Move the ball as the parameter are given.
        /// </summary>
        /// <param name="dx">movement on x axis.</param>
        /// <param name="dy">movement o y axis.</param>
        /// <returns>Boolean.</returns>
        bool Move(int dx, int dy);

        /// <summary>
        /// This method calculate the score.
        /// </summary>
        /// <param name="timeSpan">Time by user.</param>
        /// <param name="maxTime">Max time defined.</param>
        /// <returns>returns the total score.</returns>
        int CalculateScore(TimeSpan timeSpan, TimeSpan maxTime);

        /// <summary>
        /// This method checks if the ring is there on current position.
        /// </summary>
        /// <param name="x">ball position on X axis.</param>
        /// <param name="y">ball position on y axis.</param>
        /// <returns>Bool value.</returns>
        public bool RingFound(double x, double y);

        /// <summary>
        /// This method checks if the Bar is there on current position.
        /// </summary>
        /// <param name="x">ball position on X axis.</param>
        /// <param name="y">ball position on y axis.</param>
        /// <returns>Bool value.</returns>
        public bool BarFound(double x, double y);

        /// <summary>
        /// This method checks if the spider is there on current position.
        /// </summary>
        /// <param name="x">ball position on X axis.</param>
        /// <param name="y">ball position on y axis.</param>
        /// <returns>Bool value.</returns>
        public bool SpiderFound(double x, double y);

        /// <summary>
        /// This method checks if the checkpoint is there on current position.
        /// </summary>
        /// <param name="x">ball position on X axis.</param>
        /// <param name="y">ball position on y axis.</param>
        /// <returns>Bool value.</returns>
        public bool ReachedCheckpoint(double x, double y);

        /// <summary>
        /// This method move the ball to checkpoint if it busts.
        /// </summary>
        public void MoveToCheckpoint();

        /// <summary>
        /// This method moves the spider in vertically direction.
        /// </summary>
        public void MoveSpider();

        /// <summary>
        /// This method returns whether game is over.
        /// </summary>
        /// <returns>Bool Value.</returns>
        public bool IsGameOver();

        /// <summary>
        /// This method is used to make the ball jump.
        /// </summary>
        public void Jump();

        /// <summary>
        /// This method adds one give one more life to player.
        /// </summary>
        /// <param name="x">x axis.</param>
        /// <param name="y">y axis.</param>
        /// <returns>Bool value.</returns>
        public bool ExtraLive(double x, double y);
    }
}
