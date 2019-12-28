﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class MoveEffectProse
    {
        public long MoveEffectId { get; set; }
        public long LocalLanguageId { get; set; }
        public string ShortEffect { get; set; }
        public string Effect { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual MoveEffects MoveEffect { get; set; }
    }
}
