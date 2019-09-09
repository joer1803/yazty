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
        public static bool Yatzy;
        
        //A method to keep track of total score
        //A method to keep track of upper score
        //A method to see if the player got 63 points to get bonus points



        public int CalculateYahtzee( Dice[] myDice )
        {
          int Sum = 0;

          for( int i = 1; i <= 6; i++ )
          {
            int Count = 0;
            for( int j = 0; j < 5; j++ )
            {
              if( myDice[j].RollNumber == i )
                Count++;

              if( Count > 4 )
                Sum = 50;
            }
          }

          return Sum;
        }




        //Add dices when the same number is needed, the upper part of yatzy
        public int AddUpDices(int DieNumber, Dice[] myDice)
        {
            int Sum = 0;

            for (int i = 0; i < 5; i++)
            {
                if (myDice[i].RollNumber == DieNumber)
                {
                    Sum += DieNumber;
                }
            }

            return Sum;
        }

        //En metod som summerar tärningarnas värde om spelaren väljer att ta chansen
        public int AddUpChance( Dice[] myDice )
        {
          int Sum = 0;

          for( int i = 0; i < 5; i++ )
          {
            Sum += myDice[i].RollNumber;
          }

          return Sum;
        }




    }
}
