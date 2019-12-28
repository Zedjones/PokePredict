﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class EncounterConditionValueMap
    {
        public long EncounterId { get; set; }
        public long EncounterConditionValueId { get; set; }

        public virtual Encounters Encounter { get; set; }
        public virtual EncounterConditionValues EncounterConditionValue { get; set; }
    }
}
