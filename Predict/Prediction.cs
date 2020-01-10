using PokePredict.Database;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokePredict.Predict
{
    public class Prediction
    {
        public static List<PokemonDto> PredictTeam(List<PokemonDto> otherTeam)
        {
            var timer = new Stopwatch();
            timer.Start();
            var otherTeamFull = DtoToFull(otherTeam);
            System.Console.WriteLine(timer.Elapsed.ToString());
            return new List<PokemonDto>();
        }

        public static List<Database.Models.Pokemon> DtoToFull(List<PokemonDto> team) {
            List<PokePredict.Database.Models.Pokemon> teamFull;
            using (var db = new PokePredict.Database.Models.pokedexContext()) 
            {
                teamFull = Queries.AllPokemon(db)
                    .Where(pk => team.Select(opk => opk.Name).Contains(pk.Identifier))
                    .ToList();
                teamFull.ForEach(pk => pk.PokemonMoves = pk.PokemonMoves.Where(move =>
                    team
                        .Where(opk => pk.Identifier == opk.Name)
                        .First()
                        .Moves
                        .Contains(move.Move.Identifier)
                ).ToList());
            }
            return teamFull;
        }
    }
}