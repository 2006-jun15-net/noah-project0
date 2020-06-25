﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StoreApp.Library.Model
{
    [DataContract]
    [KnownType(typeof(Dictionary<Product, int>))]
    public class Location
    {
        [DataMember]
        private static int _locationIDSeed = 1;
        [DataMember]
        public int LocationID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Dictionary<Product, int> Inventory { get; set; }

        public Location()
        {
            LocationID = 0;
            Name = " ";
            Inventory = new Dictionary<Product, int>();
        }
        public Location(string name, Dictionary<Product, int> inventory)
        {
            LocationID = _locationIDSeed;
            _locationIDSeed++;
            Name = name;
            Inventory = inventory;
        }
        public void DecreaseInventory(Order order)
        {
         
            
                foreach(Product p in order.Products)
                {
                    Inventory[p] -= 1;
                }
               
            
        }

       

    
    }
}
