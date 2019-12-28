﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class LocationAreaProse
    {
        public long LocationAreaId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual LocationAreas LocationArea { get; set; }
    }
}
