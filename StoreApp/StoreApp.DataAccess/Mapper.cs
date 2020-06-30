using StoreApp.DataAccess.Model;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public static class Mapper
    {
        public static Customers ModelToEntity(Customer c)
        {
            return new Customers { FirstName = c.FirstName, LastName = c.LastName };
        }
        public static Products ModelToEntity(Product p)
        {
            return new Products { ProductName = p.Name, Price = p.Price};
        }
        //public static Stores ModelToEntity(Store s)
        //{
        //    Stores store = new Stores();
        //    store.StoreName = s.Name;

        //}
    }
}
