using PokePredict.Database;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace PokePredict.Predict
{
    public class Prediction
    {
        public static List<PokemonDto> PredictTeam(List<PokemonDto> otherTeam)
        {
            var timer = new Stopwatch();
            timer.Start();
            var otherTeamFull = DtoToFull(otherTeam);
            return new List<PokemonDto>();
        }

        /// <summary>
        /// Convert from PokemonDto to a full Pokemon object from the database
        /// </summary>
        /// <param name="team">A list of PokemonDto objects to convert</param>
        /// <returns>A list of Pokemon objects with only the specified moves</returns>
        public static List<Database.Models.Pokemon> DtoToFull(List<PokemonDto> team) {
            List<PokePredict.Database.Models.Pokemon> teamFull;
            using (var db = new PokePredict.Database.Models.pokedexContext()) 
            {
                // Only get Pokemon that are in the team
                teamFull = Queries.AllPokemon(db)
                    .Where(pk => team.Select(opk => opk.Name).Contains(pk.Identifier))
                    .ToList();
                // Only get moves that are specified for each specific Pokemon
                teamFull.ForEach(pk => pk.PokemonMoves = 
                    pk.PokemonMoves.Where(move =>
                        team
                            .Where(opk => pk.Identifier == opk.Name)
                            .First()
                            .Moves
                            .Contains(move.Move.Identifier)
                    )
                    .ToList()
                );
                // Get rid of duplicate moves with different version IDs
                Queries.ReduceMoves(teamFull.ToArray());
            }
            return teamFull;
        }
    }
}