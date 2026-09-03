using Inventory.Domain.MeasurementUnits.ValueObjects;

namespace Inventory.Tests.Domain.MeasurementUnits.ValueObjects;

public class MeasurementUnitAbbreviationTests
{
    [Test]
    public void ShouldCreateAbbreviationWhenValueIsValid()
    {
        const string value = "m";
        var name = new MeasurementUnitAbbreviation(value);
        Assert.That(name.Value, Is.EqualTo(value));
    }

    [Test]
    public void ShouldThrowWhenValueIsNull()
    {
        Assert.Throws<ArgumentException>(() => new MeasurementUnitAbbreviation(null!));
    }

    [Test]
    public void ShouldThrowWhenValueIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new MeasurementUnitAbbreviation(string.Empty));
    }
    
    [Test]
    public void ShouldThrowWhenValueIsWhiteSpace()
    {
        Assert.Throws<ArgumentException>(() => new MeasurementUnitAbbreviation("   "));
    }
    
    [Test]
    public void ShouldThrowWhenValueExceedsMaxLength()
    {
        var value = new string('A', 4);

        Assert.Throws<ArgumentException>(() => new MeasurementUnitAbbreviation(value));
    }
    
    [Test]
    public void ShouldAllowValueWithExactly20Characters()
    {
        var value = new string('A', 3);

        var name = new MeasurementUnitAbbreviation(value);

        Assert.That(name.Value, Is.EqualTo(value));
    }
}