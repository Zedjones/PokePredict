﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ConquestPokemonAbilities
    {
        public long PokemonSpeciesId { get; set; }
        public long Slot { get; set; }
        public long AbilityId { get; set; }

        public virtual Abilities Ability { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }
    }
}
