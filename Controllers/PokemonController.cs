using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using PokePredict.Database.Models;
using RabbitMQ.Client;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

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
            var factory = new ConnectionFactory { HostName = "localhost" };
            var watch = new Stopwatch();
            watch.Start();
            Pokemon fullMon;
            using (var db = new pokedexContext())
            {
                fullMon = db.Pokemon
                    .Include(pk => pk.Species)
                    //Include all moves
                    .Include(pk => pk.PokemonMoves)
                    .ThenInclude(move => move.Move)
                    .ThenInclude(move => move.Target)
                    //Include all stats
                    .Include(pk => pk.PokemonStats)
                    .ThenInclude(stat => stat.Stat)
                    .Where(pk => pk.Identifier == mon[0])
                    .First();
                _logger.LogInformation(watch.Elapsed.ToString());
            }
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "pokemon",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var monStr = JsonConvert.SerializeObject(fullMon);
                    var body = Encoding.UTF8.GetBytes(monStr);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "pokemon",
                                         basicProperties: null,
                                         body: body);
                    _logger.LogInformation("Wrote Pokemon to channel");
                }
            }
            return Ok(fullMon);
        }
    }
}
