using StoreApp.DataAccess.Model;

namespace StoreApp.Library.Repos
{
    class ProductRepo : IAddable<Products>, IRemovable<Products>
    {
        public void Add(Products entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Products entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
