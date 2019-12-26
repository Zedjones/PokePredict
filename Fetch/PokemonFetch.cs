using PokeApiNet.Models;
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
        public async void CacheAllTypes(string previousPath) {
            var types = await Client.GetNamedResourcePageAsync<Type>(int.MaxValue, 0);
            var allTypes = await Client.GetResourceAsync(types.Results);
            var myTypes = allTypes.Select(myType => new PokePredict.Database.Models.Type(myType, previousPath)).ToList();
            myTypes.ForEach(type => type.WriteOut());
        }
    }
}