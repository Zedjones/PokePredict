using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using PokePredict.Database.Models;
using PokePredict.Database;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

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
            List<Database.PokemonDto> fullMon;
            using (var db = new pokedexContext())
            {
                /*
                fullMon = Queries.AllPokemon(db)
                    .Where(pk => pk.Identifier == mon[0])
                    .First();
                */
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

                    var jsSettings = new JsonSerializerSettings();
                    jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    var monStr = JsonConvert.SerializeObject(Queries.DtoList, jsSettings);
                    var body = Encoding.UTF8.GetBytes(monStr);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "pokemon",
                                         basicProperties: null,
                                         body: body);
                    _logger.LogInformation("Wrote Pokemon to channel");
                }
            }
            return Ok(Queries.DtoList);
        }
    }
}
