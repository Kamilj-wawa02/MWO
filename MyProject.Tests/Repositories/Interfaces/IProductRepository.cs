using MyProject.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product Create(Product product);
        Product GetById(int id);
        bool Update(int id, Product updatedProduct);
        bool Delete(int id);
    }
}
