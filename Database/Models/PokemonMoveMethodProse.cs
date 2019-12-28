﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class PokemonMoveMethodProse
    {
        public long PokemonMoveMethodId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual PokemonMoveMethods PokemonMoveMethod { get; set; }
    }
}
