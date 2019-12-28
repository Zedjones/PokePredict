﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class VersionGroups
    {
        public VersionGroups()
        {
            AbilityChangelog = new HashSet<AbilityChangelog>();
            AbilityFlavorText = new HashSet<AbilityFlavorText>();
            EncounterSlots = new HashSet<EncounterSlots>();
            ItemFlavorText = new HashSet<ItemFlavorText>();
            Machines = new HashSet<Machines>();
            MoveChangelog = new HashSet<MoveChangelog>();
            MoveEffectChangelog = new HashSet<MoveEffectChangelog>();
            MoveFlavorText = new HashSet<MoveFlavorText>();
            PokedexVersionGroups = new HashSet<PokedexVersionGroups>();
            PokemonForms = new HashSet<PokemonForms>();
            PokemonMoves = new HashSet<PokemonMoves>();
            VersionGroupPokemonMoveMethods = new HashSet<VersionGroupPokemonMoveMethods>();
            VersionGroupRegions = new HashSet<VersionGroupRegions>();
            Versions = new HashSet<Versions>();
        }

        public long Id { get; set; }
        public string Identifier { get; set; }
        public long GenerationId { get; set; }
        public long? Order { get; set; }

        public virtual Generations Generation { get; set; }
        public virtual ICollection<AbilityChangelog> AbilityChangelog { get; set; }
        public virtual ICollection<AbilityFlavorText> AbilityFlavorText { get; set; }
        public virtual ICollection<EncounterSlots> EncounterSlots { get; set; }
        public virtual ICollection<ItemFlavorText> ItemFlavorText { get; set; }
        public virtual ICollection<Machines> Machines { get; set; }
        public virtual ICollection<MoveChangelog> MoveChangelog { get; set; }
        public virtual ICollection<MoveEffectChangelog> MoveEffectChangelog { get; set; }
        public virtual ICollection<MoveFlavorText> MoveFlavorText { get; set; }
        public virtual ICollection<PokedexVersionGroups> PokedexVersionGroups { get; set; }
        public virtual ICollection<PokemonForms> PokemonForms { get; set; }
        public virtual ICollection<PokemonMoves> PokemonMoves { get; set; }
        public virtual ICollection<VersionGroupPokemonMoveMethods> VersionGroupPokemonMoveMethods { get; set; }
        public virtual ICollection<VersionGroupRegions> VersionGroupRegions { get; set; }
        public virtual ICollection<Versions> Versions { get; set; }
    }
}
