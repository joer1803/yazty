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

            //DiceTest();
            GetFreePlayerList();
            //GetBusyList();
        }

        
        private void GetFreePlayerList()
        {
            lstAvailable.ItemsSource = null;
            lstAvailable.ItemsSource = DbOperations.GetFreePlayers();
        }
        //private void GetBusyList()
        //{
        //    lstBusy.ItemsSource = null;
        //    lstBusy.ItemsSource = DbOperations.GetInGamePlayers();
        //}


        private void Btn_classic_Click(object sender, RoutedEventArgs e)
        {
            Play play = new Play(CreateNewGame(1));
            play.Show();
            this.Close();
        }
        private int CreateNewGame(int gameType)
        {
            List<Player> players = new List<Player>();
            players.Add((Player)lstAvailable.SelectedItems);
            return DbOperations.CreateGame(players, gameType);
        }
        private void Btn_steerd_Click(object sender, RoutedEventArgs e)
        {
            Play play = new Play(CreateNewGame(2));
            play.Show();
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

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

        /*private void DiceTest()
{
   int nr = 1;
   BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + nr.ToString() + ".png", UriKind.Relative)); //skapar en bitmappsbild av en tärning
   dice1.Source = image; //gör ovanstående bild till source till en tom image i gränssnittet
   dice2.Source = image;
   dice3.Source = image;
   dice4.Source = image;
   dice5.Source = image;
}*/

    }

    
}
