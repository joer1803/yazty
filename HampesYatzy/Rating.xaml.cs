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
    /// Interaction logic for Rating.xaml
    /// </summary>
    public partial class Rating : Window
    {
        public Rating()
        {
            InitializeComponent();
            LoadMostGamesRankings();
            LoadTotalScoreRankings();
        }


        private void LoadMostGamesRankings()
        {
            bestFrequentPlayers.ItemsSource = null;
            bestFrequentPlayers.ItemsSource = DbOperations.GetMostGamesPlayer();
        }
        private void LoadTotalScoreRankings()
        {
            bestWeek.ItemsSource = null;
            bestWeek.ItemsSource = DbOperations.GetTotalScoresPlayer();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
