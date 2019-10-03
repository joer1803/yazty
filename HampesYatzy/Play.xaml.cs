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
            DisableDiceButtons();
            scoreRequired.Text = gameLogic.GetReqBonus();
        }

        private void SetInitials() // sätter initialer på "spelbrädet" för alla spelare
        {
            List<Label> txtblcks = new List<Label>();
            txtblcks.Add(playerOneInitial);
            txtblcks.Add(playerTwoInitial);
            txtblcks.Add(playerThreeInitial);
            txtblcks.Add(playerFourInitial);
            List<Player> players = gameLogic.GetPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                string initials = $"{players[i].Firstname[0]}{players[i].Lastname[0]}";
                txtblcks[i].Content = initials.ToUpper();
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

        private List<Image> MakeImageList() // gör en lista av tomma bilder
        {
            List<Image> dices = new List<Image>();
            dices.Add(diceOne);
            dices.Add(diceTwo);
            dices.Add(diceThree);
            dices.Add(diceFour);
            dices.Add(diceFive);
            return dices;
        }

        private List<BitmapImage> MakeDiceImageList() // gör en lista av bitmapsbilder av tärningar
        {
            List<BitmapImage> diceImages = new List<BitmapImage>();
            for (int i = 1; i < 7; i++)
            {
                BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + i.ToString() + ".png", UriKind.Relative));
                diceImages.Add(image);
            }
            return diceImages;
        }

        private List<BitmapImage> MakeDiceImageHoldList() //gör en lista av röda tärningar (sparade tärningar)
        {
            List<BitmapImage> diceImages = new List<BitmapImage>();
            for (int i = 1; i < 7; i++)
            {
                BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + i.ToString() + "red.png", UriKind.Relative));
                diceImages.Add(image);
            }
            return diceImages;
        }
        private List<Image> GetDiceGifList() // gör en lista av gifs med rullande tärningar
        {
            List<Image> diceGifs = new List<Image>();
            diceGifs.Add(diceOneGif);
            diceGifs.Add(diceTwoGif);
            diceGifs.Add(diceThreeGif);
            diceGifs.Add(diceFourGif);
            diceGifs.Add(diceFiveGif);
            return diceGifs;
        }
        private void UpdateDice() // uppdaterar alla tärningsbilder och gifs
        {
            List<BitmapImage> diceImages = MakeDiceImageList();
            List<Image> diceFrames = MakeImageList();
            List<Die> dice = gameLogic.GetDice();
            List<BitmapImage> diceRedImages = MakeDiceImageHoldList();
            List<Image> rollingdice = GetDiceGifList();
            if (dice[0].Value != 0)
            {
                for (int i = 0; i < diceFrames.Count; i++)
                {
                    if (dice[i].Hold == true)
                    {
                        diceFrames[i].Source = diceRedImages[dice[i].Value - 1];
                        rollingdice[i].Opacity = 0;

                    }
                    else
                    {
                        diceFrames[i].Source = diceImages[dice[i].Value - 1];
                        rollingdice[i].Opacity = 100;
                    }
                }
            }
            DisplayHoldLabels();

        }


        private void UpdatePlayer() // uppdaterar label med namnet på spelaren som har turen
        {
            txtActivePlayer.Text = $"{gameLogic.GetActivePlayer().Firstname} {gameLogic.GetActivePlayer().Lastname}";
        }

        private void CheckDie(int index) // kollar om en tärning är sparad eller inte
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
        private List<Label> GetHoldLabelList() // gör en lista av labels ovanför varje tärning där det står "sparad"
        {
            List<Label> labels = new List<Label>();
            labels.Add(lblDiceOne);
            labels.Add(lblDiceTwo);
            labels.Add(lblDiceThree);
            labels.Add(lblDiceFour);
            labels.Add(lblDiceFive);
            return labels;
        }

        private void DisplayHoldLabels() // visar sparad ovanför tärningar som ska sparas
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
        private void EnableDiceButtons()
        {
            holdDiceOne.IsEnabled = true;
            holdDiceTwo.IsEnabled = true;
            holdDiceThree.IsEnabled = true;
            holdDiceFour.IsEnabled = true;
            holdDiceFive.IsEnabled = true;
        }
        private void DisableDiceButtons()
        {
            holdDiceOne.IsEnabled = false;
            holdDiceTwo.IsEnabled = false;
            holdDiceThree.IsEnabled = false;
            holdDiceFour.IsEnabled = false;
            holdDiceFive.IsEnabled = false;
        }

        private void HoldDiceOne_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(0);
            UpdateDice();
        }

        private void HoldDiceTwo_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(1);
            UpdateDice();
        }

        private void HoldDiceThree_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(2);
            UpdateDice();
        }

        private void HoldDiceFour_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(3);
            UpdateDice();
        }

        private void HoldDiceFive_Click(object sender, RoutedEventArgs e)
        {
            CheckDie(4);
            UpdateDice();
        }

        private void btnThrowDice_Click(object sender, RoutedEventArgs e)
        {
            HideFirework();
            gameLogic.RollDice();
            CategoryTaken();
            UpdateDice();
            DisplayThrows();
            EnableDiceButtons();
        }

        private void DisplayThrows() // visar antalet kast man har kvar
        {
            countThrow.Content = $"Du har {gameLogic.GetThrows()} kast kvar";
            DisableThrowButton();
        }
        private void DisableThrowButton()
        {
            if (gameLogic.GetThrows() == 0)
            {
                btnThrowDice.IsEnabled = false;
            }
            else
            {
                btnThrowDice.IsEnabled = true;
            }
        }

        private void UpdateScoreSheet() // uppdaterar poängen 
        {
            List<Player> players = gameLogic.GetPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Id.Equals(gameLogic.GetActivePlayer().Id))
                {
                    switch (i)
                    {
                        case 0:
                            playerOneItems.Items.Clear();
                            playerOneItems.Items.Add(gameLogic.GetActivePlayer());
                            break;

                        case 1:
                            playerTwoItems.Items.Clear();
                            playerTwoItems.Items.Add(gameLogic.GetActivePlayer());
                            break;

                        case 2:
                            playerThreeItems.Items.Clear();
                            playerThreeItems.Items.Add(gameLogic.GetActivePlayer());
                            break;

                        case 3:
                            playerFourItems.Items.Clear();
                            playerFourItems.Items.Add(gameLogic.GetActivePlayer());
                            break;
                    }
                }
            }
        }
        private void ClearDice() // rensar tärningsbilder så dom är tomma
        {
            for (int i = 0; i < MakeImageList().Count; i++)
            {
                MakeImageList()[i].Source = null;

            }
        }
        private void CategoryTaken() //kollar om en kategori är tagen och gör så den knappen inte går klicka på
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

        private List<Button> GetButtonList() // gör en lista av alla kategorik knappar
        {
            List<Button> catButtons = new List<Button>();
            catButtons.Add(btnSelectOnes);
            catButtons.Add(btnSelectTwos);
            catButtons.Add(btnSelectThrees);
            catButtons.Add(btnSelectFours);
            catButtons.Add(btnSelectFives);
            catButtons.Add(btnSelectSixes);
            catButtons.Add(btnSelectPair);
            catButtons.Add(btnSelectTwoPair);
            catButtons.Add(btnSelectThreeOfAKind);
            catButtons.Add(btnSelectFourOfAKind);
            catButtons.Add(btnSelectSmallStraight);
            catButtons.Add(btnSelectBigStraight);
            catButtons.Add(btnSelectFullHouse);
            catButtons.Add(btnSelectChance);
            catButtons.Add(btnSelectYatzy);
            return catButtons;
        }

        private void SteerButtons() //sköter knapparna för styrd yatzy
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

        private void SendScore(int category) // skickar iväg vald kategori för poängsättning
        {
            
            if (!gameLogic.GetActivePlayer().ScoreSheet.Categories[category])
            {
                gameLogic.SetScore(category);
                UpdateScoreSheet();
                if (category == 14)
                {
                    ShowFireWork();
                }
                NextTurn();
                CheckTimer();
                ClearDice();
            }
            else
            {
                MessageBox.Show("Du har redan tagit denna kategori");
            }
          
        }
        private void GameOverCheck() //kollar om spelet är slut och skickar ett meddelande till vinnaren
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

        private void NextTurn() // nästa spelare och kör en massa metoder för gränssnittet
        {
            gameLogic.NextPlayer();
            GameOverCheck();
            UpdatePlayer();
            DisableCategoryButtons();
            DisableDiceButtons();
            DisplayThrows();
            DisplayHoldLabels();
        }

        private void btnSelectOnes_Click(object sender, RoutedEventArgs e)
        {
            SendScore(0);
        }

        private void btnSelectTwos_Click(object sender, RoutedEventArgs e)
        {
            SendScore(1);
        }

        private void btnSelectThrees_Click(object sender, RoutedEventArgs e)
        {
            SendScore(2);
        }

        private void btnSelectFours_Click(object sender, RoutedEventArgs e)
        {
            SendScore(3);
        }

        private void btnSelectFives_Click(object sender, RoutedEventArgs e)
        {
            SendScore(4);
        }

        private void btnSelectSixes_Click(object sender, RoutedEventArgs e)
        {
            SendScore(5);
        }

        private void btnSelectPair_Click(object sender, RoutedEventArgs e)
        {
            SendScore(6);
        }

        private void btnSelectTwoPair_Click(object sender, RoutedEventArgs e)
        {
            SendScore(7);
        }

        private void btnSelectThreeOfAKind_Click(object sender, RoutedEventArgs e)
        {
            SendScore(8);
        }

        private void btnSelectFourOfAKind_Click(object sender, RoutedEventArgs e)
        {
            SendScore(9);
        }

        private void btnSelectSmallStraight_Click(object sender, RoutedEventArgs e)
        {
            SendScore(10);
        }

        private void btnSelectBigStraight_Click(object sender, RoutedEventArgs e)
        {
            SendScore(11);
        }

        private void btnSelectFullHouse_Click(object sender, RoutedEventArgs e)
        {
            SendScore(12);
        }

        private void btnSelectChance_Click(object sender, RoutedEventArgs e)
        {
            SendScore(13);
        }

        private void btnSelectYatzy_Click(object sender, RoutedEventArgs e)
        {
            SendScore(14);
        }

        private void btnEndGame_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
        }

        private void CountTime() // startar tiden (2 timmar) 
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

        private void EndGame() // avslutar spelet
        {
            gameLogic.QuitGame();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void EndOrQuitGame() //kollar om spelet är slut
        {
            if (gameLogic.CheckGameOver() == false)
            {
                EndGame();
            }
        }

        private void CheckTimer() //kollar om timern är slut
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
        private void HideFirework()
        {
            fireworkyatzy.Visibility = Visibility.Hidden;
        }
        public void ShowFireWork() // visar fyrverkerier om någon får yatzy
        {
            if (gameLogic.GetActivePlayer().ScoreSheet.Scores[14] == 50)
            {
                fireworkyatzy.Visibility = Visibility.Visible;
            }
            else
            {
                fireworkyatzy.Visibility = Visibility.Hidden;
            }
        }
    }
}