using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Product
    {
        private string _name;
        private decimal _price;
        public int ProductId { get; set; } = 0;
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
