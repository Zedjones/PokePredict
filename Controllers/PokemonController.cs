using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Newtonsoft.Json;

using PokeApiNet.Models;

namespace PokePredict.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        const string DataPath = "PokemonFiles";
        PokeApiNet.PokeApiClient Client = new PokeApiNet.PokeApiClient();

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
                var watch = new Stopwatch();
                watch.Start();
                var fullMon = await Client.GetResourceAsync<Pokemon>(mon);
                _logger.LogInformation(watch.Elapsed.ToString());
                var myMon = new PokePredict.Database.Models.Pokemon(fullMon, DataPath);
                _logger.LogInformation(watch.Elapsed.ToString());
                myMon.WriteOut();
                return Ok(myMon);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
    }
}
