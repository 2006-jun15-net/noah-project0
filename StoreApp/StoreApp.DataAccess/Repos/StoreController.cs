using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    /// <summary>
    /// This class controls the repository for the Stores table in the database and provides other methods specific to Stores
    /// </summary>
    public class StoreController
    {
        /// <summary>
        /// The repository that handles DML operations for Stores
        /// </summary>
       public readonly IRepository<Stores> repository = null;

        /// <summary>
        /// Initialize the repository for Stores
        /// </summary>
       public StoreController()
        {
            repository = new GenericRepository<Stores>();
        }
       public StoreController(IRepository<Stores> repo)
        {
            repository = repo;
        }
        
        /// <summary>
        /// Prints out a list of all the stores in the Stores table
        /// </summary>
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
