using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Products
    {
        public Products()
        {
            Inventory = new HashSet<Inventory>();
            OrderLines = new HashSet<OrderLines>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<OrderLines> OrderLines { get; set; }
    }
}
