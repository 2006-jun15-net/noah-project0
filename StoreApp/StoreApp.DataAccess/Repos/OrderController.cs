using StoreApp.DataAccess.Model;
using StoreApp.Library.Interfaces;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.DataAccess.Repos
{
    public class OrderController
    {
        public readonly IRepository<Orders> repository = null;

        public OrderController()
        {
            repository = new GenericRepository<Orders>();
        }
        public OrderController(IRepository<Orders> repo)
        {
            repository = repo;
        }
        public void DisplayOrders()
        {
            Console.WriteLine("List of Orders:");
            foreach(var order in repository.GetAll().ToList())
            {
                Console.WriteLine($"Order ID: {order.OrderId} OrderDate: {order.OrderDate} " +
                    $"Placed by customer with ID: {order.CustomerId} At Store with ID: {order.StoreId}\n" +
                    $"Description: {order.OrderDescription}\n");
            }
        }
    }



}
