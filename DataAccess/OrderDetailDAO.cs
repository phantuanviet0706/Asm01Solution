using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailsList()
        {
            List<OrderDetail> orderDetailsList;
            try
            {
                var asm1DB = new Assignment1Context();
                orderDetailsList = asm1DB.OrderDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetailsList;
        }

        public OrderDetail GetOrderDetailById(int? oid, int? pid)
        {
            OrderDetail orderDetail = null;
            try
            {
                var asm1DB = new Assignment1Context();
                orderDetail = asm1DB.OrderDetails.Where(orderDetail => orderDetail.OrderId == oid)
                    .Where(orderDetail => orderDetail.ProductId == pid).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public void AddNew(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = GetOrderDetailById(orderDetail.OrderId, orderDetail.ProductId);
                if (_orderDetail == null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.OrderDetails.Add(orderDetail);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This order detail is already existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = GetOrderDetailById(orderDetail.OrderId, orderDetail.ProductId);
                if (_orderDetail != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Entry(orderDetail).State = EntityState.Modified;
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This order detail isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = GetOrderDetailById(orderDetail.OrderId, orderDetail.ProductId);
                if (_orderDetail != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.OrderDetails.Remove(orderDetail);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This order detail isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
