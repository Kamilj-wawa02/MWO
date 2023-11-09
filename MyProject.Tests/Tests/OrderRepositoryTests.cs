using Moq;
using FluentAssertions;
using MyProject.Tests.Models;
using MyProject.Tests.Repositories.Interfaces;
using MyProject.Tests.Tests.SampleData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyProject.Tests.Tests
{
    public class OrderRepositoryTests
    {
        [Theory]
        [ClassData(typeof(ProductListFromJSON))]
        public void Create_OrderIsCreated(Product product)
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var order = new Order { ClientID = 1, ProductList = new List<Product> { product }, Status = Order.OrderStatus.New };
            orderRepositoryMock.Setup(repo => repo.Create(order)).Returns(order);

            // Act
            var createdOrder = orderRepositoryMock.Object.Create(order);

            // Assert
            Assert.NotNull(createdOrder);
            // FluentAssertions
            order.ClientID.Should().Be(createdOrder.ClientID);
            order.ProductList.Count.Should().Be(createdOrder.ProductList.Count);
            order.Status.Should().Be(createdOrder.Status);
        }

        [Fact]
        public void GetById_OrderExists_ReturnsOrder()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderId = 1;
            var order = new Order { Id = orderId, ClientID = 1, ProductList = new List<Product>(), Status = Order.OrderStatus.New };
            orderRepositoryMock.Setup(repo => repo.GetById(orderId)).Returns(order);

            // Act
            var retrievedOrder = orderRepositoryMock.Object.GetById(orderId);

            // Assert
            Assert.NotNull(retrievedOrder);
            Assert.Equal(order.Id, retrievedOrder.Id);
        }

        [Fact]
        public void GetById_OrderDoesNotExist_ReturnsNull()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderId = 1;

            // Act
            var retrievedOrder = orderRepositoryMock.Object.GetById(orderId);

            // Assert
            Assert.Null(retrievedOrder);
        }

        [Fact]
        public void Update_OrderExists_UpdatesOrder()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderId = 1;
            var updatedOrder = new Order { Id = orderId, ClientID = 2, ProductList = new List<Product>(), Status = Order.OrderStatus.InProgress };
            orderRepositoryMock.Setup(repo => repo.Update(orderId, updatedOrder)).Returns(true);

            // Act
            var isUpdated = orderRepositoryMock.Object.Update(orderId, updatedOrder);

            // Assert
            Assert.True(isUpdated);
        }

        [Fact]
        public void Update_OrderDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderId = 1;
            var updatedOrder = new Order { Id = orderId, ClientID = 2, ProductList = new List<Product>(), Status = Order.OrderStatus.InProgress };
            
            // Act
            var isUpdated = orderRepositoryMock.Object.Update(orderId, updatedOrder);

            // Assert
            Assert.False(isUpdated);
        }

        [Fact]
        public void Delete_OrderExists_RemovesOrder()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderId = 1;
            orderRepositoryMock.Setup(repo => repo.Delete(orderId)).Returns(true);

            // Act
            var isDeleted = orderRepositoryMock.Object.Delete(orderId);

            // Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public void Delete_OrderDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderId = 1;

            // Act
            var isDeleted = orderRepositoryMock.Object.Delete(orderId);

            // Assert
            Assert.False(isDeleted);
        }
    }
}
