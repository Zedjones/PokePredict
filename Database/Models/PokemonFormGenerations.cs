﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class PokemonFormGenerations
    {
        public long PokemonFormId { get; set; }
        public long GenerationId { get; set; }
        public long GameIndex { get; set; }

        public virtual Generations Generation { get; set; }
        public virtual PokemonForms PokemonForm { get; set; }
    }
}
