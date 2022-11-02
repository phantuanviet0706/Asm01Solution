using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SalesWPFApp.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        ProductRepository productRepository;
        public ObservableCollection<Product> productsList { get; set; }

        private int _ProductId;
        public int ProductId { get => _ProductId; set { _ProductId = value; OnPropertyChanged(); } }

        private string _CategoryId;
        public string CategoryId { get => _CategoryId; set { _CategoryId = value; OnPropertyChanged(); } }

        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }

        private string _Weight;
        public string Weight { get => _Weight; set { _Weight = value; OnPropertyChanged(); } }

        private string _UnitPrice;
        public string UnitPrice { get => _UnitPrice; set { _UnitPrice = value; OnPropertyChanged(); } }

        private string _UnitsInStock;
        public string UnitsInStock { get => _UnitsInStock; set { _UnitsInStock = value; OnPropertyChanged(); } }

        private Product _SelectedItem;
        public Product SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; 
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    ProductId = SelectedItem.ProductId;
                    CategoryId = SelectedItem.CategoryId.ToString();
                    ProductName = SelectedItem.ProductName;
                    Weight = SelectedItem.Weight;
                    UnitPrice = SelectedItem.UnitPrice.ToString();
                    UnitsInStock = SelectedItem.UnitsInStock.ToString();
                }
            }
        }

        public ICommand AddProductCommand { get; set; }
        public ICommand UpdateProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ProductViewModel()
        {
            this.productRepository = new ProductRepository();
            productsList = new ObservableCollection<Product>();
            LoadProductList();
        }

        public void LoadProductList()
        {
            productsList.Clear();
            List<Product> products = (List<Product>)productRepository.GetProducts();
            foreach (Product product in products)
            {
                productsList.Add(product);
            }

            AddProductCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                var validate = Validate(CategoryId, ProductName, Weight, UnitPrice, UnitsInStock);
                if(validate == 1)
                {
                    return;
                }
                Product product = new Product(Int32.Parse(CategoryId), ProductName, Weight, Decimal.Parse(UnitPrice), Int32.Parse(UnitsInStock));
                try
                {
                    productRepository.InsertProduct(product);
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadProductList();
            });

            UpdateProductCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                var validate = Validate(CategoryId, ProductName, Weight, UnitPrice, UnitsInStock);
                if (validate == 1)
                {
                    return;
                }
                Product product = new Product(ProductId, Int32.Parse(CategoryId), ProductName, Weight, Decimal.Parse(UnitPrice), Int32.Parse(UnitsInStock));
                try
                {
                    productRepository.UpdateProduct(product);
                    MessageBox.Show("Sửa thông tin sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadProductList();
            });

            DeleteProductCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                if (MessageBox.Show("Bạn có chắc là muốn xóa sản phẩm này không?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        productRepository.DeleteProduct(ProductId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    LoadProductList();
                }
            });
        }

        private int Validate(string categoryId, string productName, string weight, string unitPrice, string unitsInStock)
        {
            var count = 0;
            if(categoryId == "" || categoryId == null)
            {
                MessageBox.Show("Category không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }
            int catId;
            if(!Int32.TryParse(categoryId, out catId))
            {
                MessageBox.Show("Category phải là một số!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }

            if(productName == ""|| productName == null)
            {
                MessageBox.Show("Hãy nhập tên sản phẩm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }

            if(weight == "" || weight == null)
            {
                MessageBox.Show("Hãy nhập trọng lượng của sản phẩm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }

            if (unitPrice == "" || unitPrice == null)
            {
                MessageBox.Show("Hãy nhập giá của sản phẩm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }

            decimal price;
            if(!Decimal.TryParse(unitPrice, out price))
            {
                MessageBox.Show("Giá của sản phẩm phải là số nguyên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }

            if (unitsInStock == "" || unitsInStock == null)
            {
                MessageBox.Show("Hãy nhập số lượng hàng tồn kho!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }

            int stock;
            if(!Int32.TryParse(unitsInStock, out stock))
            {
                MessageBox.Show("Số hàng tồn kho phải là số nguyên dương!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return ++count;
            }
            return count;
        }
    }
}
