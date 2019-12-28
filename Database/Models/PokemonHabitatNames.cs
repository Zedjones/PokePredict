﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class PokemonHabitatNames
    {
        public long PokemonHabitatId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual PokemonHabitats PokemonHabitat { get; set; }
    }
}
