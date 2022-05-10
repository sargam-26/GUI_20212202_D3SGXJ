namespace BounceBall
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using BounceBall.Data;
    using BounceBall.Logic;

    /// <summary>
    /// Interaction logic for ScoreBoard.xaml.
    /// </summary>
    public partial class ScoreBoard : Window
    {
        private ScoreLogic scoreLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreBoard"/> class.
        /// </summary>
        public ScoreBoard()
        {
            this.InitializeComponent();
            this.scoreLogic = new ScoreLogic();
            IList<Score> scores = this.scoreLogic.GetScores();
            foreach (var item in scores)
            {
                this.ScoreListBox.Items.Add(item.ToString());
            }
        }
    }
}
