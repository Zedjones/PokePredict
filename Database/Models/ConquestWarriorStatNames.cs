﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ConquestWarriorStatNames
    {
        public long WarriorStatId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual ConquestWarriorStats WarriorStat { get; set; }
    }
}
