using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Store
    {
        private string _name;
        /// <summary>
        /// Id of the store
        /// </summary>
        public int StoreId { get; set; } = 0;
        /// <summary>
        /// Name of the store
        /// </summary>
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
        /// Products available at the store and how much 
        /// </summary>
        public Dictionary<Product, int> Inventory { get; set; } = new Dictionary<Product, int>();
    }
}
