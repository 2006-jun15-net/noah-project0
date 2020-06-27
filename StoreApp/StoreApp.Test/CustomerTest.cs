using StoreApp.DataAccess.Model;
using StoreApp.DataAccess.Repos;
using StoreApp.Library.Model;
using System.Linq;
using Xunit;
using Program = StoreApp.App.Program;

namespace StoreApp.Test
{
    public class CustomerTest
    {
        [Fact]
        public void AddShouldAddCustomer()
        {
            //arrange
            Customers c = new Customers();
            c.FirstName = "TestFirst";
            c.LastName = "TestLast";

            //act
            CustomerController cr = new CustomerController();
            cr.repository.Add(c);

            var c2 = cr.repository.GetById(1);
    

            //assert
            Assert.Equal(c.FirstName, c2.FirstName);



        }
    }
}
