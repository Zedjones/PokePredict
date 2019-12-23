using PokeApiNet.Models;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public class Pokemon
    {
        public List<PokemonStat> Stats { get; set; }
        public List<Type> Types { get; set; }
        public List<Move> Moves { get; set; }
        public int Weight { get; set; }
    }
}