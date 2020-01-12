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
    public class GamesController : ControllerBase
    {
        //URL
        private static string url = @"http://lucas-swami-api.herokuapp.com/games";
        
        //Games list
        private static List <Game> myGames = new List<Game>();
        // GET: api/Games
        [HttpGet]
        public IEnumerable<Game> Get()
        {   
            GetAllGames();
            return myGames;
        }

        // GET: api/Games/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return GetOneGame(id);
        }

        [HttpGet("setwinners/{mode}", Name = "SetWinner")]
        public string SetWinner(string mode)
        {
            SetWinner(mode);
            return "{ complete }";
        }


        // GET: api/Games/Week
        [HttpGet("week/{week}")]
        public IEnumerable<Game> GetWeek(string week)
        {
            List<Game> weekGames = GetWeekGames(week);
            
            return weekGames;
        }
        // POST: api/Games
        [HttpPost]
        public void Post([FromBody] Game value)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value).ToString(), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var response = httpClient.PostAsync(new Uri(url), content).Result;
            }
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Game value)
        {
            string tempurl = url + "/" + id;

            var content = new StringContent(JsonConvert.SerializeObject(value).ToString(), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var response = httpClient.PutAsync(new Uri(tempurl), content).Result;

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var response = httpClient.DeleteAsync(new Uri(url+"/"+id)).Result;

            }
        }

        private static List<Game> GetAllGames()
        {
            
            //Route
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var releases = JArray.Parse(response);
                Console.WriteLine(releases);
                myGames = releases.ToObject<List<Game>>();

                return myGames;
            }
        }

        private static List<Game> GetWeekGames(string week)
        {

            //Route
            using (var httpClient = new HttpClient())
            {
                
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url+"/week/"+week)).Result;

                var releases = JArray.Parse(response);
                Console.WriteLine(releases);
                List<Game> myWeekGames = releases.ToObject<List<Game>>();

                return myWeekGames;
            }


        }

        private static string GetOneGame(string id)
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

        private static void SetWinners(string mode)
        {
            foreach (Game myGame in myGames)
            {   if(mode == "all"||myGame.winner == null)
                {
                    if (myGame.favoriteScore == 0 && myGame.underdogScore == 0)
                    {
                        myGame.winner = "P";
                    }
                    else
                    {
                        if (myGame.favoriteScore - myGame.underdogScore == myGame.line)
                        {
                            myGame.winner = "P";
                        }
                        else
                        {
                            if (myGame.favoriteScore - myGame.underdogScore > myGame.line)
                            {
                                myGame.winner = "F";
                            }
                            else
                            {
                                myGame.winner = "U";
                            }
                        }
                    }
                }
                

                //Save the updated record
                var content = new StringContent(JsonConvert.SerializeObject(myGame).ToString(), Encoding.UTF8, "application/json");   
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                    string tempurl = url +"/"+ myGame._id;
                    Console.WriteLine(myGame._id + myGame.homeTeam);
                    var response = httpClient.PutAsync(new Uri(tempurl), content).Result;  
                }
            }
        }
    }
}
