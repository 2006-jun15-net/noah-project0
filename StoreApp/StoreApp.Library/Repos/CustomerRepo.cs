using StoreApp.DataAccess.Model;
using System;
using System.Collections.Generic;

namespace StoreApp.Library.Repos
{
    class CustomerRepo:IAddable<Customers>,IRemovable<Customers>
    {
        private readonly ICollection<Customers> _customers;

        public CustomerRepo(ICollection<Customers> customers)
        {
            _customers = customers;
        }

        public void Add(Customers entity)
        {
            throw new NotImplementedException();
        }



        public void Remove(Customers entity)
        {
            throw new NotImplementedException();
        }
    }
}
