using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    /// <summary>
    /// This class controls the repository for the Orders table in the database and provides other methods specific to Orders
    /// </summary>
    public class OrderController
    {
        /// <summary>
        /// Repository that handle DML operations for the Orders table
        /// </summary>
        public readonly IRepository<Orders> repository = null;

        /// <summary>
        /// Iniitializes the repository for Orders
        /// </summary>
        public OrderController()
        {
            repository = new GenericRepository<Orders>();
        }
        public OrderController(IRepository<Orders> repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Prints out an overview of all the orders in the Orders table
        /// </summary>
        public void DisplayOrders()
        {
            Console.WriteLine("List of Orders:");
            foreach(var order in repository.GetAll().ToList())
            {
                Console.WriteLine($"Order ID: {order.OrderId} Total Cost: {order.TotalCost}\n" +
                    $"Placed by customer with ID: {order.CustomerId} On: {order.OrderDate} At Store with ID: {order.StoreId}\n" +
                    $"Description: {order.OrderDescription}\n");
            }
        }

        /// <summary>
        /// Prints the details of a specific order including the product details of the order
        /// </summary>
        /// <param name="orderId">The id of the order</param>
        public void DisplayOrderDetails(int orderId)
        {
            if(repository.GetAll().Any(o => o.OrderId == orderId))
            {
                var context = new _2006StoreApplicationContext(GenericRepository<Orders>.Options);
                var order = context.Orders
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Product)
                    .First(o => o.OrderId == orderId);
                Console.WriteLine($"Order ID: {order.OrderId} Total Cost: {order.TotalCost}\n" +
                    $"Placed by customer with ID: {order.CustomerId} On: {order.OrderDate} At Store with ID: {order.StoreId}\n" +
                    $"Description: {order.OrderDescription}");
                foreach (var ol in order.OrderLines)
                {
                    Console.WriteLine($"Product: {ol.Product.ProductName}\nPrice: {ol.Product.Price}\nQty: {ol.Amount}\n");
                }
                context.Dispose();
            }
            else
            {
                Console.WriteLine($"No orders with ID: {orderId}");
            }
            
        }

        /// <summary>
        /// Prints the order details of every order placed at a particular store
        /// </summary>
        /// <param name="storeId">The id of the store</param>
        public void DisplayOrderDetailsOfStore(int storeId)
        {
            if (repository.GetAll().Any(o => o.StoreId == storeId))
            {
                List<Orders> orders = repository.GetAll().Where(o => o.StoreId == storeId).ToList();
                foreach (var order in orders)
                {
                    DisplayOrderDetails(order.OrderId);
                }
            }
            else
            {
                Console.WriteLine($"No orders placed at store with ID: {storeId}");
            }
            
        }
        
        /// <summary>
        /// Prints the order details of every order placed by a particular customer
        /// </summary>
        /// <param name="customerId">The id of the customer</param>
        public void DisplayOrderDetailsOfCustomer(int customerId)
        {
            if (repository.GetAll().Any(o => o.CustomerId == customerId))
            {
                List<Orders> orders = repository.GetAll().Where(o => o.CustomerId == customerId).ToList();
                foreach (var order in orders)
                {
                    DisplayOrderDetails(order.OrderId);
                }
            }
            else
            {
                Console.WriteLine($"No orders for customer with ID: {customerId}");
            }
        }

    }



}
