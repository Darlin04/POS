using Blocks.Domain.Enums;
using Blocks.Domain.Exceptions;
using Blocks.Domain.ValueObjects;
using Inventory.Domain.Products;
using Inventory.Domain.Products.ValueObjects;

namespace Inventory.Tests.Domain.Products;

[TestFixture]
public class ProductTests
{
    private BarCode _validBarCode;
    private Money _validCost;
    private Money _validPrice;
    private Quantity _validWholesaleQuantity;
    private Money _validWholesalePrice;
    private Stock _validStock;
    private Stock _validMinStock;

    [SetUp]
    public void SetUp()
    {
        _validBarCode = new BarCode("1234567890123");
        _validCost = new Money(10.0m, Currency.DOP);
        _validPrice = new Money(15.0m, Currency.DOP);
        _validWholesaleQuantity = new Quantity(10);
        _validWholesalePrice = new Money(12.0m, Currency.DOP);
        _validStock = new Stock(100);
        _validMinStock = new Stock();
    }

    [Test]
    public void Constructor_WithValidParameters_ShouldCreateProduct()
    {
        // Arrange
        var description = "Test Product";
        Guid measurementUnitId = Guid.CreateVersion7();
        
        // Act
        var product = new Product(_validBarCode, description, _validCost, _validPrice, 
            _validWholesaleQuantity, _validWholesalePrice, _validStock, _validMinStock, measurementUnitId);
        
        // Assert
        Assert.That(product.BarCode, Is.EqualTo(_validBarCode));
        Assert.That(product.Description, Is.EqualTo(description));
        Assert.That(product.Cost, Is.EqualTo(_validCost));
        Assert.That(product.Price, Is.EqualTo(_validPrice));
        Assert.That(product.WholesaleQuantity, Is.EqualTo(_validWholesaleQuantity));
        Assert.That(product.WholesalePrice, Is.EqualTo(_validWholesalePrice));
        Assert.That(product.Stock, Is.EqualTo(_validStock));
        Assert.That(product.MinStock, Is.EqualTo(_validMinStock));
        Assert.That(product.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void Constructor_WithInvalidDescription_ShouldThrowArgumentException(string invalidDescription)
    {
        // Arrange
        Guid measurementUnitId = Guid.CreateVersion7();
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Product(
            _validBarCode, invalidDescription, _validCost, _validPrice, 
            _validWholesaleQuantity, _validWholesalePrice, _validStock,_validMinStock, measurementUnitId));
        
        Assert.That(exception.Message, Does.Contain("description"));
    }

    [Test]
    public void Constructor_WithCostGreaterOrEqualPrice_ShouldThrowDomainException()
    {
        // Arrange
        var cost = new Money(15.0m, Currency.DOP);
        var price = new Money(15.0m, Currency.DOP);
        Guid measurementUnitId = Guid.CreateVersion7();
        
        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => new Product(
            _validBarCode, "Test Product", cost, price, 
            _validWholesaleQuantity, _validWholesalePrice, _validStock,_validMinStock, measurementUnitId));
        
        Assert.That(exception.Message, Does.Contain("precio debe ser mayor que el costo"));
    }

    [Test]
    public void Constructor_WithWholesalePriceGreaterThanPrice_ShouldThrowDomainException()
    {
        // Arrange
        var wholesalePrice = new Money(20.0m, Currency.DOP);
        Guid measurementUnitId = Guid.CreateVersion7();
        
        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => new Product(
            _validBarCode, "Test Product", _validCost, _validPrice, 
            _validWholesaleQuantity, wholesalePrice, _validStock,_validMinStock, measurementUnitId));
        
        Assert.That(exception.Message, Does.Contain("precio al por mayor no puede ser mayor al precio regular"));
    }
 
}