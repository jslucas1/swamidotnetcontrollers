using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace SwamiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagersController : ControllerBase
    {
        //URL
        private static string url = @"http://lucas-swami-api.herokuapp.com/wagers";

        //Games list
        private static List<Wager> myWagers = new List<Wager>();
        // GET: api/Wagers
        [HttpGet]
        public IEnumerable<Wager> Get()
        {
            GetAllWagers();
            return myWagers;
        }

        // GET: api/Wagers/5
        [HttpGet("{id}", Name = "GetWager")]
        public string Get(string id)
        {
            return GetOneWager(id);
        }

        // GET: api/Games/Week
        [HttpGet("week/{week}")]
        public IEnumerable<Wager> GetWeek(string week)
        {
            List<Wager> weekWagers = GetWeekWagers(week);

            return weekWagers;
        }

        // GET: api/Games/Week
        [HttpGet("week/{week}/{userId}")]
        public IEnumerable<Wager> GetWeekForUser(string week, string userId)
        {
            List<Wager> weekForUserWagers = GetWeekForUserWagers(week, userId);

            return weekForUserWagers;
        }

        // POST: api/Wagers
        [HttpPost]
        public void Post([FromBody] Wager value)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value).ToString(), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var response = httpClient.PostAsync(new Uri(url), content).Result;
            }
        }

        // PUT: api/Wagers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var response = httpClient.DeleteAsync(new Uri(url + "/" + id)).Result;

            }

        }

        private static List<Wager> GetAllWagers()
        {

            //Route
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var releases = JArray.Parse(response);
                myWagers = releases.ToObject<List<Wager>>();

                foreach (Wager myWager in myWagers)
                {
                    Game myGame = JsonConvert.DeserializeObject<Game>(Game.GetOneGame(myWager.game));
                    if (myWager.team == "favorite")
                    {
                        myWager.teamName = myGame.favoriteName;
                    }
                    else
                    {
                        myWager.teamName = myGame.underdogName;
                    }
                }

                    return myWagers;
            }
        }

        private static List<Wager> GetWeekWagers(string week)
        {

            //Route
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url + "/week/" + week)).Result;

                var releases = JArray.Parse(response);
                List<Wager> myWeekWagers = releases.ToObject<List<Wager>>();

                return myWeekWagers;
            }
        }

        private static List<Wager> GetWeekForUserWagers(string week, string userId)
        {

            //Route
            using (var httpClient = new HttpClient())
            {
                string tempurl = url + "/week/" + week + "/" + userId;
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(tempurl)).Result;

                var releases = JArray.Parse(response);
                List<Wager> myWeekWagers = releases.ToObject<List<Wager>>();

                return myWeekWagers;
            }
        }
        private static string GetOneWager(string id)
        {

            //Route
            using (var httpClient = new HttpClient())
            {
                string tempurl = url + "/" + id;
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(tempurl)).Result;

                return response;
            }
        }
    }
}
