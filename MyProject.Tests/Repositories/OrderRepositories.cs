using MyProject.Tests.Models;
using MyProject.Tests.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyProject.Tests.Models.Order;

namespace MyProject.Tests.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private Dictionary<int, Order> orders = new Dictionary<int, Order>();
        private int nextOrderId = 1;

        public Order Create(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.Id = nextOrderId++;
            orders.Add(order.Id, order);
            return order;
        }

        public Order GetById(int id)
        {
            if (orders.TryGetValue(id, out var order))
            {
                return order;
            }
            return null;
        }

        public bool Update(int id, Order updatedOrder)
        {
            if (orders.TryGetValue(id, out var existingOrder))
            {
                existingOrder.ClientID = updatedOrder.ClientID;
                existingOrder.ProductList = updatedOrder.ProductList;
                existingOrder.Status = updatedOrder.Status;
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            if (orders.ContainsKey(id))
            {
                orders.Remove(id);
                return true;
            }
            return false;
        }
    }


}
