﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class EncounterConditionValueProse
    {
        public long EncounterConditionValueId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual EncounterConditionValues EncounterConditionValue { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
