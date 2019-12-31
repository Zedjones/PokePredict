using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokePredict.Database.Models
{
    public partial class MoveTargets
    {
        public MoveTargets()
        {
            MoveChangelog = new HashSet<MoveChangelog>();
            MoveTargetProse = new HashSet<MoveTargetProse>();
            Moves = new HashSet<Moves>();
        }

        public long Id { get; set; }
        public string Identifier { get; set; }

        public virtual ICollection<MoveChangelog> MoveChangelog { get; set; }
        public virtual ICollection<MoveTargetProse> MoveTargetProse { get; set; }
        [JsonIgnore]
        public virtual ICollection<Moves> Moves { get; set; }
    }
}
