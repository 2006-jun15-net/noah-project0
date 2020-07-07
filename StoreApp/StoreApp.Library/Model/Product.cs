using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// Business model for the product class
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Fields for name and price of the product
        /// </summary>
        private string _name;
        private decimal _price;
        //Product ID to uniquely identify a product, by default set to 0
        public int ProductId { get; set; } = 0;
        //Name property, checks to see if length of name is greater than 0, if not then throw an exception because product must have a name
        public string Name 
        {
            get => _name;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Must have a name", nameof(value));
                }
                _name = value;
            }
                
        }
        //decimal property for price of product, checks to see the value cannot be negative, otherwise throw an exception
        public decimal Price 
        {
            get => _price;
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentException("Can't be zero or negative.", nameof(value));
                }
                _price = value;
            }
        }

    }
}
