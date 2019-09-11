using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class Yatzy
    {
        public static int Ones;
        public static int Twos;
        public static int Threes;
        public static int Fours;
        public static int Fives;
        public static int Sixes;
        public static int OnePair;
        public static int TwoPair;
        public static int ThreeOfAKind;
        public static int FourOfAKind;
        public static int SmallStraight;
        public static int BigStraight;
        public static int FullHouse;
        public static int Chance;
        public static bool IsYatzy;

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


        public int GetScore(int category, int[] dice)
        {
            switch (category) //väljer metod beroende på kategori. 1or - 6or är default
            {
                case 7:
                    return CheckThreeOfAKind(dice);
                //case 8:
                //    return CheckFourOfAKind(dice);
                //case 9:
                //    return CheckSmallStraight();
                //case 10:
                //    return CheckLargeStraight();
                //case 11:
                //    return CheckFullHouse();
                //case 12:
                //    return CountChance();
                //case 13:
                //    return CheckYatzy();
                default:
                    return CountNumbers(category, dice);



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
                            return sum = dice[i] * 2;
                        }
                    }
                }
                countSame = 0;
            }
            return sum;
        }
        //private int CheckSmallStraight(int die1, int die2, int die3, int die4, int die5)// Checking that dices are ! from each other and that there's no sixes'
        //{
        //    int sum = 0;

        //    if (CheckSmallStraight)

        //    {

        //      if ((die1 != die2) && (die1 != die3) && (die1 != die4) && (die1 != die5))

        //        {

        //            if ((die2 != die3) && (die2 != die4) && (die2 != die5))

        //            {

        //                if ((die3 != die4) && (die3 != die5))

        //                {

        //                    if (die4 != die5)

        //                    {

        //                        if ((die1 != '6') && (die2 != '6') && (die3 != '6') && (die4 != '6') && (die5 != '6'))

        //                        {

        //                            return sum= 15;

        //                        }

        //                        else 
        //                        {
        //                            return sum= 0;
        //                        }

        //                    }


        //private int CheckBigStraight(int[] dice )// Sorting the numbers and checking that it's in the correct order for a big straight
        //{
        //  int sum = 0;
        //  int[] i = new int[5];

        // HÄR SKA METODEN FÖR ATT SLÅ TÄRNINGAR IN


        //  Array.Sort(i);

        //  if( ((i[0] == 2) &&
        //       (i[1] == 3) &&
        //       (i[2] == 4) &&
        //       (i[3] == 5) &&
        //       (i[4] == 6)) )
        //  {
        //    Sum = 20;
        //  }

        //  return Sum;
        //}


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

    //private int CountChance(int[] dice)
    //{
    //  int sum = 0;

    //  foreach(int die in dice)
    //  {
    //    sum+= die;
    //  }

    //  return Sum;
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

    //private int CheckOnePair(int[] dice)
    //{
    //    int sum = 0;
    //    int countSame = 0;
    //    int[] numbers = new int[] { 1, 2, 3, 4, 5, 6 };
    //    for (int i = 0; i <= dice.Length; i--)
    //    {
    //        for (int j = 0; j <= numbers.Length; i--)
    //        {
    //            if (dice[i] == numbers[j])
    //            {
    //                countSame++;
    //                if (countSame == 2)
    //                {
    //                    sum = dice[i] * 2;
    //                }
    //            }
    //        }
    //    }
    //    return sum;

    //private int CheckOnePair(int[] dice)
    //{
    //    int sum = 0;

    //int[] i = new int[5];

    //Array.Sort(i);
    //Array.Reverse(i);

    //    if ((
    //         (i[3] == i[4]) && // One pair
    //         (i[2] != i[3])) ||
    //        ((i[0] == i[1]) && // One pair
    //         i[1] != i[2]))
    //    {
    //        sum = //om kravet ovan uppfylls returneras true vilket sen måste summeras;
    //    }

    //    return sum;
    //}

}

