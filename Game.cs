using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwamiAPI
{
    public class Game
    {
        public string _id {get; set;}
        public string name { get; set; }
        public string favorite { get; set; }
        public string underdog { get; set; }
        public string favoriteName { get; set; }
        public string underdogName { get; set; }
        public double line { get; set; }
        public int week { get; set; }
        public string date { get; set; }
        public int favoriteScore { get; set; }
        public int underdogScore { get; set; }
        public string winner { get; set; }
        public string homeTeam { get; set; }

        private static List<Team> myTeams = Team.GetAllTeams();
        public Game(string name, string id, string favorite, string underdog, double line, int week, string date, 
                    int favoriteScore, int underdogScore, string winner, string homeTeam)
        {
            _id = id;
            this.favorite = favorite;
            this.underdog = underdog;
            this.line = line;
            this.week = week;
            this.date = date;
            this.favoriteScore = favoriteScore;
            this.underdogScore = underdogScore;
            this.winner = winner;
            this.homeTeam = homeTeam;

            Team tempFavorite = myTeams.Find(x => x._id == this.favorite);
            Team tempUnderdog = myTeams.Find(x => x._id == this.underdog);
            favoriteName = tempFavorite.name;
            underdogName = tempUnderdog.name;

            if(this.homeTeam == "favorite")
            {
                this.name = "At " + tempFavorite.name + " vs " + tempUnderdog.name;
            }
            else
            {
                if(this.homeTeam == "underdog")
                {
                    this.name = tempFavorite.name + " vs At " + tempUnderdog.name;
                }
                else
                {
                    this.name = tempFavorite.name + " vs " + tempUnderdog.name;
                }
            } 
        }

        
    }
}
