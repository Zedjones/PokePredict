﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ConquestStatNames
    {
        public long ConquestStatId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual ConquestStats ConquestStat { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
