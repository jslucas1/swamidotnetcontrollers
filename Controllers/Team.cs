using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwamiAPI.Controllers
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
    }
}
