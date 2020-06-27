using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    public class StoreController
    {
       public readonly IRepository<Stores> repository = null;

       public StoreController()
        {
            repository = new GenericRepository<Stores>();
        }
       public StoreController(IRepository<Stores> repo)
        {
            repository = repo;
        }
        
        public void DisplayStores()
        {
            Console.WriteLine("List of Stores:\n");
            foreach(var s in repository.GetAll().ToList())
            {
                Console.WriteLine($"{s.StoreName} ID: {s.StoreId}\n");
            }
        }
    }
}
