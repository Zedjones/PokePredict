﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class EvolutionTriggerProse
    {
        public long EvolutionTriggerId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual EvolutionTriggers EvolutionTrigger { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
