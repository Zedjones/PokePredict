﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ItemFlingEffectProse
    {
        public long ItemFlingEffectId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Effect { get; set; }

        public virtual ItemFlingEffects ItemFlingEffect { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
