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
        private int _OrderId;
        public int OrderId { get { return _OrderId; } set { _OrderId = value; OnPropertyChanged(); } }

        private int _MemberId;
        public int MemberId { get { return _MemberId; } set { _MemberId = value; OnPropertyChanged(); } }

        private DateTime _OrderDate = DateTime.Now;
        public DateTime OrderDate { get { return _OrderDate; } set { _OrderDate = value; OnPropertyChanged(); } }

        private DateTime _RequireDate = DateTime.Now;
        public DateTime RequireDate { get { return _RequireDate; } set { _RequireDate = value; OnPropertyChanged(); } }

        private DateTime _ShippedDate = DateTime.Now;
        public DateTime ShippedDate { get { return _ShippedDate; } set { _ShippedDate = value; OnPropertyChanged(); } }

        private decimal _Freight;
        public decimal Freight { get { return _Freight; } set { _Freight = value; OnPropertyChanged(); } }

        OrderRepository orderRepository;
        public ObservableCollection<Order> ordersList { get; set; }

        private Order _SelectedItem;
        public Order SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    OrderId = SelectedItem.OrderId;
                    MemberId = SelectedItem.MemberId;
                    OrderDate = SelectedItem.OrderDate;
                    RequireDate = SelectedItem.RequireDate;
                    ShippedDate = SelectedItem.ShippedDate;
                    Freight = SelectedItem.Freight;
                }
            }
        }

        public ICommand AddOrderCommand { get; set; }
        public ICommand UpdateOrderCommand { get; set; }
        public ICommand RemoveOrderCommand { get; set; }
        public OrderViewModel()
        {
            this.orderRepository = new OrderRepository();
            ordersList = new ObservableCollection<Order>();
            LoadOrderList();

            AddOrderCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                int validate = Validate(MemberId, OrderDate, RequireDate, ShippedDate, Freight);
                if(validate == 1)
                {
                    return;
                }
                Order order = new Order(MemberId, OrderDate, RequireDate, ShippedDate, Freight);
                try
                {
                    orderRepository.InsertOrder(order);
                    MessageBox.Show("Thêm order thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadOrderList();

            });

            UpdateOrderCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                int validate = Validate(MemberId, OrderDate, RequireDate, ShippedDate, Freight);
                if (validate == 1)
                {
                    return;
                }
                Order order = new Order(OrderId, MemberId, OrderDate, RequireDate, ShippedDate, Freight);
                try
                {
                    orderRepository.UpdateOrder(order);
                    MessageBox.Show("Sửa order thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadOrderList();
            });

            RemoveOrderCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                if (MessageBox.Show("Bạn có chắc là muốn xóa order này không?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        orderRepository.DeleteOrder(OrderId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    LoadOrderList();
                }
            });
        }

        private int Validate(int memberId, DateTime orderDate, DateTime requireDate, DateTime shippedDate, decimal freight)
        {
            var count = 0;
            return count;
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
