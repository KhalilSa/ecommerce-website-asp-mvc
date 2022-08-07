using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project278.Data;
using Project278.Models;
using RestSharp;

namespace Project278.Controllers
{
    public class ApiController : Controller
    {
        private readonly AppDbContext _db;

        public ApiController(AppDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpGet(Name = "Data")]
        public async Task<IActionResult> PopulateTables()
        {
            List<LabelJson> data = new List<LabelJson>();
            string secretKey = "BDgrWxrraCvfqRrVuTwJ&secret=dpzzjuaRCModCIjWqTHXDpwnfUOcmIwV";
            int i = 1;
            while (true) {
                string api1 = $"https://api.discogs.com//labels/{i++}/releases?page=1&per_page=100&key={secretKey}";
                var client1 = new RestClient(api1);
                var request1 = new RestRequest();
                request1.AddHeader("Accept", "application/json");
                request1.AddHeader("Content-Type", "application/json");
                RestResponse response1 = await client1.ExecuteAsync(request1);
                var content = response1.Content;
                if (!content.Contains("releases") || i > 2) break;
                content = "{" + content.Substring(content.IndexOf("\"releases\""));
                var jsonData = JsonConvert.DeserializeObject<LabelJson>(content);
                foreach (var label in jsonData.Labels) {
                    label.Price = Math.Round((new System.Random()).NextDouble() * 100, 2);
                    string api = $"https://api.discogs.com/database/search?q={label.ArtistName.Replace(" ", "+")}&key=BDgrWxrraCvfqRrVuTwJ&secret=dpzzjuaRCModCIjWqTHXDpwnfUOcmIwV";
                    var client = new RestClient(api);
                    var request = new RestRequest();
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");
                    RestResponse response = await client.ExecuteAsync(request);
                    content = response.Content;
                    if (!content.Contains("results")) break;
                    content = "{" + content.Substring(content.IndexOf("\"results\""));
                    var artistData = JsonConvert.DeserializeObject<ArtistJson>(content);
                    Artist artist = artistData.Artists.First();
                    label.ArtistId = artist.ArtistId;
                    if (!_db.Labels.Contains(label))
                        _db.Labels.Add(label);
                    if (!_db.Artists.Contains(artist))
                        _db.Artists.Add(artist);
                    _db.SaveChanges();

                }
                data.Add(jsonData);
            }
            

            //TODO: transform the response here to suit your needs
            return Json(data);
        }
    }
}
