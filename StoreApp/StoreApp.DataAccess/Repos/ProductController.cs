using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;

namespace StoreApp.DataAccess.Repos
{
    public class ProductController
    {
        public readonly IRepository<Product> repository = null;

        public ProductController()
        {
            repository = new GenericRepository<Product>();
        }
        public ProductController(IRepository<Product> repo)
        {
            repository = repo;
        }
    }
}
