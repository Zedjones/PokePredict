﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ConquestWarriorRankStatMap
    {
        public long WarriorRankId { get; set; }
        public long WarriorStatId { get; set; }
        public long BaseStat { get; set; }

        public virtual ConquestWarriorRanks WarriorRank { get; set; }
        public virtual ConquestWarriorStats WarriorStat { get; set; }
    }
}
