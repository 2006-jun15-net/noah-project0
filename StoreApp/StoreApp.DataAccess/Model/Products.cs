using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Products
    {
        public Products()
        {
            ProductsInStores = new HashSet<ProductsInStores>();
            ProductsOfOrders = new HashSet<ProductsOfOrders>();
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ProductsInStores> ProductsInStores { get; set; }
        public virtual ICollection<ProductsOfOrders> ProductsOfOrders { get; set; }
    }
}
