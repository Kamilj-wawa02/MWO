using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MyProject.Tests.Models;
using MyProject.Tests.Repositories;
using MyProject.Tests.Repositories.Interfaces;
using Xunit;

namespace MyProject.Tests.Tests
{
    public class ClientRepositoryTests
    {
        [Fact]
        public void Create_ClientIsCreated()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var client = new Client { Name = "John", LastName = "Doe" };
            clientRepositoryMock.Setup(repo => repo.Create(client)).Returns(client);

            // Act
            var createdClient = clientRepositoryMock.Object.Create(client);

            // Assert
            Assert.NotNull(createdClient);
            Assert.Equal(client.Name, createdClient.Name);
            Assert.Equal(client.LastName, createdClient.LastName);
        }

        [Fact]
        public void GetById_ClientExists_ReturnsClient()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var clientId = 1;
            var client = new Client { Id = clientId, Name = "John", LastName = "Doe" };
            clientRepositoryMock.Setup(repo => repo.GetById(clientId)).Returns(client);

            // Act
            var retrievedClient = clientRepositoryMock.Object.GetById(clientId);

            // Assert
            Assert.NotNull(retrievedClient);
            Assert.Equal(client.Id, retrievedClient.Id);
        }

        [Fact]
        public void GetById_ClientDoesNotExist_ReturnsNull()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var clientId = 1;

            // Act
            var retrievedClient = clientRepositoryMock.Object.GetById(clientId);

            // Assert
            Assert.Null(retrievedClient);
        }

        [Fact]
        public void Update_ClientExists_UpdatesClient()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var clientId = 1;
            var updatedClient = new Client { Id = clientId, Name = "Jane", LastName = "Smith" };
            clientRepositoryMock.Setup(repo => repo.Update(clientId, updatedClient)).Returns(true);

            // Act
            var isUpdated = clientRepositoryMock.Object.Update(clientId, updatedClient);

            // Assert
            Assert.True(isUpdated);
        }

        [Fact]
        public void Update_ClientDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var clientId = 1;
            var updatedClient = new Client { Id = clientId, Name = "Jane", LastName = "Smith" };

            // Act
            var isUpdated = clientRepositoryMock.Object.Update(clientId, updatedClient);

            // Assert
            Assert.False(isUpdated);
        }

        [Fact]
        public void Delete_ClientExists_RemovesClient()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var clientId = 1;
            clientRepositoryMock.Setup(repo => repo.Delete(clientId)).Returns(true);

            // Act
            var isDeleted = clientRepositoryMock.Object.Delete(clientId);

            // Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public void Delete_ClientDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var clientId = 1;

            // Act
            var isDeleted = clientRepositoryMock.Object.Delete(clientId);

            // Assert
            Assert.False(isDeleted);
        }
    }
}
