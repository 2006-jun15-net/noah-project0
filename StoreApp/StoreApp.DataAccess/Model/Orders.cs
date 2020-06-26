using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Orders
    {
        public Orders()
        {
            ProductsOfOrders = new HashSet<ProductsOfOrders>();
            Stores = new HashSet<Stores>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Amount { get; set; }
        public decimal TotalCost { get; set; }
        public int CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<ProductsOfOrders> ProductsOfOrders { get; set; }
        public virtual ICollection<Stores> Stores { get; set; }
    }
}
