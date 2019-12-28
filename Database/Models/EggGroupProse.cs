﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class EggGroupProse
    {
        public long EggGroupId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual EggGroups EggGroup { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
