using System.Collections.Generic;
using System.Linq;
using PokeApiNet.Models;

namespace PokePredict.Database.Models
{
    public class Move
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int Priority { get; set; }
        public List<StatChange> StatChanges { get; set; }
        public string Target { get; set; }
        public Move(PokeApiNet.Models.Move fromMove)
        {
            var client = new PokeApiNet.PokeApiClient();
            Name = fromMove.Name;
            Power = fromMove.Power;
            Accuracy = fromMove.Accuracy;
            Priority = fromMove.Priority;
            Target = fromMove.Target.Name;

            var changes = fromMove.StatChanges.Select(statChange => new StatChange(statChange, fromMove.Name));
            StatChanges = changes.ToList();

            Type = fromMove.Type.Name;
        }

        public Move()
        {
        }
    }
    public class StatChange
    {
        public string MoveName { get; set; }
        public int Change { get; set; }
        public string Name { get; set; }
        public StatChange(MoveStatChange statChange, string moveName)
        {
            var client = new PokeApiNet.PokeApiClient();
            var statTask = client.GetResourceAsync(statChange.Stat);
            Change = statChange.Change;
            statTask.Wait();
            Name = statTask.Result.Name;
            MoveName = moveName;
        }
        public StatChange()
        {
        }
    }
}