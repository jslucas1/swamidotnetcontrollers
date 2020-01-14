using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace SwamiAPI
{
    public class Wager
    {
        public string _id { get; set; }
        public string user { get; set; }
        public string game { get; set; }
        public string team { get; set; }

        public string teamName { get; set; }
        public int amount { get; set; }
        public int week { get; set; }

        public Wager()
        {

        }

        public Wager(string id, string user, string game, string team, int amount, int week)
        {
            _id = id;
            this.user = user;
            this.game = game;
            this.team = team;
            this.amount = amount;
            this.week = week;

            //Team tempTeam = myTeams.Find(x => x._id == team);
            Game myGame = JsonConvert.DeserializeObject<Game>(Game.GetOneGame(game));
            if(team == "favorite")
            {
                this.teamName = myGame.favoriteName;
            }
            else
            {
                this.teamName = myGame.underdogName;
            }
        }
        
    }
}
