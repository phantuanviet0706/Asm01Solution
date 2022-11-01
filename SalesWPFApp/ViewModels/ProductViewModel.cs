using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFApp.ViewModels
{
    public class ProductViewModel
    {
        ProductRepository productRepository;
        public ObservableCollection<Product> productsList { get; set; }
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
        }
    }
}
