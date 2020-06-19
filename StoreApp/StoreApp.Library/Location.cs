using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    class Location
    {
        public string Name { get; set; }
        public List<Product> Inventory { get; set; }

        public Location(string name, List<Product> inventory)
        {
            Name = name;
            Inventory = inventory;
        }
        public void DecreaseInventory(Order order)
        {
            if (order.OrderAccepted)
            {
                foreach(Product p in order.Products)
                {
                    Inventory.Remove(p);
                }
               
            }
        }


    }
}
