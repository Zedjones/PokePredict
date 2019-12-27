using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PokePredict.Fetch;

namespace PokePredict
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PokemonFetch.CacheAll("PokemonFiles");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
