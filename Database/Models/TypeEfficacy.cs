using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class TypeEfficacy
    {
        public long DamageTypeId { get; set; }
        public long TargetTypeId { get; set; }
        public long DamageFactor { get; set; }

        public virtual Types DamageType { get; set; }
        public virtual Types TargetType { get; set; }
    }
}
