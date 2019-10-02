using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HampesYatzy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillAvailableList();
        }

        
        private void FillAvailableList()
        {
            lstAvailable.ItemsSource = null;
            lstAvailable.ItemsSource = GetFreePlayerList();
        }
        private List<Player> GetFreePlayerList()
        {
            List<Player> playersSort = DbOperations.GetFreePlayers().OrderByDescending(p => p.Nickname).ToList();
            playersSort.Reverse();
            return playersSort;
        }
        private void Btn_classic_Click(object sender, RoutedEventArgs e)
        {
                Play play = new Play(CreateNewGame(1), 1);
                play.Show();
                this.Close();        
        }
        private bool IsPlayersChosen()
        {
            if(lstChosen.Items.Count >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int CreateNewGame(int gameType)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < lstChosen.Items.Count; i++)
            {
                Player player = new Player();
                player = (Player)lstChosen.Items[i];
                players.Add(player);
            }
            return DbOperations.CreateGame(players, gameType);
        }

        private void Btn_steerd_Click(object sender, RoutedEventArgs e)
        {
            Play play = new Play(CreateNewGame(2), 2);
            play.Show();
            this.Close();
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
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

        private void Tutorial_MouseLeave(object sender, MouseEventArgs e)
        {
            Tutorial.Content = null;
            
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Tutorial.Content = new Tutorial();
        }

        private void LstAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillChosenList();
            SetPlayButtons();


        }
        private void SetPlayButtons() //justerar spela knapparna beroende på hur många valda spelare det är
        {
            if (IsPlayersChosen())
            {
                Btn_classic.IsEnabled = true;
                Btn_classic.Content = "Spela klassisk yatzy";

                Btn_steerd.IsEnabled = true;
                Btn_steerd.Content = "Spela styrd yatzy";
            }
            else
            {
                Btn_classic.IsEnabled = false;
                Btn_classic.Content = "Välj spelare först";

                Btn_steerd.IsEnabled = false;
                Btn_steerd.Content = "Välj spelare först";
            }
        }
        private void RemoveFromAvailableList() // tar bort valda spelare från spelarlistan
        {
            List<Player> players = GetFreePlayerList();
            List<Player> playerschosen = GetChosenList();
            List<Player> newPlayers = new List<Player>();
            for(int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < playerschosen.Count; j++)
                {
                    if (players[i].Id == playerschosen[j].Id)
                    {
                        players.RemoveAt(i);
                        i=0;
                    }
                }
            }
            lstAvailable.ItemsSource = null;
            lstAvailable.ItemsSource = players;

        }
        private List<Player> GetChosenList() //gör en lista av listview för valda spelare
        {
            List<Player> playerschosen = new List<Player>();
            for (int i = 0; i < lstChosen.Items.Count; i++)
            {
                playerschosen.Add((Player)lstChosen.Items[i]);
            }
            return playerschosen;
        }
        private void FillChosenList() //stoppar valda spelaren i listview
        {
            int count = 0;
            List<Player> playerschosen = GetChosenList();
            if(lstChosen.Items.Count < 4)
            {
                if(lstAvailable.SelectedItem != null)
                {
                    for(int i = 0; i < lstChosen.Items.Count; i++)
                    {
                        if (!lstAvailable.SelectedItem.Equals(playerschosen[i]))
                        {
                            count++;
                        }
                    }
                    if (count == lstChosen.Items.Count)
                    {
                        playerschosen.Add((Player)lstAvailable.SelectedItem);
                        lstChosen.ItemsSource = null;
                        lstChosen.ItemsSource = playerschosen;
                        RemoveFromAvailableList();
                    }
                }
            }
        }
        private void ResetChosenPlayers() // rensar listan av valda spelare
        {
            FillAvailableList();
            lstChosen.ItemsSource = null;
        }

        private void Btn_clear_chosen_Click(object sender, RoutedEventArgs e)
        {
            ResetChosenPlayers();
            SetPlayButtons();
        }
    }
}