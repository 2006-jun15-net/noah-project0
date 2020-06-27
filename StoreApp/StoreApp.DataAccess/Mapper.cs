using StoreApp.DataAccess.Model;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public class Mapper
    {
        public Customers ModelToDbEntity(Customer c)
        {
            return new Customers { FirstName = c.FirstName, LastName = c.LastName };
        }
        public Products ModelToDbEntity(Product p)
        {
            return new Products { ProductName = p.Name, Price = p.Price};
        }
        //public Stores ModelToDbEntity(Store s)
        //{
        //    Stores store = new Stores();
        //    store.StoreName = s.Name;
            
        //}
    }
}
