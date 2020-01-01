using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwamiAPI
{
    public class Wager
    {
        private string user;
        private string game;
        private string team;
        private int amount;
        private int week;

        public Wager(string id, string user, string game, string team, int amount, int week)
        {
            _id = id;
            this.user = user;
            this.game = game;
            this.team = team;
            this.amount = amount;
            this.week = week;
        }
        public string _id { get; set; }
        public string User { get => user; set => user = value; }
        public string Game { get => game; set => game = value; }
        public string Team { get => team; set => team = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Week { get => week; set => week = value; }
    }
}
