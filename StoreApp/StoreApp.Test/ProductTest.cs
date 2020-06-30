using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class ProductTest
    {
        Product product = new Product();

        [Fact]
        public void ProductNameIsEmptyThrowsException()
        {
            Assert.ThrowsAny<ArgumentException>(() => product.Name = "");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-3)]
        public void ProductPriceIsZeroOrNegativeThrowsException(decimal price)
        {
            Assert.ThrowsAny<ArgumentException>(() => product.Price = price);
        }

        [Fact]
        public void ProductNameStoresCorrectly()
        {
            //arrange
            string randomName = "sdtst";
            //act
            product.Name = randomName;
            //assert
            Assert.Equal(randomName, product.Name);
        }
        [Fact]
        public void ProductPriceStoresCorrectly()
        {
            //arrange
            decimal randomValue = 1.99M;
            //act
            product.Price = randomValue;
            //assert
            Assert.Equal(randomValue, product.Price);
        }

        [Fact]
        public void ProductIdDefaultsToZero()
        {
            Assert.Equal(0, product.ProductId);
        }
    }
}
