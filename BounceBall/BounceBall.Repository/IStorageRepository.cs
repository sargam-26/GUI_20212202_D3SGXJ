namespace BounceBall.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BounceBall.Data;

    /// <summary>
    /// Interface for storage repo.
    /// </summary>
    public interface IStorageRepository
    {
        /// <summary>
        /// This method returns the list of Scores in the data file.
        /// </summary>
        /// <returns>List of Score.</returns>
        IList<Score> ScoreBoard();

        /// <summary>
        /// This method the save the score in the data file.
        /// </summary>
        /// <param name="score">Score.</param>
        void SaveScore(Score score);

        /// <summary>
        /// This method checks if the current score is a highScore.
        /// </summary>
        /// <param name="points">points.</param>
        /// <returns>Boolean.</returns>
        bool CheckHighScore(int points);
    }
}
