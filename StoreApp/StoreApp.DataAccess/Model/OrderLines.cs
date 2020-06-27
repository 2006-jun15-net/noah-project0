using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Model
{
    public partial class OrderLines
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
