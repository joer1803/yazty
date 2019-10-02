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
            bestFrequentPlayers.ItemsSource = GetMostGamesRankedList();
        }

        private void LoadConsecutiveWinsRanking()
        {
            bestConsecutiveWins.ItemsSource = null;
            bestConsecutiveWins.ItemsSource = CalculateConsecutiveWins();
        }

        private void LoadTotalScoreRankings()
        {
            bestWeek.ItemsSource = null;
            bestWeek.ItemsSource = GetTotalScoreRankedList();
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
        private List<Player> GetMostGamesRankedList() // rankar spelare efter spelade matcher
        {
            int rank = 0;
            List<Player> players = DbOperations.GetMostGamesPlayer().OrderByDescending(p => p.Stats.GamesPlayed).ToList();
            List<Player> topfive = new List<Player>();
            for(int i = 0; i < players.Count; i++)
            {
                if(i>0 && players[i].Stats.GamesPlayed == players[i - 1].Stats.GamesPlayed)
                {
                    players[i].Stats.GamesPlayedRank = players[i - 1].Stats.GamesPlayedRank;

                }
                else
                {
                    rank++;
                    if (rank == 6)
                    {
                        return topfive;
                    }
                    players[i].Stats.GamesPlayedRank = rank;
                }
                topfive.Add(players[i]);
            }
            return topfive;
        }
        private List<Player> GetTotalScoreRankedList() // rankar spelare efter totalscore
        {
            int rank = 0;
            List<Player> players = DbOperations.GetTotalScoresPlayer().OrderByDescending(p => p.Stats.TotalScore).ToList();
            List<Player> topfive = new List<Player>();
            for (int i = 0; i < players.Count; i++)
            {
                
                if (i > 0 && players[i].Stats.TotalScore == players[i - 1].Stats.TotalScore)
                {
                    players[i].Stats.TotalScoreRank = players[i - 1].Stats.TotalScoreRank;

                }
                else
                {
                    rank++;
                    if (rank == 6)
                    {
                        return topfive;
                    }
                    players[i].Stats.TotalScoreRank = rank;
                }
                topfive.Add(players[i]);
            }
            return topfive;
        }
        private List<Player> CalculateConsecutiveWins() // rankar spelare efter vinster i rad
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
            List<Player> topfive = new List<Player>();
            for (int i = 0; i < WinningplayersSorted.Count; i++)
            {
                if (i - 1 >= 0 && WinningplayersSorted[i - 1].Stats.ConsecutiveWins == WinningplayersSorted[i].Stats.ConsecutiveWins)
                {
                    WinningplayersSorted[i].Stats.ConsecutiveWinsRank = rank;
                }
                else if (rank == 5)
                {
                    return topfive;
                }
                else
                {
                    rank++;
                    WinningplayersSorted[i].Stats.ConsecutiveWinsRank = rank;
                }
                topfive.Add(WinningplayersSorted[i]);
            }
            return topfive;
        }
    }
}