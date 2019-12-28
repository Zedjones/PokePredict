﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ConquestWarriorSpecialties
    {
        public long WarriorId { get; set; }
        public long TypeId { get; set; }
        public long Slot { get; set; }

        public virtual Types Type { get; set; }
        public virtual ConquestWarriors Warrior { get; set; }
    }
}
