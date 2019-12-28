﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class AbilityChangelogProse
    {
        public long AbilityChangelogId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Effect { get; set; }

        public virtual AbilityChangelog AbilityChangelog { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
