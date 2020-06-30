using StoreApp.Library.Model;
using System;
using Xunit;

namespace StoreApp.Test
{
    public class CustomerTest
    {
        Customer customer = new Customer();
        [Fact]
        public void CustomerStoresNamesCorrectly()
        {
            //arrange
            
            string randomValue = "asdfa";
            //act
            customer.FirstName = randomValue;
            customer.LastName = randomValue;
            customer.UserName = randomValue;


            //assert
            Assert.Equal(randomValue, customer.FirstName );
            Assert.Equal(randomValue, customer.LastName );
            Assert.Equal(randomValue, customer.UserName);

        }
        [Fact]
        public void NameThrowsExceptionIfNull()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.FirstName = "");
            Assert.ThrowsAny<ArgumentException>(() => customer.LastName = "");
            Assert.ThrowsAny<ArgumentException>(() => customer.UserName = "");
        }

        [Fact]
        public void OrderHistoryDefaultsToEmpty()
        {
            Assert.Empty(customer.OrderHistory);
        }

        [Fact]
        public void IdDefaultsToZero()
        {
            Assert.Equal(0, customer.CustomerId);
        }
    }
}
