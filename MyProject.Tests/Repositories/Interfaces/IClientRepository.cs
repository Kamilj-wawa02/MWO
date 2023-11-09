using MyProject.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Client Create(Client client);
        Client GetById(int id);
        bool Update(int id, Client updatedClient);
        bool Delete(int id);
    }
}
