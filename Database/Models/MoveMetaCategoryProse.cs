﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class MoveMetaCategoryProse
    {
        public long MoveMetaCategoryId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Description { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual MoveMetaCategories MoveMetaCategory { get; set; }
    }
}
