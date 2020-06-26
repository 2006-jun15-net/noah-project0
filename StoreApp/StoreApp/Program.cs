using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Model;
using StoreApp.DataAccess.Repos;
using StoreApp.Library.Model;

namespace StoreApp.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            CustomerController cr = new CustomerController();
            OrderController oh = new OrderController();
            ProductController pr = new ProductController();
            StoreController sr = new StoreController();
            
            //Register Customers
            
            string fn = "testfn";
            string ln = "testln";
            Customer newCust = new Customer {FirstName = fn, LastName = ln };
          
            cr.DisplayCustomers();
            cr.DisplayCustomers(1);



            //Add Stores

            //Add Products

            //Add Products to Stores

            //Add Orders by Customers

            //REQUIREMENTS:
            //place orders to store locations for customers
            //add a new customer
            //search customers by name
            //display details of an order
            //display all order history of a store location
            //display all order history of a customer


        }

 
    }
}
