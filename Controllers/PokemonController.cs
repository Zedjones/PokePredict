using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        [HttpGet("/test")]
        public IEnumerable<int> Test()
        {
            var myStack = new ConcurrentStack<int>();
            Parallel.For(0, 100, i =>
            {
                myStack.Push(i);
            });
            return myStack.ToArray();
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
                return Ok(myMon);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
    }
}
