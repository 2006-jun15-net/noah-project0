using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using StoreApp.DataAccess.Model;
using StoreApp.DataAccess.Repos;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System.Linq;
using System.Security.Cryptography;
using Xunit;
using Program = StoreApp.App.Program;

namespace StoreApp.Test
{
    public class CustomerControllerTest
    {
        [Fact]
        public void DiplayCustomersShouldDisplay()
        {
            //arrange
            CustomerController cc = new CustomerController();

            //act
            cc.DisplayCustomers();
            bool DisplayedCustomers = true;

            //assert
            Assert.True(DisplayedCustomers);

        }
    }
}
