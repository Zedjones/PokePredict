using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PokeApiNet.Models;

namespace PokePredict.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        PokeApiNet.PokeApiClient client = new PokeApiNet.PokeApiClient();
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
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
                var fullmon = await client.GetResourceAsync<Pokemon>(mon);
                var allMoves = await client.GetResourceAsync(
                    fullmon.Moves.Select(move => move.Move)
                );
                return Ok(allMoves);
            }
            catch (HttpRequestException)
            {
                return BadRequest();
            }
        }
    }
}
