namespace BounceBall.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// This class keep track of spider position and direction of moving.
    /// </summary>
    public class Spiders
    {
        /// <summary>
        /// Gets or sets position of spider.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether spider is moving up.
        /// </summary>
        public bool IsMovingUp { get; set; }
    }
}
