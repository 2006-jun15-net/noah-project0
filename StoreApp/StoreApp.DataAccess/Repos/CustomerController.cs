using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    public class CustomerController
    {
        public readonly IRepository<Customers> repository = null;

        public CustomerController()
        {
            repository = new GenericRepository<Customers>();
        }
        public CustomerController(IRepository<Customers> repo)
        {
            repository = repo;
        }
        
        public void DisplayCustomers()
        {
            foreach(var c in repository.GetAll().ToList())
            {
                Console.WriteLine($"{c.FirstName} {c.LastName} ID: {c.CustomerId}\n");
            }
        }
        public void DisplayCustomers(int id)
        {
            
        }

      
    }
}
