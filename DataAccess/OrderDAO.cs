using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetOrdersList()
        {
            List<Order> orderList;
            try
            {
                var asm1DB = new Assignment1Context();
                orderList = asm1DB.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderList;
        }

        public Order GetOrderById(int? id)
        {
            Order order = null;
            try
            {
                var asm1DB = new Assignment1Context();
                order = asm1DB.Orders.SingleOrDefault(order => order.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public void AddNew(Order order)
        {
            try
            {
                Order _order = GetOrderById(order.OrderId);
                if (_order == null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Orders.Add(order);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This order is already existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                Order _order = GetOrderById(order.OrderId);
                if (order != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Entry(order).State = EntityState.Modified;
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This order isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                Order _order = GetOrderById(orderId);
                if (_order != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Orders.Remove(_order);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This order isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
