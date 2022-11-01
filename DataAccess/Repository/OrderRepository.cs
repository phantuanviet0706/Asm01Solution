using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Order GetOrderById(int orderId) => OrderDAO.Instance.GetOrderById(orderId);
        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrdersList();
        public void InsertOrder(Order order) => OrderDAO.Instance.AddNew(order);
        public void UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);
        public void DeleteOrder(Order order) => OrderDAO.Instance.DeleteOrder(order);
    }
}
