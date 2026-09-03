using Blocks.Domain.Exceptions;
using Inventory.Domain.Products.ValueObjects;

namespace Inventory.Tests.Domain.Products.ValueObjects;

[TestFixture]
public class BarCodeTests
{
    [TestCase("12345678")]     
    [TestCase("123456789012")] 
    [TestCase("1234567890123")]
    [TestCase("12345678901234")]
    public void Constructor_WithValidBarCode_ShouldCreateInstance(string validBarCode)
    {
        // Act
        var barCode = new BarCode(validBarCode);
        
        // Assert
        Assert.That(barCode.Value, Is.EqualTo(validBarCode));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void Constructor_WithNullOrWhiteSpace_ShouldThrowArgumentException(string invalidBarCode)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new BarCode(invalidBarCode));
        Assert.That(exception.Message, Does.Contain("código de barras"));
    }

    [TestCase("1234567A")] 
    [TestCase("12345678-")]
    [TestCase("123 45678")]
    [TestCase("ABC12345")] 
    public void Constructor_WithNonDigits_ShouldThrowDomainException(string invalidBarCode)
    {
        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => new BarCode(invalidBarCode));
        Assert.That(exception.Message, Does.Contain("solo dígitos"));
    }

    [TestCase("1234567")]      
    [TestCase("123456789")]    
    [TestCase("12345678901")]  
    [TestCase("123456789012345")]
    public void Constructor_WithInvalidLength_ShouldThrowDomainException(string invalidBarCode)
    {
        // Act & Assert
        var exception = Assert.Throws<DomainException>(() => new BarCode(invalidBarCode));
        Assert.That(exception.Message, Does.Contain("longitud"));
        Assert.That(exception.Message, Does.Contain("8, 12, 13 o 14"));
    }

    [Test]
    public void BarCode_ShouldBeRecordType()
    {
        // Arrange
        var barCode1 = new BarCode("1234567890123");
        var barCode2 = new BarCode("1234567890123");
        var barCode3 = new BarCode("1234567890124");
        
        // Act & Assert
        Assert.That(barCode1, Is.EqualTo(barCode2));
        Assert.That(barCode1, Is.Not.EqualTo(barCode3));
        Assert.That(barCode1 == barCode2, Is.True);
        Assert.That(barCode1 != barCode3, Is.True);
    }

    [Test]
    public void BarCode_ToString_ShouldReturnValue()
    {
        // Arrange
        const string value = "1234567890123";
        var barCode = new BarCode(value);
        
        // Act
        var result = barCode.ToString();
        
        // Assert
        Assert.That(result, Is.EqualTo(value));
    }
}