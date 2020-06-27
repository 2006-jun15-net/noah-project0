using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Orders
    {
        public Orders()
        {
            OrderLines = new HashSet<OrderLines>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Amount { get; set; }
        public decimal TotalCost { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Stores Store { get; set; }
        public virtual ICollection<OrderLines> OrderLines { get; set; }
    }
}
