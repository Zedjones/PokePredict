﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class MoveFlagMap
    {
        public long MoveId { get; set; }
        public long MoveFlagId { get; set; }

        public virtual Moves Move { get; set; }
        public virtual MoveFlags MoveFlag { get; set; }
    }
}
