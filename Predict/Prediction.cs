using PokePredict.Database;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokePredict.Predict
{
    public class Prediction
    {
        public static async Task<List<PokemonDto>> PredictTeam(List<PokemonDto> otherTeam)
        {
            var timer = new Stopwatch();
            timer.Start();
            List<PokePredict.Database.Models.Pokemon> otherTeamFull;
            using (var db = new PokePredict.Database.Models.pokedexContext()) 
            {
                otherTeamFull = Queries.AllPokemon(db)
                    .Where(pk => otherTeam.Select(opk => opk.Name).Contains(pk.Identifier))
                    .ToList();
                otherTeamFull.ForEach(pk => pk.PokemonMoves = pk.PokemonMoves.Where(move =>
                    otherTeam
                        .Where(opk => pk.Identifier == opk.Name)
                        .First()
                        .Moves
                        .Contains(move.Move.Identifier)
                ).ToList());
            }
            System.Console.WriteLine(timer.Elapsed.ToString());
            return new List<PokemonDto>();
        }
    }
}