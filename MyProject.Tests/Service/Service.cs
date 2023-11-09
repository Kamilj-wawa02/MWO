using MyProject.Tests.Models;
using MyProject.Tests.Repositories;
using MyProject.Tests.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Service
{
    public class Service
    {
        private IOrderRepository orderRepository;
        private IProductRepository productRepository;
        private IClientRepository clientRepository;

        public Service(IOrderRepository orderRepo, IProductRepository productRepo, IClientRepository clientRepo)
        {
            orderRepository = orderRepo;
            productRepository = productRepo;
            clientRepository = clientRepo;
        }

        public Order CreateOrder(int clientId, List<int> productIds)
        {
            // Sprawdzenie, czy klient istnieje
            var client = clientRepository.GetById(clientId);
            if (client == null)
            {
                throw new InvalidOperationException("Klient o podanym ID nie istnieje.");
            }

            // Sprawdzenie, czy produkty są dostępne w magazynie
            List<Product> selectedProducts = new List<Product>();
            foreach (var productId in productIds)
            {
                var product = productRepository.GetById(productId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Produkt o ID {productId} nie istnieje.");
                }
                if (product.QuantityInStock <= 0)
                {
                    throw new InvalidOperationException($"Produkt o ID {productId} nie jest dostępny w magazynie.");
                }
                selectedProducts.Add(product);
            }

            // Utworzenie nowego zamówienia
            var newOrder = new Order
            {
                ClientID = clientId,
                ProductList = selectedProducts,
                Status = Order.OrderStatus.New
            };

            // Aktualizacja ilości produktów w magazynie
            foreach (var product in selectedProducts)
            {
                product.QuantityInStock--;
                productRepository.Update(product.Id, product);
            }

            return orderRepository.Create(newOrder);
        }

        public bool UpdateOrderStatus(int orderId, Order.OrderStatus newStatus)
        {
            var existingOrder = orderRepository.GetById(orderId);
            if (existingOrder == null)
            {
                return false;
            }

            existingOrder.Status = newStatus;
            orderRepository.Update(orderId, existingOrder);
            return true;
        }

        public bool CancelOrder(int orderId)
        {
            var existingOrder = orderRepository.GetById(orderId);
            if (existingOrder == null)
            {
                return false;
            }

            // Przywrócenie produktów do magazynu
            foreach (var product in existingOrder.ProductList)
            {
                var productInStock = productRepository.GetById(product.Id);
                if (productInStock != null)
                {
                    productInStock.QuantityInStock++;
                    productRepository.Update(product.Id, productInStock);
                }
            }

            return orderRepository.Delete(orderId);
        }
    }
}
