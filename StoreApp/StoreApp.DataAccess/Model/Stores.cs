using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class Stores
    {
        public Stores()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
