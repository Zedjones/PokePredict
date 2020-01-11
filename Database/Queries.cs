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
        static Queries()
        {
            using (var db = new pokedexContext())
            {
                DtoList = NameAndMoves(db).ToList();
            }
        }
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

        public static void ReduceMoves(params Pokemon[] pokeQuery)
        {
            pokeQuery.ToList().ForEach(pk => pk.PokemonMoves =
                pk.PokemonMoves
                .GroupBy(move => move.Move)
                .Select(group => group.First())
                .ToList()
            );
        }
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