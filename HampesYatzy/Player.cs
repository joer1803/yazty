using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class Player
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public int GamesPlayed { get; set; }
        public int TotalScore { get; set; }
        public override string ToString()
        {
            return Nickname;
        }
    }
}
