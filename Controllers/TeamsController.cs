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
    public class TeamsController : ControllerBase
    {
        //URL
        private static string url = @"http://lucas-swami-api.herokuapp.com/teams";

        //Games list
        private static List<Team> myTeams = new List<Team>();
        // GET: api/Teams
        [HttpGet]
        public IEnumerable<Team> GetTeams()
        {
            GetAllTeams();
            return myTeams;
        }

        // GET: api/Teams/5
        [HttpGet("{id}", Name = "GetTeam")]
        public string GetTeam(string id)
        {
            return GetOneTeam(id);
        }

        // POST: api/Teams
        [HttpPost]
        public void PostTeam([FromBody] string value)
        {
        }

        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public void PutTeam(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteTeam(string id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                var response = httpClient.DeleteAsync(new Uri(url+"/"+id)).Result;

            }
        }

        public static List<Team> GetAllTeams()
        {

            //Route
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var releases = JArray.Parse(response);
                myTeams = releases.ToObject<List<Team>>();

                return myTeams;
            }
        }

        private static string GetOneTeam(string id)
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
