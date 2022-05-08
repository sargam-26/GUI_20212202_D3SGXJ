namespace BounceBall.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class stores the values for Score.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Score"/> class.
        /// </summary>
        /// <param name="points">GameScore.</param>
        /// <param name="name">Player Name.</param>
        public Score(int points, string name)
        {
            this.Points = points;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets game score.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets Player Name.
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Name}\t{this.Points}";
        }
    }
}
