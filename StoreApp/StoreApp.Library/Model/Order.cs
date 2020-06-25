using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Order
    {
        private static int orderIDSeed = 1;

        private List<Product> _products = null;
        
        public List<Product> Products { get; set; }
        public int OrderID {get;}

        public Customer CurrentCustomer { get; set; }

        private DateTime orderDate;
        
        public Order()
        {
            OrderID = orderIDSeed;
            orderIDSeed++;
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

        public void SubmitOrder()
        {
            orderDate = DateTime.Now;
            
        }
        public override string ToString()
        {
            return $"OrderId: {OrderID}, Products: {Products}, Customer: {CurrentCustomer}, OrderDate: {orderDate}";
        }

    }
}
