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
    /// Interaction logic for ScoreDialog.xaml.
    /// </summary>
    public partial class ScoreDialog : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreDialog"/> class.
        /// </summary>
        public ScoreDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets NameTextBox Test.
        /// </summary>
        public string NameText
        {
            get { return this.NameTextBox.Text; }
            set { this.NameTextBox.Text = value; }
        }

        private void OKButton_Cick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
