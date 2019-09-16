using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class Player
    {
        //public Player()
        //{
        //    Stats = new PlayerStats();
        //}
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public PlayerStats Stats { get; set; }
        //public int GamesPlayed { get; set; }
        public ScoreSheet ScoreSheet { get; set; }
        public int TotalScore { get; set; }

    }
}