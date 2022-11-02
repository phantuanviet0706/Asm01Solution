using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetProductsList()
        {
            List<Product> productsList;
            try
            {
                var asm1DB = new Assignment1Context();
                productsList = asm1DB.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return productsList;
        }

        public Product GetProductById(int? id)
        {
            Product product = null;
            try
            {
                var asm1DB = new Assignment1Context();
                product = asm1DB.Products.SingleOrDefault(product => product.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void AddNew(Product product)
        {
            try
            {
                Product _product = GetProductById(product.ProductId);
                if (_product == null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Products.Add(product);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This product is already existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                Product _product = GetProductById(product.ProductId);
                if (product != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Entry(product).State = EntityState.Modified;
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This product isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                Product _product = GetProductById(productId);
                if (_product != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Products.Remove(_product);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This product isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
