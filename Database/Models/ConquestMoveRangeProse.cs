using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ConquestMoveRangeProse
    {
        public long ConquestMoveRangeId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ConquestMoveRanges ConquestMoveRange { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
