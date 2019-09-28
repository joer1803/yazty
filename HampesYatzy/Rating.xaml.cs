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
            LoadConsecutiveWinsRanking();
        }

        private void LoadMostGamesRankings()
        {
            bestFrequentPlayers.ItemsSource = null;
            bestFrequentPlayers.ItemsSource = DbOperations.GetMostGamesPlayer();
        }

        private void LoadConsecutiveWinsRanking()
        {
            bestConsecutiveWins.ItemsSource = null;
            bestConsecutiveWins.ItemsSource = CalculateConsecutiveWins();
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

        private List<Player> CalculateConsecutiveWins()
        {
            List<YatzyGame> games = DbOperations.GetConsecutiveWinsRanking();
            List<YatzyGame> gamesSorted = games.OrderByDescending(g => g.EndTime).ToList();
            gamesSorted.Reverse();
            List<Player> Winningplayers = new List<Player>();
            int rank = 0;
            foreach (YatzyGame g in gamesSorted)
            {
                for (int i = 0; i < g.Players.Count; i++)
                {
                    if (g.Winner == null || g.Players[i].ScoreSheet.TotScore > g.Winner.ScoreSheet.TotScore)
                    {
                        g.Winner = g.Players[i];
                    }
                }
            }
            for (int i = 0; i < gamesSorted.Count; i++)
            {
                if (gamesSorted[i].Winner != null)
                {
                    gamesSorted[i].Winner.Stats = new PlayerStats();
                    for (int j = 0; j < gamesSorted.Count; j++)
                    {
                        if (gamesSorted[j].Winner != null)
                        {
                            if (gamesSorted[i].Winner.Id == gamesSorted[j].Winner.Id)
                            {
                                gamesSorted[i].Winner.Stats.ConsecutiveWins++;
                            }
                            else
                            {
                                for (int k = 0; k < gamesSorted[j].Players.Count; k++)
                                {
                                    if (gamesSorted[i].Winner.Id == gamesSorted[j].Players[k].Id)
                                    {
                                        gamesSorted[i].Winner.Stats.ConsecutiveWins = 0;
                                    }
                                }
                            }
                        }
                    }

                    if (gamesSorted[i].Winner.Stats.ConsecutiveWins > 0)
                    {
                        bool duplicate = false;
                        for (int q = 0; q < Winningplayers.Count; q++)
                        {
                            if (gamesSorted[i].Winner.Id == Winningplayers[q].Id)
                            {
                                duplicate = true;
                            }
                        }
                        if (duplicate == false)
                        {
                            Winningplayers.Add(gamesSorted[i].Winner);
                        }
                    }
                }
            }
            List<Player> WinningplayersSorted = Winningplayers.OrderByDescending(p => p.Stats.ConsecutiveWins).ToList();
            for (int i = 0; i < WinningplayersSorted.Count; i++)
            {
                if (i - 1 >= 0 && WinningplayersSorted[i - 1].Stats.ConsecutiveWins == WinningplayersSorted[i].Stats.ConsecutiveWins)
                {
                    WinningplayersSorted[i].Stats.ConsecutiveWinsRank = rank;
                }
                else
                {
                    rank++;
                    WinningplayersSorted[i].Stats.ConsecutiveWinsRank = rank;
                }
            }
            return WinningplayersSorted;
        }
    }
}