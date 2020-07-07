using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Product
    {
        private string _name;
        private decimal _price;
        /// <summary>
        /// Id of the product
        /// </summary>
        public int ProductId { get; set; } = 0;
        /// <summary>
        /// Name of the product
        /// </summary>
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
        /// <summary>
        /// Price of the product
        /// </summary>
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
