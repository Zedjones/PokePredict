﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class CharacteristicText
    {
        public long CharacteristicId { get; set; }
        public long LocalLanguageId { get; set; }
        public string Message { get; set; }

        public virtual Characteristics Characteristic { get; set; }
        public virtual Languages LocalLanguage { get; set; }
    }
}
