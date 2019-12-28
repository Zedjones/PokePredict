﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ContestEffectProse
    {
        public long ContestEffectId { get; set; }
        public long LocalLanguageId { get; set; }
        public string FlavorText { get; set; }
        public string Effect { get; set; }

        public virtual ContestEffects ContestEffect { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
