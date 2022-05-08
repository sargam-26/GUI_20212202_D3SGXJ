namespace BounceBall.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BounceBall.Data;
    using BounceBall.Repository;

    /// <summary>
    /// This class implements method of IScoreLogic.
    /// </summary>
    public class ScoreLogic : IScoreLogic
    {
        public StorageRepo storageRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreLogic"/> class.
        /// </summary>
        public ScoreLogic()
        {
            this.storageRepository = new StorageRepo();
        }

        /// <inheritdoc/>
        public bool IsThisAHighScore(int points)
        {
            return this.storageRepository.CheckHighScore(points);
        }

        /// <inheritdoc/>
        public void SaveScore(int points, string name)
        {
            Score score = new Score(points, name);
            this.storageRepository.SaveScore(score);
        }

        /// <inheritdoc/>
        public IList<Score> GetScores()
        {
            return this.storageRepository.ScoreBoard();
        }
    }
}
