using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class GameLogic
    {
        public const int Ones = 1;
        public const int Twos = 2;
        public const int Threes = 3;
        public const int Fours = 4;
        public const int Fives = 5;
        public const int Sixes = 6;
        public const int OnePair = 7;
        public const int TwoPair = 8;
        public const int ThreeOfAKind = 9;
        public const int FourOfAKind = 10;
        public const int SmallStraight = 11;
        public const int BigStraight = 12;
        public const int FullHouse = 13;
        public const int Chance = 14;
        public const int Yatzy = 15;
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
        private void CheckGameOver()
        {
            bool[] categories = activePlayer.ScoreSheet.Categories;
            int count = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                if (count == 15)
                {
                    DbOperations.SetEndGame(game);
                }
                else if(categories[i] == true)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
        }

        public Player GetActivePlayer()
        {
            return activePlayer;
        }

        public void NextPlayer()
        {
            int index = 0;
            for (int i = 0; i < game.Players.Count; i++)
            {
                if (game.Players[i].Equals(game.Players.Count))
                {
                    index = i;
                }
            }
            index++;
            if (index.Equals(game.Players.Count))
            {
                index = 0;
                CheckGameOver();
            }

            activePlayer = game.Players[index];
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

        public void SetScore(int category, List<Die> dice)
        {
            switch (category)

            {
                case Ones:
                    activePlayer.ScoreSheet.Ones = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case Twos:
                    activePlayer.ScoreSheet.Twos = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case Threes:
                    activePlayer.ScoreSheet.Threes = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case Fours:
                    activePlayer.ScoreSheet.Fours = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case Fives:
                    activePlayer.ScoreSheet.Fives = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case Sixes:
                    activePlayer.ScoreSheet.Sixes = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case OnePair:
                    activePlayer.ScoreSheet.OnePair = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case TwoPair:
                    activePlayer.ScoreSheet.TwoPair = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case ThreeOfAKind:
                    activePlayer.ScoreSheet.ThreeOfAKind = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case FourOfAKind:
                    activePlayer.ScoreSheet.FourOfAKind = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case SmallStraight:
                    activePlayer.ScoreSheet.SmallStraight = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case BigStraight:
                    activePlayer.ScoreSheet.BigStraight = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case FullHouse:
                    activePlayer.ScoreSheet.FullHouse = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case Chance:
                    activePlayer.ScoreSheet.Chance = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;

                case Yatzy:
                    activePlayer.ScoreSheet.Yatzy = GetScore(category, dice);
                    activePlayer.ScoreSheet.Categories[category - 1] = true;
                    break;
            }
        }


        private int CountNumbers(int category, List<Die> dice)
        {
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

        private int CheckSmallStraight(List<Die> dice)
        {
            int sum = 0;
            int count = 0;

            Array.Sort(dice.ToArray());
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

        private int CheckBigStraight(List<Die> dice)
        {
            int sum = 0;
            int count = 0;

            Array.Sort(dice.ToArray());
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

        private int CheckOnePair(List<Die> dice)
        {
            int sum = 0;
            int countSame = 0;
            Array.Sort(dice.ToArray());
            Array.Reverse(dice.ToArray());
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


        private int BonusPointClassic(int totalUp)//Textblock or box for the total sum of 1-6 category goes inside the ()
        {
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

        private int BonusPointStyrd(int totalUp)//Textblock or box for the total sum of 1-6 category goes inside the ()
        {
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

//private int CheckFullHouse(int[] dice)
//{
//    int sum = 0;

//    int[] i = new int[5];

//    Array.Sort(i);

//    if ((((i[0] == i[1]) && (i[1] == i[2])) && // Three of a Kind
//         (i[3] == i[4]) && // Two of a Kind
//         (i[2] != i[3])) ||
//        ((i[0] == i[1]) && // Two of a Kind
//         ((i[2] == i[3]) && (i[3] == i[4])) && // Three of a Kind
//         (i[1] != i[2])))
//    {
//        sum = //om kravet ovan uppfylls returneras true vilket sen måste summeras;
//    }

//    return sum;
//}


    