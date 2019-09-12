using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class InGamePlayer : Player
    {
        public int Score { get; set; }
        public bool Ones { get; set; }
        public bool Twos { get; set; }
        public bool Threes { get; set; }
        public bool Fours { get; set; }
        public bool Fives { get; set; }
        public bool Sixes { get; set; }
        public bool OnePair { get; set; }
        public bool TwoPair { get; set; }
        public bool ThreeOfAKind { get; set; }
        public bool FourOfAKind { get; set; }
        public bool SmallStraight { get; set; }
        public bool BigStraight { get; set; }
        public bool FullHouse { get; set; }
        public bool Chance { get; set; }
        public bool Yatzy { get; set; }
    }
}
