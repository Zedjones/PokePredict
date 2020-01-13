using PokePredict.Database;
using PokePredict.Database.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
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
        public static List<Pokemon> DtoToFull(List<PokemonDto> team)
        {
            var teamFull = new List<Pokemon>();
            using (var db = new pokedexContext())
            {
                var monQuery = Queries.AllPokemon(db);
                team.ForEach(dtoMon =>
                {
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

        public static Pokemon MatchPokemon(Pokemon poke)
        {
            return null;
        }

        public static Dictionary<string, long> GetDamageDone(Pokemon poke, ILogger<Controllers.PokemonController> logger, pokedexContext db)
        {
            var damageDone = new Dictionary<string, long>();
            foreach (var type in poke.PokemonTypes)
            {
                var actualType = type.Type;
                db.Entry(actualType)
                    .Collection(type => type.TypeEfficacyDamageType)
                    .Load();
                foreach (var damageRelation in actualType.TypeEfficacyDamageType)
                {
                    db.Entry(damageRelation)
                        .Reference(relation => relation.TargetType)
                        .Load();
                    logger.LogInformation(damageRelation.DamageFactor.ToString());
                    var targetIden = damageRelation.TargetType.Identifier;
                    if(!damageDone.ContainsKey(targetIden)) {
                        damageDone[targetIden] = damageRelation.DamageFactor;
                    }
                    else {
                        damageDone[targetIden] += damageRelation.DamageFactor;
                    } 
                }
            }
            return damageDone;
        }
    }
}