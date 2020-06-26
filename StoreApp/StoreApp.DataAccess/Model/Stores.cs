using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Stores
    {
        public Stores()
        {
            ProductsInStores = new HashSet<ProductsInStores>();
        }

        public int StoreId { get; set; }
        public int OrderId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual ICollection<ProductsInStores> ProductsInStores { get; set; }
    }
}
