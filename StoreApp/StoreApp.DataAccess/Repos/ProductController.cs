using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    public class ProductController
    {
        public readonly IRepository<Products> repository = null;

        public ProductController()
        {
            repository = new GenericRepository<Products>();
        }
        public ProductController(IRepository<Products> repo)
        {
            repository = repo;
        }

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
