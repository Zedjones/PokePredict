using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace PokePredict.Database.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public List<Stat> Stats { get; set; } = new List<Stat>();
        public List<string> Types { get; set; }
        public List<Move> Moves { get; set; }
        public int Weight { get; set; }
        private string WritePath;
        public Pokemon(PokeApiNet.Models.Pokemon fromMon, string basePath)
        {
            var client = new PokeApiNet.PokeApiClient();
            WritePath = Path.Combine(basePath, "Pokemon", fromMon.Name + ".json");

            var fullMoves = client.GetResourceAsync(fromMon.Moves.Select(move => move.Move));
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

            var myMoves = new ConcurrentStack<Move>();
            Parallel.ForEach(fullMoves.Result, fullMove => {
                var moveTask = new Move(fullMove, basePath);
                myMoves.Push(moveTask);
            });
            Moves = myMoves.ToList();

            Types = fromMon.Types.Select(type => type.Type.Name).ToList();

            Weight = fromMon.Weight;
            Name = fromMon.Name;
        }

        public void WriteOut()
        {
            var dirName = Path.GetDirectoryName(WritePath);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            var monStr = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(WritePath, monStr);
        }
    }
    public class Stat
    {
        public int Effort { get; set; }
        public int BaseStat { get; set; }
        public string Name { get; set; }
    }
}