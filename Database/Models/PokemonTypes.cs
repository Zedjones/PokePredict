﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class PokemonTypes
    {
        public long PokemonId { get; set; }
        public long TypeId { get; set; }
        public long Slot { get; set; }

        public virtual Pokemon Pokemon { get; set; }
        public virtual Types Type { get; set; }
    }
}
