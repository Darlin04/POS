using Inventory.Application.Features.Products;
using Inventory.Application.Features.Products.Queries;
using Inventory.Application.Features.Products.Queries.GetAllProducts;
using Moq;
using Shouldly;

namespace Inventory.Tests.Application.Features.Products.Queries.GetAllProducts;

[TestFixture]
public class GetAllProductsQueryHandlerTests
{
    private Mock<IProductQueries> _productQueriesMock;
    private GetAllProductsQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _productQueriesMock = new Mock<IProductQueries>();
        _handler = new GetAllProductsQueryHandler(_productQueriesMock.Object);
    }

    [Test]
    public async Task ShouldReturnAllProductsWhenQueryIsExecuted()
    {
        // Arrange
        var expectedProducts = new List<ProductDto>
        {
            new ProductDto
            {
                Id = Guid.NewGuid(),
                BarCode = "12345678",
                Description = "Product 1",
                Currency = "DOP",
                Cost = 10.50m,
                Price = 15.75m,
                WholesaleQuantity = 10.0m,
                WholesalePrice = 12.50m,
                Stock = 100.0m,
                MinStock = 5.0m,
                MeasurementUnit = "Kilogramo"
            },
            new ProductDto
            {
                Id = Guid.NewGuid(),
                BarCode = "87654321",
                Description = "Product 2",
                Currency = "DOP",
                Cost = 20.00m,
                Price = 30.00m,
                WholesaleQuantity = 5.0m,
                WholesalePrice = 25.00m,
                Stock = 50.0m,
                MinStock = 10.0m,
                MeasurementUnit = "Litro"
            }
        };

        _productQueriesMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProducts);

        var query = new GetAllProductsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
        result.ShouldBe(expectedProducts);
        
        _productQueriesMock.Verify(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ShouldReturnEmptyListWhenNoProductsExist()
    {
        // Arrange
        var emptyProductList = new List<ProductDto>();

        _productQueriesMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyProductList);

        var query = new GetAllProductsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(0);
        result.ShouldBeEmpty();
        
        _productQueriesMock.Verify(
            x => x.GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}