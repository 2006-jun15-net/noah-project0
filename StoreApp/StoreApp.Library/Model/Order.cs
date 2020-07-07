using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// Business model for the order class
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Private fields for Order info such as customer, store location, and total cost of order(by default, intially 0)
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

        //Order Id that uniquely identifies the order
        public int OrderId { get; set; } = 0;
        //used to get current date time when finalizing the order
        public DateTime? OrderDate { get; set; } = null;
        //Holds customer object, if missing, will throw an exception
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
        //Contains store object for the order being placed, throws exception if the object is null
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
        //Dictonary DS for retrieving all the items, and their quanitites in current order
        public Dictionary<Product, int> OrderLine { get; set; } = new Dictionary<Product, int>();
        //decimal value for total cost of the order, if no orders in cart, then throw an exception, otherwise add up the prices of all orders in orderline
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
