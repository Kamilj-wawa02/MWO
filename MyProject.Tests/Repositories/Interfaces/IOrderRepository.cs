using MyProject.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order Create(Order order);
        Order GetById(int id);
        bool Update(int id, Order updatedOrder);
        bool Delete(int id);
    }
}
