using Moq;
using MyProject.Tests.Models;
using MyProject.Tests.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void Create_ProductIsCreated()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var product = new Product { Name = "Product 1", Price = 10.0, QuantityInStock = 100 };
            productRepositoryMock.Setup(repo => repo.Create(product)).Returns(product);

            // Act
            var createdProduct = productRepositoryMock.Object.Create(product);

            // Assert
            Assert.NotNull(createdProduct);
            Assert.Equal(product.Name, createdProduct.Name);
            Assert.Equal(product.Price, createdProduct.Price);
            Assert.Equal(product.QuantityInStock, createdProduct.QuantityInStock);
        }

        [Fact]
        public void GetById_ProductExists_ReturnsProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productId = 1;
            var product = new Product { Id = productId, Name = "Product 1", Price = 10.0, QuantityInStock = 100 };
            productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns(product);

            // Act
            var retrievedProduct = productRepositoryMock.Object.GetById(productId);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal(product.Id, retrievedProduct.Id);
        }

        [Fact]
        public void GetById_ProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productId = 1;

            // Act
            var retrievedProduct = productRepositoryMock.Object.GetById(productId);

            // Assert
            Assert.Null(retrievedProduct);
        }

        [Fact]
        public void Update_ProductExists_UpdatesProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productId = 1;
            var updatedProduct = new Product { Id = productId, Name = "Product 2", Price = 15.0, QuantityInStock = 50 };
            productRepositoryMock.Setup(repo => repo.Update(productId, updatedProduct)).Returns(true);

            // Act
            var isUpdated = productRepositoryMock.Object.Update(productId, updatedProduct);

            // Assert
            Assert.True(isUpdated);
        }

        [Fact]
        public void Update_ProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productId = 1;
            var updatedProduct = new Product { Id = productId, Name = "Product 2", Price = 15.0, QuantityInStock = 50 };
            
            // Act
            var isUpdated = productRepositoryMock.Object.Update(productId, updatedProduct);

            // Assert
            Assert.False(isUpdated);
        }

        [Fact]
        public void Delete_ProductExists_RemovesProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productId = 1;
            productRepositoryMock.Setup(repo => repo.Delete(productId)).Returns(true);

            // Act
            var isDeleted = productRepositoryMock.Object.Delete(productId);

            // Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public void Delete_ProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productId = 1;
            
            // Act
            var isDeleted = productRepositoryMock.Object.Delete(productId);

            // Assert
            Assert.False(isDeleted);
        }
    }
}
