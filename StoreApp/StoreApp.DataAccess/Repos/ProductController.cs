using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    /// <summary>
    /// This class controls the repository for the Products table in the database and provides other methods specific to Products
    /// </summary>
    public class ProductController
    {
        /// <summary>
        /// The repository that handles DML operations for Products
        /// </summary>
        public readonly IRepository<Products> repository = null;

        /// <summary>
        /// Initialize the repository for Products
        /// </summary>
        public ProductController()
        {
            repository = new GenericRepository<Products>();
        }
        public ProductController(IRepository<Products> repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Prints out a list of all products in the Products table
        /// </summary>
        public void DisplayProducts()
        {
            Console.WriteLine("List of Products:\n");
            foreach(var p in repository.GetAll().ToList())
            {
                Console.WriteLine($"Product Name: {p.ProductName} ID: {p.ProductId}\n");
            }
        }
    }
}
