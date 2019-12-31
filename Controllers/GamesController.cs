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
            //SetWinners();
            return myGames;
        }

        // GET: api/Games/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Games
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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

        private static void SetWinners()
        {
            foreach (Game myGame in myGames)
            {
                if (myGame.FavoriteScore == 0 && myGame.UnderdogScore == 0)
                {
                    myGame.Winner = "P";
                }
                else
                {
                    if (myGame.FavoriteScore - myGame.UnderdogScore == myGame.Line)
                    {
                        myGame.Winner = "P";
                    }
                    else
                    {
                        if (myGame.FavoriteScore - myGame.UnderdogScore > myGame.Line)
                        {
                            myGame.Winner = "F";
                        }
                        else
                        {
                            myGame.Winner = "U";
                        }
                    }
                }

                //Save the updated record
                var content = new StringContent(JsonConvert.SerializeObject(myGame).ToString(), Encoding.UTF8, "application/json");   
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                    string tempurl = url +"/"+ myGame._id;
                    Console.WriteLine(myGame._id + myGame.HomeTeam);
                    var response = httpClient.PutAsync(new Uri(tempurl), content).Result;  
                }
            }
        }
    }
}
