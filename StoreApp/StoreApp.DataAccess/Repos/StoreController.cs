using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;

namespace StoreApp.DataAccess.Repos
{
    public class StoreController
    {
       public readonly IRepository<Store> repository = null;

       public StoreController()
        {
            repository = new GenericRepository<Store>();
        }
       public StoreController(IRepository<Store> repo)
        {
            repository = repo;
        }
        
    }
}
