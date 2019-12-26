using PokeApiNet.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PokePredict.Fetch
{
    public class PokemonFetch
    {
        PokeApiNet.PokeApiClient Client = new PokeApiNet.PokeApiClient();
        public async void CacheAllPokemon() {
            var allMons = await Client.GetNamedResourcePageAsync<Pokemon>(1000, 0);
            var detailedMons = await Client.GetResourceAsync(allMons.Results);
            System.Diagnostics.Debug.WriteLine(allMons.Count);
        }
        public static async Task CacheAllTypes(string previousPath) {
            var client = new PokeApiNet.PokeApiClient();
            var types = await client.GetNamedResourcePageAsync<Type>(int.MaxValue, 0);
            var allTypes = await client.GetResourceAsync(types.Results);
            Parallel.ForEach(allTypes, myType => {
                var fullType = new PokePredict.Database.Models.Type(myType, previousPath);
                fullType.WriteOut();
            });
        }
    }
}