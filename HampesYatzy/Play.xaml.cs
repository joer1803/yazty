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

namespace HampesYatzy
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        GameLogic gameLogic;
        public Play(int gameId)
        {
            InitializeComponent();
            gameLogic = new GameLogic(gameId);
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Rating rating = new Rating();
            rating.Show();
            this.Close();

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.Show();
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Dice()
        {
            int nr = 1;
            BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + nr.ToString() + "png", UriKind.Relative));
            dice1.Source = image;
            dice2.Source = image;
            dice3.Source = image;
            dice4.Source = image;
            dice5.Source = image;
        }
    }
}
