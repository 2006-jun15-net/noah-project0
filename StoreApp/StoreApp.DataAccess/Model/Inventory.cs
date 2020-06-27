using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Inventory
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual Products Product { get; set; }
        public virtual Stores Store { get; set; }
    }
}
