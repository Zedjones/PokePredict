﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class TypeGameIndices
    {
        public long TypeId { get; set; }
        public long GenerationId { get; set; }
        public long GameIndex { get; set; }

        public virtual Generations Generation { get; set; }
        public virtual Types Type { get; set; }
    }
}
