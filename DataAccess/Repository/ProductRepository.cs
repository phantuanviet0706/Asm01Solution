using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProductById(int productId) => ProductDAO.Instance.GetProductById(productId);
        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProductsList();
        public void InsertProduct(Product product) => ProductDAO.Instance.AddNew(product);
        public void UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);
        public void DeleteProduct(int productId) => ProductDAO.Instance.DeleteProduct(productId);
    }
}
