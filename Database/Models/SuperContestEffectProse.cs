﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class SuperContestEffectProse
    {
        public long SuperContestEffectId { get; set; }
        public long LocalLanguageId { get; set; }
        public string FlavorText { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual SuperContestEffects SuperContestEffect { get; set; }
    }
}
