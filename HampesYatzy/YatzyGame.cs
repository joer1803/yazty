using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HampesYatzy
{
    class YatzyGame
    {

        public YatzyGame()
        {
            Dice = new List<Die>();
            for(int i=0; i<5; i++)
            {
                Die die = new Die();
                Dice.Add(die);
            }
        }

        public int GameId { get; set; }
        public DateTime StartTime { get; set; }
        public List<Player> Players { get; set; }
        public List<Die> Dice { get; set; }
        public Player Winner { get; set; }
        public string GameType { get; set; }
        /*private bool SaveDice();*///Metod for att avsluta en spelares omgang, spara i kategori

        //MakeTurn metod, do a while loop maybe to keep track of rolling and score?
    }
}