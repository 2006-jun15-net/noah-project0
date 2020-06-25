using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Library.Repos
{
    public class StoreRepo
    {
        private readonly List<Location> _stores;

        public StoreRepo(List<Location> stores)
        {
            _stores = stores ?? throw new ArgumentNullException(nameof(stores));
        }
        public IEnumerable<Location> GetAllStores()
        {
            foreach(var item in _stores)
            {
                yield return item;
            }
        }

        public Location GetStoreById(int id)
        {
            return _stores.First(s => s.LocationID == id);
        }

        public void AddStore(Location store)
        {
            _stores.Add(store);
        }
        public void DeleteStore(int id)
        {
            _stores.Remove(_stores.First(s => s.LocationID == id));
               
        }
        public void EditStore(Location store)
        {
            DeleteStore(store.LocationID);
            AddStore(store);
        }
        public void CheckInventory(int id)
        {
            var storeToCheck = _stores.First(s => s.LocationID == id);
            Dictionary<Product, int>.KeyCollection productsInStore = storeToCheck.Inventory.Keys;
            Console.WriteLine($"Products in {storeToCheck.Name}:\n");
            foreach(var p in productsInStore)
            {
                Console.WriteLine($"{storeToCheck.Inventory[p]} {p.Name}(s) left.\n");
            }
        }
    }
}
