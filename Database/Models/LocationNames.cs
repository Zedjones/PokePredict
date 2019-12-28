using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class LocationNames
    {
        public long LocationId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }

        public virtual Languages LocalLanguage { get; set; }
        public virtual Locations Location { get; set; }
    }
}
