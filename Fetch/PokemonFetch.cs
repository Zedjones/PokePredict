using PokeApiNet.Models;

namespace PokePredict.Fetch
{
    public class PokemonFetch
    {
        PokeApiNet.PokeApiClient client = new PokeApiNet.PokeApiClient();
        public async void CacheAllPokemon() {
            var allMons = await client.GetNamedResourcePageAsync<Pokemon>(1000, 0);
            var detailedMons = await client.GetResourceAsync(allMons.Results);
            System.Diagnostics.Debug.WriteLine(allMons.Count);
        }
    }
}