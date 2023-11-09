using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientID { get; set; }

        public List<Product> ProductList { get; set; }
        public OrderStatus Status { get; set; }


        public enum OrderStatus
        {
            New,
            InProgress,
            Finalized
        }
    }
}
