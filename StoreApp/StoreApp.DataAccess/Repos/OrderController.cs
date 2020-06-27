using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;

namespace StoreApp.DataAccess.Repos
{
    public class OrderController
    {
        public readonly IRepository<Orders> repository = null;

        public OrderController()
        {
            repository = new GenericRepository<Orders>();
        }
        public OrderController(IRepository<Orders> repo)
        {
            repository = repo;
        }
    }



}
