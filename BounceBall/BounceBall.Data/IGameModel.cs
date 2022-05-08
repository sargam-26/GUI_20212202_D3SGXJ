namespace BounceBall.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Interface for ball methods.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Moving the ball up.
        /// </summary>
        /// <param name="item">Point.</param>
        /// <param name="dx">change in position.</param>
        public void MoveY(Point item, int dx);
    }
}
