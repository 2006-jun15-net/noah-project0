using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Store
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public Dictionary<Product, int> Inventory {get; set;}
    }
}
