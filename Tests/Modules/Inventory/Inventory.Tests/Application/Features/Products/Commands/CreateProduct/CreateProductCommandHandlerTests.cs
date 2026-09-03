using AutoMapper;
using Blocks.Application.Exceptions;
using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Inventory.Application.Features.Products;
using Inventory.Application.Features.Products.Commands.CreateProduct;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;
using Inventory.Domain.Products;
using Moq;
using Shouldly;

namespace Inventory.Tests.Application.Features.Products.Commands.CreateProduct;
    
[TestFixture]
public class CreateProductCommandHandlerTests
{
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<IMeasurementUnitRepository> _measurementUnitRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMapper> _mapperMock;
    private CreateProductCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _measurementUnitRepositoryMock = new Mock<IMeasurementUnitRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        
        _handler = new CreateProductCommandHandler(
            _productRepositoryMock.Object,
            _measurementUnitRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object);
    }

    [Test]
    public async Task ShouldCreateProductWhenDataIsValid()
    {
        // Arrange
        var command = new CreateProductCommand(
            "12345678",
            "Test Product",
            10.50m,
            15.75m,
            10.0m,
            12.50m,
            100.0m,
            5.0m,
            Guid.CreateVersion7());
        
        var measurementUnitId = command.MeasurementUnitId;
        var expectedProductDto = new ProductDto
        {
            Id = Guid.CreateVersion7(),
            BarCode = command.BarCode,
            Description = command.Description,
            Cost = command.Cost,
            Price = command.Price,
            WholesaleQuantity = command.WholesaleQuantity,
            WholesalePrice = command.WholesalePrice,
            Stock = command.Stock,
            MinStock = command.MinStock,
            MeasurementUnit = "kg"
        };

        _productRepositoryMock
            .Setup(x => x.MeasurementUnitExistsAsync(measurementUnitId))
            .ReturnsAsync(true);
            
        _productRepositoryMock
            .Setup(x => x.BarCodeExistsAsync(command.BarCode))
            .ReturnsAsync(false);
            
        _productRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            
        _unitOfWorkMock
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
            
        _mapperMock
            .Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
            .Returns(expectedProductDto);
            
        var measurementUnit = new MeasurementUnit(new MeasurementUnitName("Kilogramo"), new MeasurementUnitAbbreviation("kg"));
        _measurementUnitRepositoryMock
            .Setup(x => x.GetByIdAsync(measurementUnitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(measurementUnit);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(expectedProductDto);
        result.MeasurementUnit.ShouldBe("Kilogramo");
        
        _productRepositoryMock.Verify(
            x => x.AddAsync(It.Is<Product>(p => 
                p.BarCode.Value == command.BarCode &&
                p.Description == command.Description),
                It.IsAny<CancellationToken>()),
            Times.Once);
            
        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Once);
            
        _measurementUnitRepositoryMock.Verify(
            x => x.GetByIdAsync(measurementUnitId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public void ShouldThrowNotFoundExceptionWhenMeasurementUnitDoesNotExist()
    {
        // Arrange
        var command = new CreateProductCommand(
            "123456789",
            "Test Product",
            10.50m,
            15.75m,
            10.0m,
            12.50m,
            100.0m,
            5.0m,
            Guid.NewGuid());
        
        _productRepositoryMock
            .Setup(x => x.MeasurementUnitExistsAsync(command.MeasurementUnitId))
            .ReturnsAsync(false);

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
        
        _productRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), 
            Times.Never);
            
        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }

    [Test]
    public void ShouldThrowDomainExceptionWhenBarCodeAlreadyExists()
    {
        // Arrange
        var command = new CreateProductCommand(
            "123456789",
            "Test Product",
            10.50m,
            15.75m,
            10.0m,
            12.50m,
            100.0m,
            5.0m,
            Guid.NewGuid());
        
        _productRepositoryMock
            .Setup(x => x.MeasurementUnitExistsAsync(command.MeasurementUnitId))
            .ReturnsAsync(true);
            
        _productRepositoryMock
            .Setup(x => x.BarCodeExistsAsync(command.BarCode))
            .ReturnsAsync(true);

        // Act & Assert
        Assert.ThrowsAsync<DomainException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
        
        _productRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), 
            Times.Never);
            
        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }
}