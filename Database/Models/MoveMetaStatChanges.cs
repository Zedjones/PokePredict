﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class MoveMetaStatChanges
    {
        public long MoveId { get; set; }
        public long StatId { get; set; }
        public long Change { get; set; }

        public virtual Moves Move { get; set; }
        public virtual Stats Stat { get; set; }
    }
}
