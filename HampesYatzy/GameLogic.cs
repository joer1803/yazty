using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class GameLogic
    {
        public const int Ones = 0;
        public const int Twos = 1;
        public const int Threes =2;
        public const int Fours = 3;
        public const int Fives = 4;
        public const int Sixes = 5;
        public const int OnePair = 6;
        public const int TwoPair = 7;
        public const int ThreeOfAKind =8;
        public const int FourOfAKind = 9;
        public const int SmallStraight = 10;
        public const int BigStraight = 11;
        public const int FullHouse = 12;
        public const int Chance = 13;
        public const int Yatzy = 14;
        int throws = 3;
        YatzyGame game;
        Player activePlayer;
        
        public GameLogic(int gameId)
        {
            game = new YatzyGame();
            game.Players = DbOperations.GetGame(gameId);
            game.StartTime = DateTime.Now;
            game.GameId = gameId;
            activePlayer = game.Players[0];
        }
        public List<Player> GetPlayers()
        {
            return game.Players;
        }
        public bool CheckGameOver()
        {
            bool[] categories = activePlayer.ScoreSheet.Categories;
            int count = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                if (count == 15)
                {
                    return true;
                }
                else if(categories[i] == true)
                {
                    count++;
                }  
            }
            return false;
        }
        public void SetGameOver()
        {    
                DbOperations.SetEndGame(game);            
        }

        public Player GetActivePlayer()
        {
            return activePlayer;
        }
        private void ResetThrows()
        {
            throws = 3;
        }

        public void NextPlayer()
        {
            int index = 0;
            for (int i = 0; i < game.Players.Count; i++)
            {
                if (game.Players[i].Equals(activePlayer))
                {
                    index = i;
                }
            }
            index++;
            if (index.Equals(game.Players.Count))
            {
                index = 0;
                if (CheckGameOver())
                {
                    SetGameOver();
                }
                

            }
            ResetDice();
            ResetThrows();
            CheckBonus();
            activePlayer = game.Players[index];
        }
        private void ResetDice()
        {
            for(int i = 0; i < game.Dice.Count; i++)
            {
                game.Dice[i].Hold = false;
                game.Dice[i].Value = 0;
            }
        }

        public void RollDice()
        {
            Random rnd = new Random();
            if (throws != 0)
            {
                throws--;
                for (int i = 0; i < 5; i++)
                {
                    if (game.Dice[i].Hold != true)
                    {
                        
                        game.Dice[i].Value = rnd.Next(1, 7);
                    }
                }
            }
        }

        public List<Die> GetDice()
        {
            return game.Dice;
        }

        private int GetScore(int category, List<Die> dice)
        {
            switch (category) //väljer metod beroende på kategori. 1or - 6or är default
            {
                case OnePair:
                    return CheckOnePair(dice);

                case TwoPair:
                    return CheckTwoPair(dice);

                case ThreeOfAKind:
                    return CheckThreeOfAKind(dice);

                case FourOfAKind:
                    return CheckFourOfAKind(dice);

                case SmallStraight:
                    return CheckSmallStraight(dice);

                case BigStraight:
                    return CheckBigStraight(dice);

                case FullHouse:
                    return CheckFullHouse(dice);

                case Chance:
                    return CountChance(dice);

                case Yatzy:
                    return CheckYatzy(dice);

                default:
                    return CountNumbers(category, dice);
            }
        }

        public void SetScore(int category)
        {
                switch (category)

                {
                    case Ones:
                        activePlayer.ScoreSheet.Scores[Ones] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case Twos:
                        activePlayer.ScoreSheet.Scores[Twos] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case Threes:
                        activePlayer.ScoreSheet.Scores[Threes] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case Fours:
                        activePlayer.ScoreSheet.Scores[Fours] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case Fives:
                        activePlayer.ScoreSheet.Scores[Fives] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case Sixes:
                        activePlayer.ScoreSheet.Scores[Sixes] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case OnePair:
                        activePlayer.ScoreSheet.Scores[OnePair] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case TwoPair:
                        activePlayer.ScoreSheet.Scores[TwoPair] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case ThreeOfAKind:
                        activePlayer.ScoreSheet.Scores[ThreeOfAKind] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case FourOfAKind:
                        activePlayer.ScoreSheet.Scores[FourOfAKind] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case SmallStraight:
                        activePlayer.ScoreSheet.Scores[SmallStraight] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case BigStraight:
                        activePlayer.ScoreSheet.Scores[BigStraight] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case FullHouse:
                        activePlayer.ScoreSheet.Scores[FullHouse] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case Chance:
                        activePlayer.ScoreSheet.Scores[Chance] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;

                    case Yatzy:
                        activePlayer.ScoreSheet.Scores[Yatzy] = GetScore(category, game.Dice);
                        activePlayer.ScoreSheet.Categories[category] = true;
                        break;
                }
            
        }


        private int CountNumbers(int category, List<Die> dice)
        {
            category = category + 1;
            int sum = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i].Value == category)
                {
                    sum += category;
                }
            }
            return sum;
        }

        private int CheckThreeOfAKind(List<Die> dice)
        {
            int sum = 0;
            int countSame = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = 0; j < dice.Count; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 3)
                        {
                            sum = dice[i].Value * 3;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CheckFourOfAKind(List<Die> dice)
        {
            int sum = 0;
            int countSame = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = 0; j < dice.Count; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 4)
                        {
                            sum = dice[i].Value * 4;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CheckSmallStraight(List<Die> diceUnsorted)
        {
            int sum = 0;
            int count = 0;

            List<Die> dice = diceUnsorted.OrderByDescending(d => d.Value).ToList();
            dice.Reverse();
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[count].Value < dice[i].Value && dice[i].Value != 6)
                {
                    count++;
                }
                if (count == 4)
                {
                    sum = 15;
                }
            }
            return sum;
        }

        private int CheckBigStraight(List<Die> diceUnsorted)
        {
            int sum = 0;
            int count = 0;

            List<Die> dice = diceUnsorted.OrderByDescending(d => d.Value).ToList();
            dice.Reverse();
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[count].Value < dice[i].Value && dice[i].Value != 1)
                {
                    count++;
                }
                if (count == 4)
                {
                    sum = 20;
                }
            }
            return sum;
        }

        private int CheckOnePair(List<Die> diceUnsorted)
        {
            int sum = 0;
            int countSame = 0;
            List<Die> dice = diceUnsorted.OrderByDescending(d => d.Value).ToList();
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = 0; j < dice.Count; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 2)
                        {
                            sum = dice[i].Value * 2;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CheckTwoPair(List<Die> dice)
        {
            int sum = 0;
            int countSame = 0;
            int firstpair = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = 0; j < dice.Count; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 2)
                        {
                            if (firstpair != 0 && firstpair != dice[i].Value)
                            {
                                return sum += dice[i].Value * 2;
                            }
                            firstpair = dice[i].Value;
                            sum = dice[i].Value * 2;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CountChance(List<Die> dice)
        {
            int sum = 0;

            foreach (Die die in dice)
            {
                sum += die.Value;
            }

            return sum;
        }

        private int CheckYatzy(List<Die> dice)
        {
            int sum = 0;
            int countSame = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = 0; j < dice.Count; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 5)
                        {
                            sum = 50;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CheckFullHouse(List<Die> dice)
        {
            int sum = 0;
            int countSame = 0;
            int threeOfAKind = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                for (int j = 0; j < dice.Count; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 3)
                        {
                            threeOfAKind = dice[i].Value;
                            sum = dice[i].Value * 3;
                        }
                        else if (countSame == 2 && threeOfAKind != 0 && dice[i].Value != threeOfAKind)
                        {
                            return sum += dice[i].Value + dice[i].Value;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }
        private void CheckBonus()
        {
            int count = 0;
            for(int i = 0; i < 6; i++)
            {
                if (activePlayer.ScoreSheet.Categories[i])
                {
                    count++;
                }
            }
            if (count == 5)
            {
                activePlayer.ScoreSheet.Bonus = BonusPointClassic();
            }
        }
        
        private int BonusPointClassic()//Textblock or box for the total sum of 1-6 category goes inside the ()
        {
            int totalUp = 0;
            for(int i = 0; i < 6; i++)
            {
                totalUp += activePlayer.ScoreSheet.Scores[i];
            }
            int sum = 0;
            if (totalUp >= 63)
            {
                sum = 50;
            }
            else
            {
                sum = 0;
            }

            return sum;
        }

        private int BonusPointStyrd()//Textblock or box for the total sum of 1-6 category goes inside the ()
        {
            int totalUp = 0;
            for (int i = 0; i < 6; i++)
            {
                totalUp += activePlayer.ScoreSheet.Scores[i];
            }
            int sum = 0;
            if (totalUp >= 42)
            {
                sum = 50;
            }
            else
            {
                sum = 0;
            }

            return sum;
        }
        public void QuitGame()
        {
            DbOperations.DeleteGame(game.GameId);
        }
    }
}


    