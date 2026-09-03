using Blocks.Application.Exceptions;
using Inventory.Application.Features.Products;
using Inventory.Application.Features.Products.Queries;
using Inventory.Application.Features.Products.Queries.GetProductByBarCode;
using Moq;
using Shouldly;

namespace Inventory.Tests.Application.Features.Products.Queries.GetProductByBarCode;

[TestFixture]
public class GetProductByBarCodeQueryHandlerTests
{
    private Mock<IProductQueries> _productQueriesMock;
    private GetProductByBarCodeQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _productQueriesMock = new Mock<IProductQueries>();
        _handler = new GetProductByBarCodeQueryHandler(_productQueriesMock.Object);
    }

    [Test]
    public async Task ShouldReturnProductWhenBarCodeExists()
    {
        // Arrange
        var expectedProduct = new ProductDto
        {
            Id = Guid.NewGuid(),
            BarCode = "1234567890123",
            Description = "Test Product",
            Currency = "DOP",
            Cost = 10.50m,
            Price = 15.75m,
            WholesaleQuantity = 10.0m,
            WholesalePrice = 12.50m,
            Stock = 100.0m,
            MinStock = 5.0m,
            MeasurementUnit = "Kilogramo"
        };

        _productQueriesMock
            .Setup(x => x.GetByBarCodeAsync("1234567890123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProduct);

        var query = new GetProductByBarCodeQuery("1234567890123");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(expectedProduct);
        result.BarCode.ShouldBe("1234567890123");
        
        _productQueriesMock.Verify(
            x => x.GetByBarCodeAsync("1234567890123", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ShouldThrowNotFoundExceptionWhenProductDoesNotExist()
    {
        // Arrange
        _productQueriesMock
            .Setup(x => x.GetByBarCodeAsync("9999999999999", It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductDto?)null);

        var query = new GetProductByBarCodeQuery("9999999999999");

        // Act & Assert
        var exception = await Should.ThrowAsync<NotFoundException>(
            () => _handler.Handle(query, CancellationToken.None));

        exception.Message.ShouldContain("No existe el producto con el código de barras 9999999999999");
        
        _productQueriesMock.Verify(
            x => x.GetByBarCodeAsync("9999999999999", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ShouldHandle8DigitBarCode()
    {
        // Arrange
        var expectedProduct = new ProductDto
        {
            Id = Guid.NewGuid(),
            BarCode = "12345678",
            Description = "8-digit Product",
            Currency = "DOP",
            Cost = 5.00m,
            Price = 7.50m,
            WholesaleQuantity = 20.0m,
            WholesalePrice = 6.00m,
            Stock = 50.0m,
            MinStock = 10.0m,
            MeasurementUnit = "Unidad"
        };

        _productQueriesMock
            .Setup(x => x.GetByBarCodeAsync("12345678", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProduct);

        var query = new GetProductByBarCodeQuery("12345678");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.BarCode.ShouldBe("12345678");
        
        _productQueriesMock.Verify(
            x => x.GetByBarCodeAsync("12345678", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ShouldHandle12DigitBarCode()
    {
        // Arrange
        var expectedProduct = new ProductDto
        {
            Id = Guid.NewGuid(),
            BarCode = "123456789012",
            Description = "12-digit Product",
            Currency = "USD",
            Cost = 25.00m,
            Price = 35.00m,
            WholesaleQuantity = 5.0m,
            WholesalePrice = 30.00m,
            Stock = 25.0m,
            MinStock = 5.0m,
            MeasurementUnit = "Litro"
        };

        _productQueriesMock
            .Setup(x => x.GetByBarCodeAsync("123456789012", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProduct);

        var query = new GetProductByBarCodeQuery("123456789012");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.BarCode.ShouldBe("123456789012");
        
        _productQueriesMock.Verify(
            x => x.GetByBarCodeAsync("123456789012", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ShouldHandle14DigitBarCode()
    {
        // Arrange
        var expectedProduct = new ProductDto
        {
            Id = Guid.NewGuid(),
            BarCode = "12345678901234",
            Description = "14-digit Product",
            Currency = "EUR",
            Cost = 100.00m,
            Price = 150.00m,
            WholesaleQuantity = 2.0m,
            WholesalePrice = 125.00m,
            Stock = 10.0m,
            MinStock = 2.0m,
            MeasurementUnit = "Metro"
        };

        _productQueriesMock
            .Setup(x => x.GetByBarCodeAsync("12345678901234", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProduct);

        var query = new GetProductByBarCodeQuery("12345678901234");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.BarCode.ShouldBe("12345678901234");
        
        _productQueriesMock.Verify(
            x => x.GetByBarCodeAsync("12345678901234", It.IsAny<CancellationToken>()),
            Times.Once);
    }
}