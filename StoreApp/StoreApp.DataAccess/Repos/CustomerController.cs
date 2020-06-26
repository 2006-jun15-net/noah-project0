using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;

namespace StoreApp.DataAccess.Repos
{
    public class CustomerController
    {
        public readonly IRepository<Customer> repository = null;

        public CustomerController()
        {
            repository = new GenericRepository<Customer>();
        }
        public CustomerController(IRepository<Customer> repo)
        {
            repository = repo;
        }

        public void DisplayCustomers()
        {
            
        }
        public void DisplayCustomers(int id)
        {
            
        }

      
    }
}
