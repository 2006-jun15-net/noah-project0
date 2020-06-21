using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Order
    {
        private static int orderIDSeed = 0;

        private List<Product> _products;

        public int OrderID {get;}
        public Location StoreLocation { get; set;}

        public Customer customer { get; }

        public bool OrderAccepted => false;

        public string OrderDate 
        {
            get
            {
                if (OrderAccepted)
                {
                    return DateTime.Now.ToString("F");
                }
                else
                {
                    return "order failed";
                }

            }
        }

        public List<Product> Products 
        { 
            get => _products; 
            set
            {
                if(value.Count > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Too many products in order.");
                }
                _products = value;
            }
        }
        
        public Order()
        {
            OrderID = orderIDSeed++;
        }

    }
}
