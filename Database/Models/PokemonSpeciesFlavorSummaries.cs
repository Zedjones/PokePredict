using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class PokemonSpeciesFlavorSummaries
    {
        public long PokemonSpeciesId { get; set; }
        public long LocalLanguageId { get; set; }
        public string FlavorSummary { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual PokemonSpecies PokemonSpecies { get; set; }
    }
}
