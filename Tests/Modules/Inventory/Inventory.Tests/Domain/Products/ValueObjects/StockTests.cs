using Blocks.Domain.Exceptions;
using Inventory.Domain.Products.ValueObjects;

namespace Inventory.Tests.Domain.Products.ValueObjects;

[TestFixture]
public class StockTests
{
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10.5)]
    [TestCase(100.99)]
    [TestCase(999999.99)]
    public void Constructor_WithValidStock_ShouldCreateInstance(decimal validStock)
    {
        // Act
        var stock = new Stock(validStock);
        
        // Assert
        Assert.That(stock.Value, Is.EqualTo(validStock));
    }

    [TestCase(-0.01)]
    [TestCase(-1)]
    [TestCase(-10.5)]
    [TestCase(-100)]
    public void Constructor_WithNegativeStock_ShouldThrowArgumentException(decimal invalidStock)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Stock(invalidStock));
        Assert.That(exception.Message, Does.Contain("stock"));
    }

    [TestCase(1.001)]
    [TestCase(10.123)]
    [TestCase(100.999)]
    [TestCase(1.0001)]
    public void Constructor_WithMoreThanTwoDecimals_ShouldThrowDomainException(decimal invalidStock)
    {
        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => new Stock(invalidStock));
        Assert.That(exception.Message, Does.Contain("Stock"));
    }

    [Test]
    public void Constructor_WithDefaultValue_ShouldCreateStockWithZero()
    {
        // Act
        var stock = new Stock();
        
        // Assert
        Assert.That(stock.Value, Is.EqualTo(0));
    }

    [Test]
    public void Increase_ShouldAddQuantityAndReturnNewStock()
    {
        // Arrange
        var initialStock = new Stock(10.5m);
        var quantity = new Quantity(5.25m);
        
        // Act
        var result = initialStock.Increase(quantity);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(15.75m));
        Assert.That(initialStock.Value, Is.EqualTo(10.5m));
    }

    [Test]
    public void Decrease_WithSufficientStock_ShouldSubtractQuantityAndReturnNewStock()
    {
        // Arrange
        var initialStock = new Stock(20.5m);
        var quantity = new Quantity(5.25m);
        
        // Act
        var result = initialStock.Decrease(quantity);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(15.25m));
        Assert.That(initialStock.Value, Is.EqualTo(20.5m));
    }

    [Test]
    public void Decrease_WithInsufficientStock_ShouldThrowDomainException()
    {
        // Arrange
        var stock = new Stock(5.0m);
        var quantity = new Quantity(10.0m);
        
        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => stock.Decrease(quantity));
        Assert.That(exception.Message, Does.Contain("Stock insuficiente"));
    }

    [Test]
    public void Decrease_WithExactStock_ShouldCreateZeroStock()
    {
        // Arrange
        var stock = new Stock(10.0m);
        var quantity = new Quantity(10.0m);
        
        // Act
        var result = stock.Decrease(quantity);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(0));
    }

    [Test]
    public void Stock_ShouldBeRecordType()
    {
        // Arrange
        var stock1 = new Stock(10.5m);
        var stock2 = new Stock(10.5m);
        var stock3 = new Stock(15.75m);
        
        // Act & Assert
        Assert.That(stock1, Is.EqualTo(stock2));
        Assert.That(stock1, Is.Not.EqualTo(stock3));
        Assert.That(stock1 == stock2, Is.True);
        Assert.That(stock1 != stock3, Is.True);
    }

    [Test]
    public void Stock_ToString_ShouldReturnValue()
    {
        // Arrange
        var value = 10.5m;
        var stock = new Stock(value);
        
        // Act
        var result = stock.ToString();
        
        // Assert
        Assert.That(result, Is.EqualTo(value.ToString()));
    }
}