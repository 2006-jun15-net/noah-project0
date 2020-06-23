using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace StoreApp.Library.Model
{
    [DataContract]
    public class Location
    { 
        public int? LocationID { get; set; }
        public string Name { get; set; }
        [DataMember]
        public Dictionary<Product, int> Inventory { get; set; }

        public Location()
        {
            LocationID = null;
            Name = null;
            Inventory = null;
        }
        public Location(string name, int locationID)
        {
            LocationID = locationID;
            Name = name;
            Inventory = GenerateInventory();
        }
        public void DecreaseInventory(Order order)
        {
         
            
                foreach(Product p in order.Products)
                {
                    Inventory[p] -= 1;
                }
               
            
        }

        private List<Product> GenerateInventoryFile()
        {
            var serializer = new DataContractSerializer(typeof(Location));
            
            using (var writer = new XmlTextWriter(stream, "../../../../Inventories.xml"))
            {
                writer.Formatting = Formatting.Indented; // indent the Xml so it's human readable
                serializer.WriteObject(writer, Inventory);
                writer.Flush();
                
            }
            
        }

    
    }
}
