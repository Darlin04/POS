using Blocks.Domain.Exceptions;
using Inventory.Domain.Products.ValueObjects;

namespace Inventory.Tests.Domain.Products.ValueObjects;

[TestFixture]
public class QuantityTests
{
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10.5)]
    [TestCase(100.99)]
    [TestCase(999999.99)]
    public void Constructor_WithValidQuantity_ShouldCreateInstance(decimal validQuantity)
    {
        // Act
        var quantity = new Quantity(validQuantity);
        
        // Assert
        Assert.That(quantity.Value, Is.EqualTo(validQuantity));
    }

    [TestCase(-0.01)]
    [TestCase(-1)]
    [TestCase(-10.5)]
    [TestCase(-100)]
    public void Constructor_WithNegativeQuantity_ShouldThrowArgumentException(decimal invalidQuantity)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Quantity(invalidQuantity));
        Assert.That(exception.Message, Does.Contain("cantidad"));
    }

    [TestCase(1.001)]
    [TestCase(10.123)]
    [TestCase(100.999)]
    [TestCase(1.0001)]
    public void Constructor_WithMoreThanTwoDecimals_ShouldThrowDomainException(decimal invalidQuantity)
    {
        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => new Quantity(invalidQuantity));
        Assert.That(exception.Message, Does.Contain("Cantidad"));
    }

    [Test]
    public void Quantity_ShouldBeRecordType()
    {
        // Arrange
        var quantity1 = new Quantity(10.5m);
        var quantity2 = new Quantity(10.5m);
        var quantity3 = new Quantity(15.75m);
        
        // Act & Assert
        Assert.That(quantity1, Is.EqualTo(quantity2));
        Assert.That(quantity1, Is.Not.EqualTo(quantity3));
        Assert.That(quantity1 == quantity2, Is.True);
        Assert.That(quantity1 != quantity3, Is.True);
    }

    [Test]
    public void Quantity_ToString_ShouldReturnValue()
    {
        // Arrange
        var value = 10.5m;
        var quantity = new Quantity(value);
        
        // Act
        var result = quantity.ToString();
        
        // Assert
        Assert.That(result, Is.EqualTo(value.ToString()));
    }

    [Test]
    public void Quantity_WithZero_ShouldBeValid()
    {
        // Arrange & Act
        var quantity = new Quantity(0);
        
        // Assert
        Assert.That(quantity.Value, Is.EqualTo(0));
    }

    [Test]
    public void Quantity_WithExactlyTwoDecimals_ShouldBeValid()
    {
        // Arrange
        var value = 10.99m;
        
        // Act
        var quantity = new Quantity(value);
        
        // Assert
        Assert.That(quantity.Value, Is.EqualTo(value));
    }
}