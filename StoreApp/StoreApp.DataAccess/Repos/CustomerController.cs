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
            Console.WriteLine("List of Customers:");
            foreach(var c in repository.GetAll().ToList())
            {
                Console.WriteLine($"Username: {c.UserName} ID: {c.CustomerId}\n");
            }
        }

      
    }
}
