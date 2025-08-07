using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RockPaperScissors.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GameCountInput.Text, out int rounds) && rounds > 0)
            {
                MessageBox.Show($"Starting game with {rounds} rounds!");
                // TODO: Transition to game screen or open a new window
            }
            else
            {
                MessageBox.Show("Please enter a valid number of rounds.");
            }
        }
    }
}