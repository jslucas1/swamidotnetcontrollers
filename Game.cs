using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwamiAPI
{
    public class Game
    {
        private string name;
        private string favorite;
        private string underdog;
        private double line;
        private int week;
        private string date;
        private int favoriteScore;
        private int underdogScore;
        private string winner;
        private string homeTeam;

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

        public string _id { get; set; }
        public string Name { get => name; set => name = value; }
        public string Favorite { get => favorite; set => favorite = value; }
        public string Underdog { get => underdog; set => underdog = value; }
        public double Line { get => line; set => line = value; }
        public int Week { get => week; set => week = value; }
        public string Date { get => date; set => date = value; }
        public int FavoriteScore { get => favoriteScore; set => favoriteScore = value; }
        public int UnderdogScore { get => underdogScore; set => underdogScore = value; }
        public string Winner { get => winner; set => winner = value; }
        public string HomeTeam { get => homeTeam; set => homeTeam = value; }
    }
}
