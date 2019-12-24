using PokeApiNet.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokePredict.Database.Models
{
    public class Pokemon
    {
        public List<Stat> Stats { get; set; } = new List<Stat>();
        public List<Type> Types { get; set; }
        public List<Move> Moves { get; set; }
        public int Weight { get; set; }
        public Pokemon(PokeApiNet.Models.Pokemon fromMon)
        {
            var client = new PokeApiNet.PokeApiClient();

            var fullMoves = client.GetResourceAsync(fromMon.Moves.Select(move => move.Move));
            var fullTypes = client.GetResourceAsync(fromMon.Types.Select(type => type.Type));
            var fullStats = client.GetResourceAsync(fromMon.Stats.Select(stat => stat.Stat));

            fullStats.Wait();
            for (var i = 0; i < fullStats.Result.Count; i++)
            {
                Stats.Add(new Stat
                {
                    Effort = fromMon.Stats[i].Effort,
                    BaseStat = fromMon.Stats[i].BaseStat,
                    Name = fullStats.Result[i].Name
                });
            }

            fullMoves.Wait();
            var myMoves = fullMoves.Result.Select(fullMove => new Move(fullMove));
            Moves = myMoves.ToList();

            fullTypes.Wait();
            Types = fullTypes.Result;

            Weight = fromMon.Weight;
        }
    }
    public class Stat
    {
        public int Effort { get; set; }
        public int BaseStat { get; set; }
        public string Name { get; set; }
    }
}