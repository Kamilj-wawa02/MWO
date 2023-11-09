using MyProject.Tests.Models;
using MyProject.Tests.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private Dictionary<int, Client> clients = new Dictionary<int, Client>();
        private int nextClientId = 1;

        public Client Create(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            client.Id = nextClientId++;
            clients.Add(client.Id, client);
            return client;
        }

        public Client GetById(int id)
        {
            if (clients.TryGetValue(id, out var client))
            {
                return client;
            }
            return null;
        }

        public bool Update(int id, Client updatedClient)
        {
            if (clients.TryGetValue(id, out var existingClient))
            {
                existingClient.Name = updatedClient.Name;
                existingClient.LastName = updatedClient.LastName;
                existingClient.Email = updatedClient.Email;
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            if (clients.ContainsKey(id))
            {
                clients.Remove(id);
                return true;
            }
            return false;
        }
    }
}
