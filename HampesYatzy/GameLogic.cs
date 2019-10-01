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
        
        public GameLogic(int gameId, int gametype)
        {
            game = new YatzyGame();
            game.Players = DbOperations.GetGame(gameId);
            game.StartTime = DateTime.Now;
            game.GameId = gameId;
            game.GameType = gametype;
            activePlayer = game.Players[0];
            if(game.GameType == 2)
            {
                StartSteeredYatzy();
            }
        }
        private void StartSteeredYatzy()
        {
            for(int i=0; i < game.Players.Count; i++)
            {
                for(int j = 1; j < game.Players[i].ScoreSheet.Categories.Length; j++)
                {
                    game.Players[i].ScoreSheet.Categories[j] = true;
                }
            }
        }
        public List<Player> GetPlayers()
        {
            return game.Players;
        }
        public bool CheckGameOver()
        {
            bool[] categories = game.Players[game.Players.Count-1].ScoreSheet.Categories;
            int count = 0;
            for (int i = 0; i < categories.Length; i++)
            {  
                if(categories[i] == true)
                {
                    count++;
                }  
            }
            if (count == 15)
            {
                return true;
            }
            return false;
        }
        public void SetGameOver()
        {    
                DbOperations.SetEndGame(game);
        }
        public Player GetWinner()
        {
            List<Player> players = game.Players.OrderByDescending(p => p.ScoreSheet.TotScore).ToList();
            return players[0];
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
                    if (game.Dice[i].Hold != true || game.Dice[i].Value == 0)
                    {
                        
                        game.Dice[i].Value = rnd.Next(1, 7);
                    }
                }
            }
        }

        public int GetThrows()
        {
            return throws;
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
            activePlayer.ScoreSheet.Scores[category] = GetScore(category, game.Dice);
            activePlayer.ScoreSheet.Categories[category] = true;
            AddTotScore(category);
            if(category < 6)
            {
                AddToSum(category);
            }
            if (game.GameType == 2)
            {
                if(category<activePlayer.ScoreSheet.Categories.Length-1)
                {
                    activePlayer.ScoreSheet.Categories[category + 1] = false;
                }
            }
            
        }
        private void AddToSum(int category)
        {
            activePlayer.ScoreSheet.Sum += activePlayer.ScoreSheet.Scores[category];
        }
        private void AddTotScore(int category)
        {           
                activePlayer.ScoreSheet.TotScore += activePlayer.ScoreSheet.Scores[category];   
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
                    if (dice[i].Value == dice[j].Value)
                    {
                        countSame++;
                        if (countSame == 3)
                        {
                           return sum = dice[i].Value * 3;
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
                    if (dice[i].Value == dice[j].Value)
                    {
                        countSame++;
                        if (countSame == 4)
                        {
                            return sum = dice[i].Value * 4;
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
                    if (dice[i].Value == dice[j].Value)
                    {
                        countSame++;
                        if (countSame == 2)
                        {
                            return sum = dice[i].Value + dice[i].Value;
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
                    if (dice[i].Value == dice[j].Value)
                    {
                        countSame++;
                        if (countSame == 2)
                        {
                            if (firstpair != 0 && firstpair != dice[i].Value)
                            {
                                return sum += dice[i].Value + dice[i].Value;
                            }
                            firstpair = dice[i].Value;
                            sum = dice[i].Value + dice[i].Value;
                        }
                    }
                }
                countSame = 0;
            }
            return sum = 0;
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
                    if (dice[i].Value == dice[j].Value)
                    {
                        countSame++;
                        if (countSame == 5)
                        {
                           return sum = 50;
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
                    if (dice[i].Value == dice[j].Value)
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
            return sum = 0;
        }
        private void CheckBonus()
        {
            int count = 0;
            if (!activePlayer.ScoreSheet.BonusTaken)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (activePlayer.ScoreSheet.Categories[i])
                    {
                        count++;
                    }
                }
                if (count == 6)
                {
                    activePlayer.ScoreSheet.BonusTaken = true;
                    if (game.GameType == 1)
                    {
                        BonusPointClassic();
                    }
                    else if (game.GameType == 2)
                    {
                        BonusPointStyrd();
                    }
                }
            }
            
        }
        
        private void BonusPointClassic()//Textblock or box for the total sum of 1-6 category goes inside the ()
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

            activePlayer.ScoreSheet.Bonus = sum;
            activePlayer.ScoreSheet.TotScore += activePlayer.ScoreSheet.Bonus;
        }

        private void BonusPointStyrd()//Textblock or box for the total sum of 1-6 category goes inside the ()
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
            activePlayer.ScoreSheet.Bonus = sum;
            activePlayer.ScoreSheet.TotScore += activePlayer.ScoreSheet.Bonus;
        }
        public void QuitGame()
        {
            DbOperations.DeleteGame(game.GameId);
        }
    }
}


    