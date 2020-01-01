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
    public class Team
    {
        private string Name;
        private string League;
        private string Conference;

        public Team(string id, string Name, string League, string Conference)
        {
            _id = id;
            name = Name;
            league = League;
            conference = Conference;
        }

        public string _id { get; set; }
        public string name { get => Name; set => Name = value; }
        public string league { get => League; set => League = value; }
        public string conference { get => Conference; set => Conference = value; }

        public Boolean Equals(string id)
        {
            return this._id == id;
        }

        public static List<Team> GetAllTeams()
        {
            string url = @"http://lucas-swami-api.herokuapp.com/teams";
            //Route
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var releases = JArray.Parse(response);
                List<Team> myTeams = releases.ToObject<List<Team>>();

                return myTeams;
            }
        }
    }
}
