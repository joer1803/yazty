﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class YatzyGame
    {
        public List<InGamePlayer> Players { get; set; }
        public List<Die> Dice { get; set; }
        public Player Winner { get; set; }
        /*private bool SaveDice();*///Metod for att avsluta en spelares omgang, spara i kategori

        //MakeTurn metod, do a while loop maybe to keep track of rolling and score?
    }
}
