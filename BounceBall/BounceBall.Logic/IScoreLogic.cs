namespace BounceBall.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BounceBall.Data;

    /// <summary>
    /// This interface have methods related to saving the scores and accessing them.
    /// </summary>
    public interface IScoreLogic
    {
        /// <summary>
        /// This method checks if the current score is top 3 highscore.
        /// </summary>
        /// <param name="points">points.</param>
        /// <returns>bool.</returns>
        public bool IsThisAHighScore(int points);

        /// <summary>
        /// This method save the scores in the data file.
        /// </summary>
        /// <param name="points">points.</param>
        /// <param name="name">Name.</param>
        public void SaveScore(int points, string name);

        /// <summary>
        /// This method gives the list of score in the datafile.
        /// </summary>
        /// <returns>IList.</returns>
        public IList<Score> GetScores();
    }
}