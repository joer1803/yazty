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
            GetFreePlayerList();
        }

        

        private void GetFreePlayerList()
        {
            lstAvailable.ItemsSource = null;
            lstAvailable.ItemsSource = DbOperations.GetFreePlayers();
        }
        private void Btn_classic_Click(object sender, RoutedEventArgs e)
        {
                Play play = new Play(CreateNewGame(1));
                play.Show();
                this.Close();        
        }
        private bool IsPlayersChosen()
        {
            if(lstAvailable.SelectedItems.Count >= 2 && lstAvailable.SelectedItems.Count <= 4)
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
            for (int i = 0; i < lstAvailable.SelectedItems.Count; i++)
            {
                Player player = new Player();
                player = (Player)lstAvailable.SelectedItems[i];
                players.Add(player);
            }
            return DbOperations.CreateGame(players, gameType);
        }

        private void Btn_steerd_Click(object sender, RoutedEventArgs e)
        {
            Play play = new Play(CreateNewGame(2));
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
            if (IsPlayersChosen())
            {
                Btn_classic.IsEnabled = true;
                Btn_steerd.IsEnabled = true;
            }
            else
            {
                Btn_classic.IsEnabled = false;
                Btn_steerd.IsEnabled = false;
            }
        }
    }
}