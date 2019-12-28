using PokeApiNet.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PokeFetch
{
    public class PokemonFetch
    {
        private static async Task CacheAllPokemon(PokeApiNet.PokeApiClient client, string previousPath)
        {
            var allMons = await client.GetNamedResourcePageAsync<Pokemon>(int.MaxValue, 0);
            var pokePath = Path.Join(previousPath, "Pokemon");
            if (!Directory.Exists(pokePath))
            {
                Directory.CreateDirectory(pokePath);
            }
            // If we already have all the Pokemon, return
            // TODO: Filter out Pokemon that we already have the files for
            if (new DirectoryInfo(pokePath).GetFiles().Length == allMons.Count) return;
            var pokeFiles = new DirectoryInfo(pokePath).GetFiles().Select(file => file.Name.Split('.')[0]).ToList();
            var filteredMons = allMons.Results.Where(mon => !pokeFiles.Contains(mon.Name));
            var detailedMons = await client.GetResourceAsync(filteredMons);
            Parallel.ForEach(detailedMons, myMon =>
            {
                var fullMon = new PokePredict.Database.Models.Pokemon(myMon, previousPath);
                System.Console.WriteLine(fullMon.Name);
                fullMon.WriteOut();
            });
            System.Diagnostics.Debug.WriteLine(allMons.Count);
        }
        private static async Task CacheAllTypes(PokeApiNet.PokeApiClient client, string previousPath)
        {
            var types = await client.GetNamedResourcePageAsync<Type>(int.MaxValue, 0);
            var typePath = Path.Join(previousPath, "Types");
            if (!Directory.Exists(typePath))
            {
                Directory.CreateDirectory(typePath);
            }
            // If we already have all the types, return
            // TODO: Filter out types that we already have the files for
            if (new DirectoryInfo(typePath).GetFiles().Length == types.Count) return;
            var allTypes = await client.GetResourceAsync(types.Results);
            Parallel.ForEach(allTypes, myType =>
            {
                var fullType = new PokePredict.Database.Models.Type(myType, previousPath);
                System.Console.WriteLine(fullType.Name);
                fullType.WriteOut();
            });
        }
        public static void CacheAll(string basePath)
        {
            var client = new PokeApiNet.PokeApiClient();
            var tasks = new List<Task>();
            tasks.Add(PokemonFetch.CacheAllPokemon(client, basePath));
            tasks.Add(PokemonFetch.CacheAllTypes(client, basePath));
            Task.WhenAll(tasks).Wait();
        }
    }
}