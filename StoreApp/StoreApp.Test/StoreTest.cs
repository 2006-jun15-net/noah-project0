using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class StoreTest
    {
        Store store = new Store();
        [Fact]
        public void StoreIdDefaultsToZero()
        {
            Assert.Equal(0, store.StoreId);
        }

        [Fact]
        public void StoreNameIsEmptyThrowsException()
        {
            Assert.ThrowsAny<ArgumentException>(() => store.Name = "");
        }
        
        [Fact]
        public void StoreInventoryIsInitiallyEmpty()
        {
            Assert.Empty(store.Inventory);
        }

    }
}
