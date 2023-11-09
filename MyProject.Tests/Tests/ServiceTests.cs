using Moq;
using MyProject.Tests.Models;
using MyProject.Tests.Service;
using MyProject.Tests.Repositories;
using MyProject.Tests.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Tests
{
    public class ServiceTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void CreateOrder_ValidOrder_ReturnsOrder(int QuantityInStock)
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var client = new Client { Id = 1 };
            var product = new Product { Id = 1, QuantityInStock = QuantityInStock };
            var clientId = 1;
            var productIds = new List<int> { 1 };
            clientRepositoryMock.Setup(repo => repo.GetById(clientId)).Returns(client);
            productRepositoryMock.Setup(repo => repo.GetById(productIds[0])).Returns(product);
            orderRepositoryMock.Setup(repo => repo.Create(It.IsAny<Order>())).Returns(new Order { Id = 1 });

            // Act
            var createdOrder = service.CreateOrder(clientId, productIds);

            // Assert
            Assert.NotNull(createdOrder);
        }

        [Fact]
        public void CreateOrder_ClientNotExists_ThrowsException()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var clientId = 1;
            var productIds = new List<int> { 1 };
            

            // Act
            Action action = () => service.CreateOrder(clientId, productIds);

            // Assert
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void CreateOrder_ProductNotExists_ThrowsException()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var clientId = 1;
            var client = new Client { Id = clientId };
            var productIds = new List<int> { 1 };
            clientRepositoryMock.Setup(repo => repo.GetById(clientId)).Returns(client);

            // Act
            Action action = () => service.CreateOrder(clientId, productIds);

            // Assert
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void CreateOrder_ProductNotInStock_ThrowsException()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var clientId = 1;
            var client = new Client { Id = clientId };
            var product = new Product { Id = clientId, QuantityInStock = 0 };
            var productIds = new List<int> { 1 };
            clientRepositoryMock.Setup(repo => repo.GetById(clientId)).Returns(client);
            productRepositoryMock.Setup(repo => repo.GetById(productIds[0])).Returns(product);

            // Act
            Action action = () => service.CreateOrder(clientId, productIds);

            // Assert
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void UpdateOrderStatus_OrderExists_UpdatesStatus()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var orderId = 1;
            var updatedStatus = Order.OrderStatus.InProgress;
            orderRepositoryMock.Setup(repo => repo.GetById(orderId)).Returns(new Order { Id = orderId });

            // Act
            var result = service.UpdateOrderStatus(orderId, updatedStatus);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateOrderStatus_OrderDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var orderId = 1;
            var updatedStatus = Order.OrderStatus.InProgress;

            // Act
            var result = service.UpdateOrderStatus(orderId, updatedStatus);

            // Assert
            Assert.False(result);
        }

        public static IEnumerable<object[]> GetProducts()
        {
            yield return new Product[] { new Product { Id = 1, Name = "Product1", QuantityInStock = 1, Price = 10.0 } };
            yield return new Product[] { new Product { Id = 2, Name = "Product2", QuantityInStock = 4, Price = 20.0 } };
            yield return new Product[] { new Product { Id = 3, Name = "Product3", QuantityInStock = 2, Price = 100.0 } };


        }

        [Theory]
        [MemberData(nameof(GetProducts))]
        public void CancelOrder_OrderExists_ProductsAreRestocked(Product product)
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var orderId = 1;
            var productsInOrder = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 } };
            var order = new Order { Id = orderId, ProductList = productsInOrder };
            orderRepositoryMock.Setup(repo => repo.GetById(orderId)).Returns(order);
            orderRepositoryMock.Setup(repo => repo.Delete(orderId)).Returns(true);
            productRepositoryMock.Setup(repo => repo.GetById(1)).Returns(new Product { Id = 1, QuantityInStock = 1 });
            productRepositoryMock.Setup(repo => repo.GetById(2)).Returns(new Product { Id = 2, QuantityInStock = 0 });

            // Act
            var result = service.CancelOrder(orderId);

            // Assert
            Assert.True(result);
            productRepositoryMock.Verify(repo => repo.Update(1, It.Is<Product>(p => p.QuantityInStock == 2)), Times.Once);
            productRepositoryMock.Verify(repo => repo.Update(2, It.Is<Product>(p => p.QuantityInStock == 1)), Times.Once);
        }

        [Fact]
        public void CancelOrder_OrderDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var service = new Service.Service(orderRepositoryMock.Object, productRepositoryMock.Object, clientRepositoryMock.Object);

            var orderId = 1;
            var productsInOrder = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 } };
            var order = new Order { Id = orderId, ProductList = productsInOrder };

            // Act
            var result = service.CancelOrder(orderId);

            // Assert
            Assert.False(result);
        }
    }
}
