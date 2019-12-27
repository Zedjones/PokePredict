using PokeApiNet.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace PokePredict.Fetch
{
    public class PokemonFetch
    {
        private static async Task CacheAllPokemon(PokeApiNet.PokeApiClient client, string previousPath) {
            var allMons = await client.GetNamedResourcePageAsync<Pokemon>(int.MaxValue, 0);
            var pokePath = Path.Join(previousPath, "Pokemon");
            // If we already have all the Pokemon, return
            if(new DirectoryInfo(pokePath).GetFiles().Length == allMons.Count) return;
            var detailedMons = await client.GetResourceAsync(allMons.Results);
            Parallel.ForEach(detailedMons, myMons => {
                var fullMon = new PokePredict.Database.Models.Pokemon(myMons, previousPath);
                fullMon.WriteOut();
            });
            System.Diagnostics.Debug.WriteLine(allMons.Count);
        }
        private static async Task CacheAllTypes(PokeApiNet.PokeApiClient client, string previousPath) {
            var types = await client.GetNamedResourcePageAsync<Type>(int.MaxValue, 0);
            var typePath = Path.Join(previousPath, "Types");
            // If we already have all the types, return
            if(new DirectoryInfo(typePath).GetFiles().Length == types.Count) return;
            var allTypes = await client.GetResourceAsync(types.Results);
            Parallel.ForEach(allTypes, myType => {
                var fullType = new PokePredict.Database.Models.Type(myType, previousPath);
                fullType.WriteOut();
            });
        }
        public static void CacheAll(string basePath) {
            var client = new PokeApiNet.PokeApiClient();
            var tasks = new List<Task>();
            tasks.Add(PokemonFetch.CacheAllPokemon(client, basePath));
            tasks.Add(PokemonFetch.CacheAllTypes(client, basePath));
            Task.WhenAll(tasks);
        }
    }
}