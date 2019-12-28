﻿using System;
using System.Collections.Generic;

namespace PokePredict.Database.Models
{
    public partial class ItemPockets
    {
        public ItemPockets()
        {
            ItemCategories = new HashSet<ItemCategories>();
            ItemPocketNames = new HashSet<ItemPocketNames>();
        }

        public long Id { get; set; }
        public string Identifier { get; set; }

        public virtual ICollection<ItemCategories> ItemCategories { get; set; }
        public virtual ICollection<ItemPocketNames> ItemPocketNames { get; set; }
    }
}
