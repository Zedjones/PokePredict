using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PokePredict.Database.Models
{
    public partial class MoveMetaStatChanges
    {
        public long MoveId { get; set; }
        public long StatId { get; set; }
        public long Change { get; set; }
        [JsonIgnore]
        public virtual Moves Move { get; set; }
        public virtual Stats Stat { get; set; }
    }
}
