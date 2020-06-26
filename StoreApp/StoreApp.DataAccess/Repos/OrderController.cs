using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;

namespace StoreApp.DataAccess.Repos
{
    public class OrderController
    {
        public readonly IRepository<Order> repository = null;

        public OrderController()
        {
            repository = new GenericRepository<Order>();
        }
        public OrderController(IRepository<Order> repo)
        {
            repository = repo;
        }
    }



}
