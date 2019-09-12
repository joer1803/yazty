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
        //A method to keep track of total score
        //A method to keep track of upper score
        //A method to see if the player got 63 points to get bonus points



        //public int CalculateYatzy( Dice[] myDice )
        //{
        //  int Sum = 0;

        //  for( int i = 1; i <= 6; i++ )
        //  {
        //    int Count = 0;
        //    for( int j = 0; j < 5; j++ )
        //    {
        //      if( myDice[j].RollNumber == i )
        //        Count++;

        //      if( Count == 5 )
        //        Sum = 50;
        //    }
        //  }

        //  return Sum;
        //}




        //Add dices when the same number is needed, the upper part of yatzy
        //public int AddUpDices(int DieNumber, Dice[] myDice)
        //{
        //    int Sum = 0;

        //    for (int i = 0; i < 5; i++)
        //    {
        //        if (myDice[i].RollNumber == DieNumber)
        //        {
        //            Sum += DieNumber;
        //        }
        //    }

        //    return Sum;
        //}


        public static int GetScore(int category, int[] dice)
        {
            switch (category) //väljer metod beroende på kategori. 1or - 6or är default
            {
                case OnePair:
                    return CheckOnePair(dice);
                //case TwoPair:
                //    return CheckTwoPair(dice);
                case ThreeOfAKind:
                    return CheckThreeOfAKind(dice);
                case FourOfAKind:
                    return CheckFourOfAKind(dice);
                case SmallStraight:
                    return CheckSmallStraight(dice);
                case BigStraight:
                    return CheckBigStraight(dice);
                //case FullHouse:
                //    return CheckFullHouse(dice);
                case Chance:
                    return CountChance(dice);
                case Yatzy:
                    return CheckYatzy(dice);
                default:
                    return CountNumbers(category, dice);



            }
        }
        private static int CountNumbers(int category, int[] dice)
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
        private static int CheckThreeOfAKind(int[] dice)
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
        private static int CheckFourOfAKind(int[] dice)
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
        private static int CheckSmallStraight(int[] dice)
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
        private static int CheckBigStraight(int[] dice)
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
        private static int CheckOnePair(int[] dice)
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
            return sum = 0;
        }
        private static int CountChance(int[] dice)
        {
            int sum = 0;

            foreach (int die in dice)
            {
                sum += die;
            }

            return sum;
        }
        private static int CheckYatzy(int[] dice)
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
        //private int CheckTwoPair(int[] dice)
        //{
        //    int sum = 0;
        //    int countSame = 0;
        //    Array.Sort(dice);
        //    Array.Reverse(dice);
        //    for (int i = 0; i < dice.Length; i++)
        //    {
        //        for (int j = 0; j < dice.Length; j++)
        //        {
        //            if (dice[i] == dice[j])
        //            {
        //                countSame++;
        //                if (countSame == 2)
        //                {
        //                    //Här måste en loop eller if-sats leta efter det andra paret.
        //                }
        //            }
        //        }
        //        countSame = 0;
        //    }
        //    return sum;


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



//private int CheckYatzy(int[] dice)
//{
//    int sum = 0;
//    int countSame = 0;
//    int[] numbers = new int[] { 1, 2, 3, 4, 5, 6 };
//    for (int i = 0; i <= dice.Length; i++)
//    {
//        for (int j = 0; j <= numbers.Length; i++)
//        {
//            if (dice[i] == numbers[j])
//            {
//                countSame++;
//                if (countSame == 5)
//                {
//                    sum = 50;
//                }
//            }
//        }
//    }
//    return sum;
//}

//private bool eller int BonusPoint()//Should it be a bool or an int?
//{
//    int sum = 0;
//    if(TotalUp == 63 || TotalUp > 63)
//    {
//        sum = 50;
//    }
//    else
//    {
//        sum = 0;
//    }

//    return sum;
//}




