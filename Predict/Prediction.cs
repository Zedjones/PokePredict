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
            var teamFull = new List<Database.Models.Pokemon>();
            using (var db = new PokePredict.Database.Models.pokedexContext()) 
            {
                var monQuery = Queries.AllPokemon(db);
                team.ForEach(dtoMon => {
                    // Get the specific Pokemon
                    var fullMon = monQuery.Where(pk => pk.Identifier == dtoMon.Name).First();
                    // Only get moves that are in our PokemonDto's list
                    fullMon.PokemonMoves = fullMon.PokemonMoves
                        .Where(move => dtoMon.Moves.Contains(move.Move.Identifier)).ToList();
                    teamFull.Add(fullMon);
                });
            }
            // Get rid of duplicate moves
            Queries.ReduceMoves(teamFull.ToArray());
            return teamFull;
        }
    }
}