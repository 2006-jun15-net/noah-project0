using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Order
    {
        private static int orderIDSeed = 0;

        private List<Product> _products = null;
        
        public List<Product> Products { get; set; }
        public int OrderID {get;}
        public Location StoreLocation { get; set;}

        public Customer CurrentCustomer { get; }

        private string orderDate = "";
        
        public Order()
        {
            OrderID = orderIDSeed++;
        }

        public void AddToOrder(Product p)
        {
            if (_products.Count >= 100)
            {
                throw new ArgumentOutOfRangeException(nameof(_products), "Too many products in order.");
            }
            else
            {
                _products.Add(p);
            }
            
        }

        public void SubmitOrder(Location store)
        {
            StoreLocation = store;
            orderDate = DateTime.Now.ToString("F");
            CurrentCustomer.OrderHistory.Add(this);
            
        }

    }
}
