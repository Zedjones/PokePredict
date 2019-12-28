using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Newtonsoft.Json;
using PokePredict.Database.Models;

using PokeApiNet.Models;

namespace PokePredict.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        const string DataPath = "PokemonFiles";
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/pokemon")]
        public IActionResult Pokemon()
        {
            var mon = Request.Query["pokemon"];
            if (mon.Count == 0)
            {
                return BadRequest();
            }
            try
            {
                var watch = new Stopwatch();
                using (var db = new pokedexContext())
                {
                    var fullMon = db.Pokemon.First();
                    return Ok(JsonConvert.SerializeObject(fullMon.Species));
                }
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
    }
}
