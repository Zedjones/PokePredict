﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ItemNames
    {
        public long ItemId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual Items Item { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
