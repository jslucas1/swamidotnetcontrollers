using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwamiAPI
{
    public class Wager
    {
        public string _id { get; set; }
        public string user { get; set; }
        public string game { get; set; }
        public string team { get; set; }
        public int amount { get; set; }
        public int week { get; set; }

        public Wager(string id, string user, string game, string team, int amount, int week)
        {
            _id = id;
            this.user = user;
            this.game = game;
            this.team = team;
            this.amount = amount;
            this.week = week;
        }
        
    }
}
