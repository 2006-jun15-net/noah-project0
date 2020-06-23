using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Library.Repos
{
    public class OrderHistory
    {
        private readonly ICollection<Order> _data;

        public OrderHistory(ICollection<Order> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }
        public IEnumerable<Order> GetOrderHistory()
        {
            foreach (var item in _data)
            {
                yield return item;
            }
        }

        public Order GetOrderById(int id)
        {
            return _data.First(o => o.OrderID == id);
        }

        public void AddOrder(Order order)
        {
            if (_data.Any(o => o.OrderID == order.OrderID))
            {
                throw new InvalidOperationException($"Cannot create order with ID: {order.OrderID} because it already exists.");
            }
            else
            {
                _data.Add(order);
            }

        }

        public IEnumerable<Order> GetCustomerOrderHistory(int id)
        {
            if(_data.Any(o => o.CurrentCustomer.CustomerID == id))
            {
                IEnumerable<Order> customerOrders = _data.Where(o => o.CurrentCustomer.CustomerID == id);
                return customerOrders;
            }
            else
            {
                throw new ArgumentException($"Customer with ID:{id} does not exist.");
            }
        }
    }

    

}
