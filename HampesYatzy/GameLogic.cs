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
        int throws = 0;
        YatzyGame game;
        Player activePlayer;

        public GameLogic(int gameId)
        {
            game = new YatzyGame();
            game.Players = DbOperations.GetGame(gameId);
            game.StartTime = DateTime.Now;
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
            if (throws != 3)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (game.Dice[i].Hold != true)
                    {
                        throws++;
                        game.Dice[i].Value = rnd.Next(1, 7);
                    }
                }
            }
        }

        public List<Die> GetDice()
        {
            return game.Dice;
        }

        public int GetScore(int category, int[] dice)
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

        public void SetScore(int category, int points)
        {
            switch (category)

            {
                case 1:
                    activePlayer.ScoreSheet.Ones = GetScore(1,);
                    break;

                case 2:
                    activePlayer.ScoreSheet.Twos = GetScore(2,);
                    break;

                case 3:
                    activePlayer.ScoreSheet.Threes = GetScore(3,);
                    break;

                case 4:
                    activePlayer.ScoreSheet.Fours = GetScore(4,);
                    break;

                case 5:
                    activePlayer.ScoreSheet.Fives = GetScore(5,);
                    break;

                case 6:
                    activePlayer.ScoreSheet.Sixes = GetScore(6,);
                    break;

                case 7:
                    activePlayer.ScoreSheet.Bonus = GetScore(7,);
                    break;

                case 8:
                    activePlayer.ScoreSheet.OnePair = GetScore(8,);
                    break;

                case 9:
                    activePlayer.ScoreSheet.TwoPair = GetScore(9,);
                    break;

                case 10:
                    activePlayer.ScoreSheet.ThreeOfAKind = GetScore(10,);
                    break;

                case 11:
                    activePlayer.ScoreSheet.FourOfAKind = GetScore(11,);
                    break;

                case 12:
                    activePlayer.ScoreSheet.SmallStraight = GetScore(12,);
                    break;

                case 13:
                    activePlayer.ScoreSheet.BigStraight = GetScore(13,);
                    break;

                case 14:
                    activePlayer.ScoreSheet.FullHouse = GetScore(14,);
                    break;

                case 15:
                    activePlayer.ScoreSheet.Chance = GetScore(15,);
                    break;

                case 16:
                    activePlayer.ScoreSheet.Yatzy = GetScore(16,);
                    break;

                case 17:
                    activePlayer.ScoreSheet.Sum = GetScore(17,);
                    break;
            }
        }


        private int CountNumbers(int category, int[] dice)
        {
            int sum = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] == category)
                {
                    sum += category;
                }
            }
            return sum;
        }

        private int CheckThreeOfAKind(int[] dice)
        {
            int sum = 0;
            int countSame = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                for (int j = 0; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 3)
                        {
                            sum = dice[i] * 3;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CheckFourOfAKind(int[] dice)
        {
            int sum = 0;
            int countSame = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                for (int j = 0; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 4)
                        {
                            sum = dice[i] * 4;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CheckSmallStraight(int[] dice)
        {
            int sum = 0;
            int count = 0;

            Array.Sort(dice);
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[count] < dice[i] && dice[i] != 6)
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

        private int CheckBigStraight(int[] dice)
        {
            int sum = 0;
            int count = 0;

            Array.Sort(dice);
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[count] < dice[i] && dice[i] != 1)
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

        private int CheckOnePair(int[] dice)
        {
            int sum = 0;
            int countSame = 0;
            Array.Sort(dice);
            Array.Reverse(dice);
            for (int i = 0; i < dice.Length; i++)
            {
                for (int j = 0; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 2)
                        {
                            sum = dice[i] * 2;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CheckTwoPair(int[] dice)
        {
            int sum = 0;
            int countSame = 0;
            int firstpair = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                for (int j = 0; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 2)
                        {
                            if (firstpair != 0 && firstpair != dice[i])
                            {
                                return sum += dice[i] * 2;
                            }
                            firstpair = dice[i];
                            sum = dice[i] * 2;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }

        private int CountChance(int[] dice)
        {
            int sum = 0;

            foreach (int die in dice)
            {
                sum += die;
            }

            return sum;
        }

        private int CheckYatzy(int[] dice)
        {
            int sum = 0;
            int countSame = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                for (int j = 0; j < dice.Length; j++)
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

        private int CheckFullHouse(int[] dice)
        {
            int sum = 0;
            int countSame = 0;
            int threeOfAKind = 0;
            for (int i = 0; i < dice.Length; i++)
            {
                for (int j = 0; j < dice.Length; j++)
                {
                    if (dice[i] == dice[j])
                    {
                        countSame++;
                        if (countSame == 3)
                        {
                            threeOfAKind = dice[i];
                            sum = dice[i] * 3;
                        }
                        else if (countSame == 2 && threeOfAKind != 0 && dice[i] != threeOfAKind)
                        {
                            return sum += dice[i] + dice[i];
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