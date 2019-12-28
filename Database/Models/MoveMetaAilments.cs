using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class MoveMetaAilments
    {
        public MoveMetaAilments()
        {
            MoveMeta = new HashSet<MoveMeta>();
            MoveMetaAilmentNames = new HashSet<MoveMetaAilmentNames>();
        }

        public long Id { get; set; }
        public string Identifier { get; set; }

        public virtual ICollection<MoveMeta> MoveMeta { get; set; }
        public virtual ICollection<MoveMetaAilmentNames> MoveMetaAilmentNames { get; set; }
    }
}
