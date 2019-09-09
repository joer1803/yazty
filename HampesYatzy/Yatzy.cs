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

        //En metod som summerar tärningarnas värde om spelaren väljer att ta chansen
        //public int AddUpChance( Dice[] myDice )
        //{
        //  int Sum = 0;

        //  for( int i = 0; i < 5; i++ )
        //  {
        //    Sum += myDice[i].RollNumber;
        //  }

        //  return Sum;
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
            for(int i = 0; i < dice.Length; i++)
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
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6 };
            for (int i=0;i<=dice.Length;i++)
            {
                for(int j = 0; j <= numbers.Length; i++)
                {
                    if(dice[i] == numbers[j])
                    {
                        countSame++;
                        if (countSame == 3)
                        {
                            sum = dice[i] * 3;
                        }
                    }
                }
            }
            return sum;
        }
    }
}
