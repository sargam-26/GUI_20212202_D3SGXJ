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

    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml.
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuWindow"/> class.
        /// </summary>
        public MainMenuWindow()
        {
            this.InitializeComponent();
        }

        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow game = new MainWindow();
            game.Show();
        }

        private void ScoreBoard_Click(object sender, RoutedEventArgs e)
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.Show();
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
