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
    
        //private void Dice()
        //{
        //    int nr = 1;
        //    BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + nr.ToString() + "png", UriKind.Relative));
        //    dice1.Source = image;
        //    dice2.Source = image;
        //    dice3.Source = image;
        //    dice4.Source = image;
        //    dice5.Source = image;
        //}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private List<Image> MakeDiceList()
        {
            List<Image> dices = new List<Image>();
            dices.Add(diceOne);
            dices.Add(diceTwo);
            dices.Add(diceThree);
            dices.Add(diceFour);
            dices.Add(diceFive);
            return dices;
        }
        private List<BitmapImage> MakeDiceImageList()
        {
            List<BitmapImage> diceImages = new List<BitmapImage>();
            for (int i = 1; i < 7; i++)
            {
                BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + i.ToString() + ".png", UriKind.Relative));
                diceImages.Add(image);
            }
            return diceImages;
        }
        private void UpdateDice()
        {
            List<BitmapImage> diceImages = MakeDiceImageList();
            List<Image> diceFrames = MakeDiceList();
            List<Die> dice = gameLogic.GetDice();

            for (int i = 0; i < diceFrames.Count;i++)
            {
                diceFrames[i].Source = diceImages[dice[i].Value-1];
            }
        }
    }
}
