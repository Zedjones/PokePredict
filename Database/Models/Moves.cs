using System.Collections.Generic;
using PokeApiNet.Models;

namespace PokePredict.Database.Models
{
    public class Move
    {
        public List<Type> Types { get; set; }
        public string Name { get; set; }
        public List<Names> Names { get; set; }
        public int Power { get; set; }
        public int Priority { get; set; }
        public int Accuracy { get; set; }
        public List<StatChange> StatChanges { get; set; }
        public string Target { get; set; }
    }
    public class StatChange
    {
        public int Change { get; set; }
        public Stat Stat { get; set; }
    }
}