﻿using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using PokePredict.Database.Models;
using System.Threading.Tasks;
using PokePredict.Database;
using RabbitMQ.Client;
using System.Linq;
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
            Database.Models.Pokemon fullMon;
            using (var db = new pokedexContext())
            {
                fullMon = Queries.AllPokemon(db)
                    .Where(pk => pk.Identifier == mon[0])
                    .First();
                Queries.ReduceMoves(fullMon);
                _logger.LogInformation(watch.Elapsed.ToString());
                _logger.LogInformation(fullMon.PokemonMoves.Count.ToString());
                Predict.Prediction.GetDamageDone(fullMon, _logger, db);
            }

            var jsSettings = new JsonSerializerSettings();
            jsSettings.Formatting = Formatting.Indented;
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var monStr = JsonConvert.SerializeObject(fullMon, jsSettings);

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "pokemon",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(monStr);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "pokemon",
                                         basicProperties: null,
                                         body: body);
                    _logger.LogInformation("Wrote Pokemon to channel");
                }
            }
            return Ok(monStr);
        }
        [HttpPost("/process")]
        public async Task<IActionResult> ProcessTeam()
        {
            var jsonReader = new StreamReader(Request.Body);
            var pokemonJson = await jsonReader.ReadToEndAsync();
            var jsSettings = new JsonSerializerSettings();
            jsSettings.MissingMemberHandling = MissingMemberHandling.Error;
            try
            {
                var monList = JsonConvert.DeserializeObject<List<PokemonDto>>(pokemonJson, jsSettings);
                var fullTeam = Predict.Prediction.DtoToFull(monList);
                return Ok(fullTeam);
            }
            catch (JsonSerializationException jse)
            {
                return BadRequest(jse.Message);
            }
        }
    }
}
