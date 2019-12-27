using PokeApiNet.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PokePredict.Fetch
{
    public class PokemonFetch
    {
        private static async Task CacheAllPokemon(PokeApiNet.PokeApiClient client, string previousPath) {
            var allMons = await client.GetNamedResourcePageAsync<Pokemon>(1000, 0);
            var detailedMons = await client.GetResourceAsync(allMons.Results);
            Parallel.ForEach(detailedMons, myMons => {
                var fullMon = new PokePredict.Database.Models.Pokemon(myMons, previousPath);
                fullMon.WriteOut();
            });
            System.Diagnostics.Debug.WriteLine(allMons.Count);
        }
        private static async Task CacheAllTypes(PokeApiNet.PokeApiClient client, string previousPath) {
            var types = await client.GetNamedResourcePageAsync<Type>(int.MaxValue, 0);
            var allTypes = await client.GetResourceAsync(types.Results);
            Parallel.ForEach(allTypes, myType => {
                var fullType = new PokePredict.Database.Models.Type(myType, previousPath);
                fullType.WriteOut();
            });
        }
        public static void CacheAll(string previousPath) {
            var client = new PokeApiNet.PokeApiClient();
            var tasks = new List<Task>();
            tasks.Add(PokemonFetch.CacheAllTypes(client, previousPath));
            tasks.Add(PokemonFetch.CacheAllPokemon(client, previousPath));
            Task.WhenAll(tasks);
        }
    }
}