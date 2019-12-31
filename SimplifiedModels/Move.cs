using System.Collections.Generic;

namespace PokePredict.SimplifiedModels
{
    public class Move
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int Priority { get; set; }
        public string Target { get; set; }
        public List<StatChange> StatChanges { get; set; }
    }
}