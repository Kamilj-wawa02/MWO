using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Tests.Models;
using MyProject.Tests.Repositories.Interfaces;

namespace MyProject.Tests.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private Dictionary<int, Product> products = new Dictionary<int, Product>();
        private int nextProductId = 1;

        public Product Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.Id = nextProductId++;
            products.Add(product.Id, product);
            return product;
        }

        public Product GetById(int id)
        {
            if (products.TryGetValue(id, out var product))
            {
                return product;
            }
            return null;
        }

        public bool Update(int id, Product updatedProduct)
        {
            if (products.TryGetValue(id, out var existingProduct))
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.QuantityInStock = updatedProduct.QuantityInStock;
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            if (products.ContainsKey(id))
            {
                products.Remove(id);
                return true;
            }
            return false;
        }
    }
}
