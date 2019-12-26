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
                //var types = await Client.GetNamedResourcePageAsync<Type>(int.MaxValue, 0);
                //var allTypes = await Client.GetResourceAsync(types.Results);
                //var myTypes = allTypes.Select(myType => new PokePredict.Database.Models.Type(myType, DataPath)).ToList();
                //myTypes.ForEach(type => type.WriteOut());
                var myMon = new PokePredict.Database.Models.Pokemon(fullMon, DataPath);
                //var myMon = await PokePredict.Database.Models.Pokemon.FromNetMon(fullMon, DataPath);
                _logger.LogInformation(watch.Elapsed.ToString());
                //myMon.WriteOut();
                return Ok(myMon);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
    }
}
