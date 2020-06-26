using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Model;

namespace StoreApp.App
{
    public class Program
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static readonly DbContextOptions<_2006StoreApplicationContext> Options = new DbContextOptionsBuilder<_2006StoreApplicationContext>()
            .UseLoggerFactory(MyLoggerFactory)
            .UseSqlServer(SecretConfiguration.connectionString)
            .Options;


        static void Main(string[] args)
        {
            //Register Customers

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
