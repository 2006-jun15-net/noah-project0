using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Library.Repos
{
    public class StoreRepo
    {
        private readonly ICollection<Location> _stores;

        public StoreRepo(ICollection<Location> stores)
        {
            _stores = stores ?? throw new ArgumentNullException(nameof(stores));
        }

        public Location GetStoreById(int id)
        {
            return _stores.First(s => s.LocationID == id);
        }

        public void AddStore(Location store)
        {
            _stores.Add(store);
        }
        public void DeleteStore(int? id)
        {
            _stores.Remove(_stores.First(s => s.LocationID == id));
               
        }
        public void EditStore(Location store)
        {
            DeleteStore(store.LocationID);
            AddStore(store);
        }
        public int CheckInventory(int id)
        {
            var storeToCheck = _stores.First(s => s.LocationID == id);

        }
    }
}
