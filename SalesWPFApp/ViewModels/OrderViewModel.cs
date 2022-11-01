using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SalesWPFApp.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        OrderRepository orderRepository;
        public ObservableCollection<Order> ordersList { get; set; }
        public OrderViewModel()
        {
            this.orderRepository = new OrderRepository();
            ordersList = new ObservableCollection<Order>();
            LoadOrderList();
        }

        public void LoadOrderList()
        {
            ordersList.Clear();
            List<Order> orders = (List<Order>)orderRepository.GetOrders();
            foreach (Order order in orders)
            {
                ordersList.Add(order);
            }
        }
    }
}
