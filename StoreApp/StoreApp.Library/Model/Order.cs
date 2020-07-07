using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Order
    {
        /// <summary>
        /// Customer of the order
        /// </summary>
        private Customer _customer;
        /// <summary>
        /// Store that the order was placed at
        /// </summary>
        private Store _store;
        /// <summary>
        /// Total cost of the order
        /// </summary>
        private decimal _totalCost = 0;
        /// <summary>
        /// Id of the order
        /// </summary>
        public int OrderId { get; set; } = 0;
        /// <summary>
        /// Date of the order
        /// </summary>
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
        /// <summary>
        /// Products in the order and their quantities
        /// </summary>
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
