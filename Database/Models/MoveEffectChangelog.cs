﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class MoveEffectChangelog
    {
        public MoveEffectChangelog()
        {
            MoveEffectChangelogProse = new HashSet<MoveEffectChangelogProse>();
        }

        public long Id { get; set; }
        public long EffectId { get; set; }
        public long ChangedInVersionGroupId { get; set; }

        public virtual VersionGroups ChangedInVersionGroup { get; set; }
        public virtual MoveEffects Effect { get; set; }
        public virtual ICollection<MoveEffectChangelogProse> MoveEffectChangelogProse { get; set; }
    }
}
