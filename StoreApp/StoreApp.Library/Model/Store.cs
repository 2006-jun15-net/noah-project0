using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// Busines model for store class
    /// </summary>
    public class Store
    {
        /// <summary>
        /// Private fields for store name
        /// </summary>
        private string _name;
        //Int value for store id, which uniquely identifies this store, set by default to 0 initially
        public int StoreId { get; set; } = 0;
        //string property for name of store, performs validation when setting name to see if nothing was entered, if so throw an exception
        public string Name 
        {
            get => _name;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Must have a name.", nameof(value));
                }
                _name = value;
            }
        }
        /// <summary>
        /// Dictionary to keep track of the inventory
        /// </summary>
        /// <remarks>
        /// Used to map products to int value representing the quanitiy of that product in stock
        /// </remarks>
        public Dictionary<Product, int> Inventory { get; set; } = new Dictionary<Product, int>();
    }
}
