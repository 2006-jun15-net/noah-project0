using StoreApp.Library;
using System.Collections.Generic;
using System.IO;
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
            List<Product> p = new List<Product>
            {
                new Product("p1", 100),
                new Product("p2", 100),
                new Product("p3", 100)

            };

            Order o = new Order
            {
                Products = p
            };
            Customer c = new Customer("noah", "funtanilla");
            c.OrderHistory.Add(o);

            //act
            Program.GenerateOrderHistory(c.OrderHistory);

            //assert

            Assert.True(File.Exists("../../../../OrderHistory.json"));
        }
       
        [Fact]
        public void FileIsBeingReadCorrectly()
        {
            //arrange
            
            //act
            List<Order> dataFromJson = Program.GetInitialData();

            //assert
            Assert.Single(dataFromJson);
           

        }
    }
}
