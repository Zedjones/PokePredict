using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class PokemonEggGroups
    {
        public long SpeciesId { get; set; }
        public long EggGroupId { get; set; }

        public virtual EggGroups EggGroup { get; set; }
        public virtual PokemonSpecies Species { get; set; }
    }
}
