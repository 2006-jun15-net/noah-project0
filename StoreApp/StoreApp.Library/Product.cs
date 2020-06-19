using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    class Product
    {
        private double _price;
        public string Name { get; set; }

        public double Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "price can't be nonpositive.");
                }
                _price = value;
            }
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;

        }
  }
}
