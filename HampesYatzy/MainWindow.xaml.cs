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

        //private void Image_MousesEnter(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    Tutorial.Content = new Tutorial();
        //}

        private void Tutorial_MouseLeave(object sender, MouseEventArgs e)
        {
            Tutorial.Content = null;
            
            //felkod. Stänger hela fönstrert. Vill endast manövrera tillbaka till mainwindow.
           
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Tutorial.Content = new Tutorial();
        }
    }
}