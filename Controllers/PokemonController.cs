using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using PokeApiNet.Models;

namespace PokePredict.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        PokeApiNet.PokeApiClient client = new PokeApiNet.PokeApiClient();

        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/pokemon")]
        public async Task<IActionResult> Pokemon()
        {
            var mon = Request.Query["pokemon"];
            if (mon.Count == 0)
            {
                return BadRequest();
            }
            try
            {
                var fullMon = await client.GetResourceAsync<Pokemon>(mon);
                var myMon = new PokePredict.Database.Models.Pokemon(fullMon);
                var monStr = JsonConvert.SerializeObject(myMon);
                var path = System.IO.Path.Combine("PokemonFiles", myMon.Name + ".json");
                System.IO.File.WriteAllText(path, monStr);
                var readIn = JsonConvert.DeserializeObject<PokePredict.Database.Models.Pokemon>(monStr);
                return Ok(myMon);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
    }
}
