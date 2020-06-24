using StoreApp.Library.Model;
using StoreApp.Library.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Program = StoreApp.App.Program;

namespace StoreApp.Test
{
    public class UnitTest1
    {
        [Fact]
        public void FileIsBeingWritten()
        {
            //arrange
            var dataList = new List<Order>();
            List<Product> p = new List<Product>
            {
                new Product("x", 100),
                new Product("y", 100),
                new Product("z", 100)

            };
            List<Product> p2 = new List<Product>
            {
                new Product("x", 100),
                new Product("y", 100),
                new Product("z", 100)

            };
            Customer c = new Customer("noah", "funtanilla");
            Order o = new Order
            {
                Products = p,
                CurrentCustomer = c
            };
            Order o2 = new Order
            {
                Products = p2,
                CurrentCustomer = c
            };
            dataList.Add(o);
            dataList.Add(o2);

            OrderHistory oh = new OrderHistory(dataList);

            List<Location> stores = new List<Location>
            {
                new Location
                {
                    Name = "store1",
                    LocationID = 1,
                    Inventory =
                    {
                        { new Product("a", 10.00), 100 },
                        { new Product("b", 20.00), 200 },
                        { new Product("c", 30.00), 300 }
                    }
                },
                new Location
                {
                    Name = "store2",
                    LocationID = 2,
                    Inventory =
                    {
                        { new Product("e", 10.00), 100 },
                        { new Product("f", 20.00), 200 },
                        { new Product("g", 30.00), 300 }
                    }
                }
            };
            StoreRepo storeRepo = new StoreRepo(stores);

            string orderHistoryPath = "../../../../OrderHistory.xml";
            string storesDataPath = "../../../../StoresData.xml";

            //act
            Program.GenerateOrderHistory(oh, orderHistoryPath);
            Program.GenerateStores(storeRepo, storesDataPath);

            //assert
            Assert.True(File.Exists(orderHistoryPath));
            Assert.True(File.Exists(storesDataPath));
        }
       
        [Fact]
        public void FileIsBeingReadCorrectly()
        {
            //arrange
            string orderHistoryPath = "../../../../OrderHistory.xml";
            string storesDataPath = "../../../../StoresData.xml";
            //act

            OrderHistory dataFromOrdersXml = Program.GetInitialData(orderHistoryPath);
            StoreRepo dataFromStoresXml = Program.GetInitialStoreData(storesDataPath);
            
            //assert
            Assert.Equal("x", dataFromOrdersXml.GetOrderHistory().ToList()[0].Products[0].Name);
            Assert.True(dataFromStoresXml is StoreRepo);


        }
    }
}
