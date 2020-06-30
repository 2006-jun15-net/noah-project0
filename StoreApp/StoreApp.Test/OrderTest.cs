using StoreApp.DataAccess.Model;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    
    public class OrderTest
    {
        Order order = new Order();
        [Fact]
        public void OrderIdShouldDefaultToZero()
        {
            Assert.Equal(0, order.OrderId);
        }
        [Fact]
        public void OrderDateShouldDefaultToNull()
        {
            Assert.Null(order.OrderDate);
        }
        [Fact]
        public void OrderCustomerIsNullShouldThrowException()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => order.Customer = null);
        }
        [Fact]
        public void StoreIsNullShouldThrowException()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => order.Store = null);
        }
        [Fact]
        public void OrderLineIsEmptyInitially()
        {
            Assert.Empty(order.OrderLine);
        }
        [Fact]
        public void TotalCostCalculatesCorrectly()
        {
            //arrange
            decimal randomPrice1 = 100;
            int randomQty1 = 2;
            decimal randomPrice2 = 50;
            int randomQty2 = 3;
            Dictionary<Product, int> ol = new Dictionary<Product, int>();
            ol.Add(new Product { Name = "p1", Price = randomPrice1 }, randomQty1);
            ol.Add(new Product { Name = "p2", Price = randomPrice2 }, randomQty2);
            order.OrderLine = ol;

            //act
            decimal TotalCost = randomPrice1 * randomQty1 + randomPrice2 * randomQty2;

            //assert
            Assert.Equal(TotalCost, order.TotalCost);

        }
        [Fact]
        public void TotalCostThrowsExceptionWhenOrderLineIsEmpty()
        {
            Assert.ThrowsAny<Exception>(() => order.TotalCost);
        }
    }

}
