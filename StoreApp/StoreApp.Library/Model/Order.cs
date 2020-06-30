using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Order
    {
        private Customer _customer;
        private Store _store;
        private decimal _totalCost = 0;
        public int OrderId { get; set; } = 0;
        public DateTime? OrderDate { get; set; } = null;
        public Customer Customer 
        {
            get => _customer; 
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Orders must have a Customer.");
                }
                _customer = value;
            }
        }
        public Store Store 
        {
            get => _store;
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Orders must have a Store.");
                }
                _store = value;
            }
        }
        public Dictionary<Product, int> OrderLine { get; set; } = new Dictionary<Product, int>();
        public decimal TotalCost 
        { 
            get
            {
                if(OrderLine.Count == 0)
                {
                    throw new Exception("There is no OrderLine for this order yet.");
                }
                foreach (var p in OrderLine.Keys)
                {
                    _totalCost += p.Price * OrderLine[p];
                }
                return _totalCost;
            }
            set => _totalCost = value; 
        }
    }
}
