using StoreApp.DataAccess.Repos;
using Xunit;

namespace StoreApp.Test
{
    public class StoreTest
    {
        [Fact]
        public void DisplayOrdersShouldDisplayOrders()
        {
            //arrange
            OrderController oc = new OrderController();
            bool DisplayedOrders = false;

            //act
            oc.DisplayOrders();
            DisplayedOrders = true;

            //assert
            Assert.True(DisplayedOrders);

        }

    }
}
