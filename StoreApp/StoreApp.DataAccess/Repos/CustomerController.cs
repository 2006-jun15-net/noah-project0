using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    /// <summary>
    /// This class controls the repository for the Customers table in the database and provides other methods related to Customers
    /// </summary>
    public class CustomerController
    {
        /// <summary>
        /// Repository for handling DML operations for the Customers table
        /// </summary>
        public readonly IRepository<Customers> repository = null;

        /// <summary>
        /// Initialize the a repository
        /// </summary>
        public CustomerController()
        {
            repository = new GenericRepository<Customers>();
        }
        public CustomerController(IRepository<Customers> repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Print the list of all customers in the Customers table
        /// </summary>
        public void DisplayCustomers()
        {
            Console.WriteLine("List of Customers:");
            foreach (var c in repository.GetAll().ToList())
            {
                Console.WriteLine($"Username: {c.UserName} ID: {c.CustomerId}\n");
            }
        }

        /// <summary>
        /// Searches for the username of a customer in the Customers table and returns that customer
        /// </summary>
        /// <param name="username">The username of the customer</param>
        /// <returns>The Customers entity corresponding to the given username</returns>
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
