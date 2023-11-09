using MWO_2;

namespace MyProject.Tests;

public class UnitTest1
{

    [Fact]
    public void Create_WhenValidBookIsProvided_ShouldReturnCreatedBookWithId()
    {
        // Arrange
        var repository = new ProductRepository();
        var product = new Product { Name = "Sample Product", Description = "Some description here" };

        // Act
        var createdProduct = repository.Create(product);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.True(createdProduct.Id > 0);
        Assert.Equal(product.Name, createdProduct.Name);
        Assert.Equal(product.Description, createdProduct.Description);
    }

    [Theory]
    [InlineData("Sample Book 1", "John Doe 1")]
    [InlineData("Sample Book 2", "John Doe 2")]
    [InlineData("Sample Book 3", "John Doe 3")]
    public void Create_WhenValidBookIsProvided_ShouldReturnCreatedBookWithId2(string name, string description)
    {
        // Arrange
        var repository = new ProductRepository();
        var product = new Product { Name = name, Description = description };

        // Act
        var createdProduct = repository.Create(product);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.True(createdProduct.Id > 0);
        Assert.Equal(name, createdProduct.Name);
        Assert.Equal(description, createdProduct.Description);
    }

}