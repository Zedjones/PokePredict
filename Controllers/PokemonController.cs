using System.Net.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Newtonsoft.Json;
using PokePredict.Database.Models;

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
                watch.Start();
                using (var db = new pokedexContext())
                {
                    var fullMon = db.Pokemon
                        .Include(pk => pk.Species)
                        .Include(pk => pk.PokemonStats)
                        .ThenInclude(stat => stat.Stat)
                        .ToList();
                    _logger.LogInformation(watch.Elapsed.ToString());
                    return Ok(fullMon.Select(mon => mon.PokemonStats.Select(stat => stat.Stat)));
                }
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
    }
}
