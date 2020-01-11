using System;
using PokePredict.Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PokePredict.Database
{
    public class PokemonDto
    {
        public string Name;
        public List<string> Moves;
    }
    public static class Queries
    {
        public readonly static List<PokemonDto> DtoList;
        /// <summary>
        /// Static constructor to set our full list of PokemonDto objects and store
        /// it in memory since we'll be accessing it often
        /// </summary>
        static Queries()
        {
            using (var db = new pokedexContext())
            {
                DtoList = NameAndMoves(db).ToList();
            }
        }
        /// <summary>
        /// Return all Pokemon (as a query) with the relevant information that we need
        /// NOTE: We need to return a query because converting to an IEnumerable would
        /// use all of our memory (or at least, a LOT of it)
        /// </summary>
        /// <param name="db">A database context for Pokemon</param>
        /// <returns>An IQueryable for all Pokemon with all pertinent information</returns>
        public static Func<pokedexContext, IQueryable<Pokemon>> AllPokemon =
            (pokedexContext db) => db.Pokemon
                .Include(pk => pk.Species)
                //Include all moves
                .Include(pk => pk.PokemonMoves)
                .ThenInclude(move => move.Move.Target)
                .Include(pk => pk.PokemonMoves)
                .ThenInclude(move => move.Move.Type)
                .Include(pk => pk.PokemonMoves)
                .ThenInclude(move => move.Move.MoveMetaStatChanges)
                .ThenInclude(change => change.Stat)
                //Include all stats
                .Include(pk => pk.PokemonStats)
                .ThenInclude(stat => stat.Stat)
                .Include(pk => pk.PokemonTypes);
        /// <summary>
        /// Get rid of duplicate moves for each Pokemon.
        /// A duplicate move, in this context, is one that has the same inner move
        /// but a different version ID
        /// </summary>
        /// <param name="pokeQuery">1 or more Pokemon to remove duplicate moves for</param>
        public static void ReduceMoves(params Pokemon[] pokeQuery)
        {
            pokeQuery.ToList().ForEach(pk => pk.PokemonMoves =
                pk.PokemonMoves
                .GroupBy(move => move.Move)
                .Select(group => group.First())
                .ToList()
            );
        }
        /// <summary>
        /// Get a full list of PokemonDto objects (Pokemon names and move names)
        /// </summary>
        /// <param name="db">A database context for Pokemon</param>
        /// <returns>A list of objects containing Pokemon names and list of moves</returns>
        private static Func<pokedexContext, IEnumerable<PokemonDto>> NameAndMoves =
            EF.CompileQuery((pokedexContext db) => db.Pokemon
                .Include(pk => pk.PokemonMoves)
                .ThenInclude(move => move.Move)
                .Select(pk => new PokemonDto
                {
                    Name = pk.Identifier,
                    Moves = pk.PokemonMoves.Select(mv => mv.Move.Identifier).ToList()
                }));
    }
}