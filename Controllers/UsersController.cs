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
    public class UsersController : ControllerBase
    {
        //URL
        private static string url = @"http://lucas-swami-api.herokuapp.com/users";

        //Games list
        private static List<User> myUsers = new List<User>();
        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            GetAllUsers();
            return myUsers;
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetOneUser")]
        public string GetUser(string id)
        {
            return GetOneUser(id);
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] User value)
        {
            string tempurl = url + "/" + id;
            //Console.WriteLine("temp url is " + tempurl);
           // Console.WriteLine("The passed in object tostring is " + value.ToString());
            User tempUser = (User)value;
           // Console.WriteLine("The object toJason" + tempUser.ToJSON());

           
            using (var httpClient = new HttpClient())
            {
                //var content = new StringContent(JsonConvert.SerializeObject(value).ToString(), Encoding.UTF8, "application/json");
                var content = new StringContent(JsonConvert.SerializeObject(value).ToString(), Encoding.UTF8, "text/plain");
                Console.WriteLine("Value of content " + content.ToString());
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.PutAsync(new Uri(tempurl), content).Result;
               // var response = httpClient.PutAsync(new Uri(tempurl), tempUser.ToJSON()).Result;
            }
           
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

        private static List<User> GetAllUsers()
        {

            //Route
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var releases = JArray.Parse(response);
                myUsers = releases.ToObject<List<User>>();

                return myUsers;
            }
        }

        private static string GetOneUser(string id)
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
