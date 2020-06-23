using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Location
    { 
        public int LocationID { get; set; }
        public string Name { get; set; }
        public Dictionary<Product, int> Inventory { get; set; }

        public Location(string name, int locationID)
        {
            LocationID = locationID;
            Name = name;
            Inventory = GenerateInventory();
        }
        public void DecreaseInventory(Order order)
        {
         
            
                foreach(Product p in order.Products)
                {
                    Inventory[p] -= 1;
                }
               
            
        }

        private Dictionary<Product, int> GenerateInventory()
        {
            Dictionary<Product, int> inventory = new Dictionary<Product, int>();
            
            Product p1 = new Product("Guitar", 500.00);
            Product p2 = new Product("Piano", 1000.00);
            Product p3 = new Product("Drums", 1500.00);
            inventory.Add(p1,100);
            inventory.Add(p2,100);
            inventory.Add(p3,100);
           
            return inventory;
        }

        public string GetProductNames()
        {
            //get the inventory product names
            Dictionary<Product, int>.KeyCollection products = Inventory.Keys;
            string productNames = "";
            int count = 0;

            foreach (Product p in products)
            {
                count++;
                productNames += $"{count}. {p.Name}\n";
            }
            return productNames;
        }
    }
}
