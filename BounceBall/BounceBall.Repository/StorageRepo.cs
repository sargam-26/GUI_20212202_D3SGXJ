namespace BounceBall.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using BounceBall.Data;

    /// <summary>
    /// This class stores method related to storing score.
    /// </summary>
    public class StorageRepo : IStorageRepository
    {
        private XDocument doc;
        private IList<Score> scoreList;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageRepo"/> class.
        /// </summary>
        public StorageRepo()
        {
            this.doc = XDocument.Load("scores.xml");
            var q = from item in this.doc.Descendants("score")
                    select new Score(
                        name: item.Element("name").Value,
                        points: int.Parse(item.Attribute("points").Value));
            this.scoreList = q.ToList();
        }

        /// <summary>
        /// This method checks if the current score is a highScore.
        /// </summary>
        /// <param name="points">points.</param>
        /// <returns>Boolean.</returns>
        public bool CheckHighScore(int points)
        {
            var query = from item in this.scoreList
                        where item.Points < points
                        select item;
            return query.ToList().Count > 0;
        }

        /// <summary>
        /// This method the save the score in the data file.
        /// </summary>
        /// <param name="score">Score.</param>
        public void SaveScore(Score score)
        {
            if (score == null)
            {
                throw new ArgumentNullException(nameof(score));
            }

            for (int i = 0; i < this.scoreList.Count; i++)
            {
                if (this.scoreList.ToArray()[i].Points < score.Points)
                {
                    this.scoreList.Insert(i, score);
                    break;
                }
            }

            var highScores = this.scoreList.Take(3).ToArray();
            XDocument doc = new XDocument();
            doc.Add(new XElement("Scores"));
            for (int i = 0; i < highScores.Length; i++)
            {
                XElement node = new XElement(
                    "score",
                    new XAttribute("points", highScores[i].Points),
                    new XElement("name", highScores[i].Name));
                doc.Root.Add(node);
            }

            doc.Save("scores.xml");
        }

        /// <summary>
        /// This method returns the list of Scores in the data file.
        /// </summary>
        /// <returns>List of Score.</returns>
        public IList<Score> ScoreBoard()
        {
            return this.scoreList.Take(3).ToList();
        }
    }
}
