using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class ProductsInStores
    {
        public int StoreId { get; set; }
        public Guid ProductId { get; set; }
        public int Inventory { get; set; }

        public virtual Products Product { get; set; }
        public virtual Stores Store { get; set; }
    }
}
