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

        public Play(int gameId, int gametype)
        {
            InitializeComponent();
            gameLogic = new GameLogic(gameId, gametype);
            UpdatePlayer();
            SetInitials();
            CountTime();
            DisableCategoryButtons();
            score_required.Text = gameLogic.GetReqBonus();
        }

        private void SetInitials()
        {
            List<Label> txtblcks = new List<Label>();
            txtblcks.Add(player_one_initial);
            txtblcks.Add(player_two_initial);
            txtblcks.Add(player_three_initial);
            txtblcks.Add(player_four_initial);
            List<Player> players = gameLogic.GetPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                string initials = $"{players[i].Firstname[0]}{players[i].Lastname[0]}";
                txtblcks[i].Content = initials;
            }
        }

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

        private List<BitmapImage> MakeDiceImageHoldList()
        {
            List<BitmapImage> diceImages = new List<BitmapImage>();
            for (int i = 1; i < 7; i++)
            {
                BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + i.ToString() + "red.png", UriKind.Relative));
                diceImages.Add(image);
            }
            return diceImages;
        }

        private void UpdateDice()
        {
            List<BitmapImage> diceImages = MakeDiceImageList();
            List<Image> diceFrames = MakeImageList();
            List<Die> dice = gameLogic.GetDice();
            List<BitmapImage> diceRedImages = MakeDiceImageHoldList();
            if(dice[0].Value!= 0)
            {
                for (int i = 0; i < diceFrames.Count; i++)
                {
                    if (dice[i].Hold == true)
                    {
                        diceFrames[i].Source = diceRedImages[dice[i].Value - 1];
                        
                    }
                    else
                    {
                        diceFrames[i].Source = diceImages[dice[i].Value - 1];
                    }
                }
            }
            DisplayHoldLabels();
           
        }


        private void UpdatePlayer()
        {
            txtActivePlayer.Text = $"{gameLogic.GetActivePlayer().Firstname} {gameLogic.GetActivePlayer().Lastname}";
        }

        private void CheckDie(int index)
        {
            if (gameLogic.GetDice()[index].Hold == true)
            {
                gameLogic.GetDice()[index].Hold = false;
                
            }
            else
            {
                gameLogic.GetDice()[index].Hold = true;

            }
        }
        private List<Label> GetHoldLabelList()
        {
            List<Label> labels = new List<Label>();
            labels.Add(lblHold_diceOne);
            labels.Add(lblHold_diceTwo);
            labels.Add(lblHold_diceThree);
            labels.Add(lblHold_diceFour);
            labels.Add(lblHold_diceFive);
            return labels;
        }

        private void DisplayHoldLabels()
        {
            List<Label> labels = GetHoldLabelList();

            for (int i = 0; i < labels.Count; i++)
            {
                if (gameLogic.GetDice()[i].Hold == true)
                {
                    labels[i].Content = "Sparad";

                }
                else
                {
                    labels[i].Content = "";

                }
            }

        }

        private void Hold_diceOne_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(0);
            UpdateDice();
        }

        private void Hold_diceTwo_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(1);
            UpdateDice();
        }

        private void Hold_diceThree_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(2);
            UpdateDice();
        }

        private void Hold_diceFour_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(3);
            UpdateDice();
        }

        private void Hold_diceFive_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(4);
            UpdateDice();
        }

        private void Trow_dice_Click(object sender, RoutedEventArgs e)
        {
            gameLogic.RollDice();
            CategoryTaken();
            UpdateDice();
            DisplayThrows();
        }

        private void DisplayThrows()
        {
            count_trow.Content = $"Du har {gameLogic.GetThrows()} kast kvar";
        }

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
                            player_one_items.Items.Clear();
                            player_one_items.Items.Add(gameLogic.GetActivePlayer());
                            break;

                        case 1:
                            player_two_items.Items.Clear();
                            player_two_items.Items.Add(gameLogic.GetActivePlayer());
                            break;

                        case 2:
                            player_three_items.Items.Clear();
                            player_three_items.Items.Add(gameLogic.GetActivePlayer());
                            break;

                        case 3:
                            player_four_items.Items.Clear();
                            player_four_items.Items.Add(gameLogic.GetActivePlayer());
                            break;
                    }
                }
            }
        }
        private void ClearDice()
        {
            for(int i = 0; i < MakeImageList().Count; i++)
            {
                MakeImageList()[i].Source = null;

            }
        }
        private void CategoryTaken()
        {
            List<Button> buttons = GetButtonList();
            for (int i = 0; i < buttons.Count; i++)
            {
                if (gameLogic.GetActivePlayer().ScoreSheet.Categories[i])
                {
                    buttons[i].IsEnabled = false;
                }
                else
                {
                    buttons[i].IsEnabled = true;
                }
            }
        }

        private List<Button> GetButtonList()
        {
            List<Button> catButtons = new List<Button>();
            catButtons.Add(btn_select_ones);
            catButtons.Add(btn_select_twos);
            catButtons.Add(btn_select_threes);
            catButtons.Add(btn_select_fours);
            catButtons.Add(btn_select_fives);
            catButtons.Add(btn_select_sixes);
            catButtons.Add(btn_select_pair);
            catButtons.Add(btn_select_twoPair);
            catButtons.Add(btn_select_threeOfAKind);
            catButtons.Add(btn_select_fourOfAKind);
            catButtons.Add(btn_select_smallStraight);
            catButtons.Add(btn_select_bigStraight);
            catButtons.Add(btn_select_fullHouse);
            catButtons.Add(btn_select_chance);
            catButtons.Add(btn_select_yatzy);
            return catButtons;
        }

        private void SteerButtons()
        {
            DisableCategoryButtons();
            List<Button> buttons = GetButtonList();
            int index = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (gameLogic.GetActivePlayer().ScoreSheet.Categories[i] == true)
                {
                    index = i;
                }
            }
            buttons[index + 1].IsEnabled = true;
        }

        private void DisableCategoryButtons()
        {
            foreach (Button b in GetButtonList())
            {
                b.IsEnabled = false;
            }
        }

        private void SendScore(int category)
        {
            if (!gameLogic.GetActivePlayer().ScoreSheet.Categories[category])
            {
                gameLogic.SetScore(category);
                UpdateScoreSheet();
                NextTurn();
                CheckTimer();
                ClearDice();
            }
            else
            {
                MessageBox.Show("Du har redan tagit denna kategori");
            }
        }
        private void GameOverCheck()
        {
            if (gameLogic.CheckGameOver())
            {
                gameLogic.SetGameOver();
                MessageBox.Show($"GRATTIS {gameLogic.GetWinner().Nickname.ToUpper()} DU VANN SPELET!!! WOOOOOWWWWW");
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                this.Close();
            }
        }

        private void NextTurn()
        {
            gameLogic.NextPlayer();
            GameOverCheck();
            UpdatePlayer();
            DisableCategoryButtons();
            DisplayThrows();
            DisplayHoldLabels();
        }

        private void Btn_select_ones_Click(object sender, RoutedEventArgs e)
        {
            SendScore(0);
        }

        private void Btn_select_twos_Click(object sender, RoutedEventArgs e)
        {
            SendScore(1);
        }

        private void Btn_select_threes_Click(object sender, RoutedEventArgs e)
        {
            SendScore(2);
        }

        private void Btn_select_fours_Click(object sender, RoutedEventArgs e)
        {
            SendScore(3);
        }

        private void Btn_select_fives_Click(object sender, RoutedEventArgs e)
        {
            SendScore(4);
        }

        private void Btn_select_sixes_Click(object sender, RoutedEventArgs e)
        {
            SendScore(5);
        }

        private void Btn_select_pair_Click(object sender, RoutedEventArgs e)
        {
            SendScore(6);
        }

        private void Btn_select_twoPair_Click(object sender, RoutedEventArgs e)
        {
            SendScore(7);
        }

        private void Btn_select_threeOfAKind_Click(object sender, RoutedEventArgs e)
        {
            SendScore(8);
        }

        private void Btn_select_fourOfAKind_Click(object sender, RoutedEventArgs e)
        {
            SendScore(9);
        }

        private void Btn_select_smallStraight_Click(object sender, RoutedEventArgs e)
        {
            SendScore(10);
        }

        private void Btn_select_BigStraight_Click(object sender, RoutedEventArgs e)
        {
            SendScore(11);
        }

        private void Btn_select_FullHouse_Click(object sender, RoutedEventArgs e)
        {
            SendScore(12);
        }

        private void Btn_select_Chance_Click(object sender, RoutedEventArgs e)
        {
            SendScore(13);
        }

        private void Btn_select_Yatzy_Click(object sender, RoutedEventArgs e)
        {
            SendScore(14);
        }

        private void end_game_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
        }

        private void CountTime()
        {
            _time = TimeSpan.FromHours(2);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private void EndGame()
        {
            gameLogic.QuitGame();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void EndOrQuitGame()
        {
            if (gameLogic.CheckGameOver() == false)
            {
                EndGame();
            }
        }

        private void CheckTimer()
        {
            if (_time <= TimeSpan.Zero)
            {
                MessageBox.Show($"Tiden är slut");
                EndOrQuitGame();
            }
        }

        private void Tutorial_MouseLeave(object sender, MouseEventArgs e)
        {
            Tutorial.Content = null;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Tutorial.Content = new Tutorial();
        }
    }
}