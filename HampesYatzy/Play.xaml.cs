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
            UpdatePlayer();
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

        private List<Image> MakeImageList()
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
            List<Image> diceFrames = MakeImageList();
            List<Die> dice = gameLogic.GetDice();

            for (int i = 0; i < diceFrames.Count; i++)
            {
                diceFrames[i].Source = diceImages[dice[i].Value - 1];
            }
        }


        private void UpdatePlayer()
        {
            lblActivePlayer.Content = gameLogic.GetActivePlayer().Nickname;
        }
        private string CheckButton(int index)
        {
            if(gameLogic.GetDice()[index].Hold == true)
            {
                gameLogic.GetDice()[index].Hold = false;
                return "Spara";
            }
            else
            {
                gameLogic.GetDice()[index].Hold = true;
                return "Släpp";

            }
        }

        private void Hold_diceOne_Click(object sender, RoutedEventArgs e)
        {
            hold_diceOne.Content = CheckButton(0);
        }

        private void Hold_diceTwo_Click(object sender, RoutedEventArgs e)
        {
            hold_diceTwo.Content = CheckButton(1);
        }

        private void Hold_diceThree_Click(object sender, RoutedEventArgs e)
        {
            hold_diceThree.Content = CheckButton(2);
        }

        private void Hold_diceFour_Click(object sender, RoutedEventArgs e)
        {
            hold_diceFour.Content = CheckButton(3);
        }

        private void Hold_diceFive_Click(object sender, RoutedEventArgs e)
        {
            hold_diceFive.Content = CheckButton(4);
        }

        private void Trow_dice_Click(object sender, RoutedEventArgs e)
        {
            gameLogic.RollDice();
            UpdateDice();
        }

        private void Btn_select_ones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_twos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_threes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_fours_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_fives_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_sixes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_pair_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_twoPair_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_threeOfAKind_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_fourOfAKind_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_smallStraight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_BigStraight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_FullHouse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_Chance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_select_Yatzy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void end_game_Click(object sender, RoutedEventArgs e)
        {
            //DbOperations.DeleteGame();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}