using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class ScoreSheet
    {
        public ScoreSheet()
        {
            Categories = new bool[15];
            Scores = new int[15];
        }
        public int Bonus { get; set; }
        public bool[] Categories { get; set; }
        public int[] Scores { get; set; }
        public int Sum { get; set; }
        public int TotScore { get; set; }
    }
}
