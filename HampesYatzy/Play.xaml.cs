﻿using System;
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
using System.Windows.Threading;

namespace HampesYatzy
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        GameLogic gameLogic;
        DispatcherTimer _timer;
        TimeSpan _time;

        public Play(int gameId)
        {
            InitializeComponent();
            gameLogic = new GameLogic(gameId);
            UpdatePlayer();
            CountTime();
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
            if (gameLogic.GetDice()[index].Hold == true)
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

        //private bool buttonOnesWasClicked = false;
        //private bool buttonTwosWasClicked = false;
        //private bool buttonThreesWasClicked = false;
        //private bool buttonFoursWasClicked = false;
        //private bool buttonFivesWasClicked = false;
        //private bool buttonSixesWasClicked = false;
        //private bool buttonOnePairWasClicked = false;
        //private bool buttonTwoPairWasClicked = false;
        //private bool buttonThreeOfAKindWasClicked = false;
        //private bool buttonFourOfAKindWasClicked = false;
        //private bool buttonSmallStraightWasClicked = false;
        //private bool buttonBigStraightWasClicked = false;
        //private bool buttonFullHouseWasClicked = false;
        //private bool buttonChanceWasClicked = false;
        //private bool buttonYatzyWasClicked = false;
        private void UpdateScoreSheet()
        {
            List<Player> players = gameLogic.GetPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Id.Equals(gameLogic.GetActivePlayer().Id))
                {
                    switch (i)
                    {
                        case 0:
                            player_one_items.Items.Add(gameLogic.GetActivePlayer());
                            break;
                        case 1:
                            player_two_items.Items.Add(gameLogic.GetActivePlayer());
                            break;
                        case 2:
                            player_three_items.Items.Add(gameLogic.GetActivePlayer());
                            break;
                        case 3:
                            player_four_items.Items.Add(gameLogic.GetActivePlayer());
                            break;
                    }
                    
                }
            }
            
        }

        private void SendScore(int category)
        {
            gameLogic.SetScore(category);
            UpdateScoreSheet();
            NextTurn();
            ResetDiceButtons();
        }
        private void ResetDiceButtons()
        {
            string save = "Spara";
            hold_diceOne.Content = save;
            hold_diceTwo.Content = save;
            hold_diceThree.Content = save;
            hold_diceFour.Content = save;
            hold_diceFive.Content = save;
        }

        private void NextTurn()
        {
            gameLogic.NextPlayer();
            UpdatePlayer();
        }

        private void Btn_select_ones_Click(object sender, RoutedEventArgs e)
        {
            SendScore(0);
            //buttonOnesWasClicked = true;
        }

        private void Btn_select_twos_Click(object sender, RoutedEventArgs e)
        {
            SendScore(1);
            //buttonTwosWasClicked = true;
        }

        private void Btn_select_threes_Click(object sender, RoutedEventArgs e)
        {
            SendScore(2);
            //buttonThreesWasClicked = true;
        }

        private void Btn_select_fours_Click(object sender, RoutedEventArgs e)
        {
            SendScore(3);
            //buttonFoursWasClicked = true;
        }

        private void Btn_select_fives_Click(object sender, RoutedEventArgs e)
        {
            SendScore(4);
            //buttonFivesWasClicked = true;
        }

        private void Btn_select_sixes_Click(object sender, RoutedEventArgs e)
        {
            SendScore(5);
            //buttonSixesWasClicked = true;
        }

        private void Btn_select_pair_Click(object sender, RoutedEventArgs e)
        {
            SendScore(6);
            //buttonOnePairWasClicked = true;
        }

        private void Btn_select_twoPair_Click(object sender, RoutedEventArgs e)
        {
            SendScore(7);
            //buttonTwoPairWasClicked = true;
        }

        private void Btn_select_threeOfAKind_Click(object sender, RoutedEventArgs e)
        {
            SendScore(8);
            //buttonThreeOfAKindWasClicked = true;
        }

        private void Btn_select_fourOfAKind_Click(object sender, RoutedEventArgs e)
        {
            SendScore(9);
            //buttonFourOfAKindWasClicked = true;
        }

        private void Btn_select_smallStraight_Click(object sender, RoutedEventArgs e)
        {
            SendScore(10);
            //buttonSmallStraightWasClicked = true;
        }

        private void Btn_select_BigStraight_Click(object sender, RoutedEventArgs e)
        {
            SendScore(11);
            //buttonBigStraightWasClicked = true;
        }

        private void Btn_select_FullHouse_Click(object sender, RoutedEventArgs e)
        {
            SendScore(12);
            //buttonFullHouseWasClicked = true;
        }

        private void Btn_select_Chance_Click(object sender, RoutedEventArgs e)
        {
            SendScore(13);
            //buttonChanceWasClicked = true;
        }

        private void Btn_select_Yatzy_Click(object sender, RoutedEventArgs e)
        {
            SendScore(14);
            //buttonYatzyWasClicked = true;
        }

        private void end_game_Click(object sender, RoutedEventArgs e)
        {
            gameLogic.QuitGame();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void CountTime()
        {
            _time = TimeSpan.FromMinutes(2);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c"); //namnge textblocket till "tbTime"
                        if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        //private void CheckTimer()
        //{
        //    if (buttonOnesWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonTwosWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonThreesWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonFoursWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonFivesWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonSixesWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonOnePairWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonTwoPairWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonThreeOfAKindWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonFoursWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonSmallStraightWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonBigStraightWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonFullHouseWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonChanceWasClicked == true && _time == TimeSpan.Zero ||
        //        buttonYatzyWasClicked == true && _time == TimeSpan.Zero)
        //    {
        //        MessageBox.Show($"Tiden är slut");
        //    }

        //}
    }
}
    