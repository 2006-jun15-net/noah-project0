using StoreApp.Library.Model;
using StoreApp.Library.Repos;
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
                new Product("p1", 100),
                new Product("p2", 100),
                new Product("p3", 100)

            };
            List<Product> p2 = new List<Product>
            {
                new Product("p1", 100),
                new Product("p2", 100),
                new Product("p3", 100)

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

            //act
            Program.GenerateOrderHistory(oh, "../../../../OrderHistory.json");

            //assert

            Assert.True(File.Exists("../../../../OrderHistory.json"));
        }
       
        [Fact]
        public void FileIsBeingReadCorrectly()
        {
            //arrange
            
            //act
            
            OrderHistory dataFromJson = Program.GetInitialData("../../../../OrderHistory.json");


            //assert
            Assert.Equal("p1", dataFromJson.GetOrderHistory().ToList()[0].Products[0].Name);
           

        }
    }
}
