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
            foreach (var c in repository.GetAll().ToList())
            {
                Console.WriteLine($"Username: {c.UserName} ID: {c.CustomerId}\n");
            }
        }

        public Customers SearchCustomerByUsername(string username)
        {
            if (repository.GetAll().Any(c => c.UserName.Equals(username)))
            {
                Customers customer = repository.GetAll().First(c => c.UserName.Equals(username));
                Console.WriteLine($"Username: {customer.UserName}");
                return customer;
            }
            else
            {
                Console.WriteLine($"No customers exist by username: {username}");
                return null;
            }

        }
    }   

    
}
