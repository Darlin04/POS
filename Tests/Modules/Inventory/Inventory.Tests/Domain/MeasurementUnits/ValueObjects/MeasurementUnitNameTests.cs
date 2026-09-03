using Inventory.Domain.MeasurementUnits.ValueObjects;

namespace Inventory.Tests.Domain.MeasurementUnits.ValueObjects;

public class MeasurementUnitNameTests
{
    [Test]
    public void ShouldCreateNameWhenValueIsValid()
    {
        const string value = "Meter";
        var name = new MeasurementUnitName(value);
        Assert.That(name.Value, Is.EqualTo(value));
    }

    [Test]
    public void ShouldThrowWhenValueIsNull()
    {
        Assert.Throws<ArgumentException>(() => new MeasurementUnitName(null!));
    }

    [Test]
    public void ShouldThrowWhenValueIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => new MeasurementUnitName(string.Empty));
    }
    
    [Test]
    public void ShouldThrowWhenValueIsWhiteSpace()
    {
        Assert.Throws<ArgumentException>(() => new MeasurementUnitName("   "));
    }
    
    [Test]
    public void ShouldThrowWhenValueExceedsMaxLength()
    {
        var value = new string('A', 21);

        Assert.Throws<ArgumentException>(() => new MeasurementUnitName(value));
    }
    
    [Test]
    public void ShouldAllowValueWithExactly20Characters()
    {
        var value = new string('A', 20);

        var name = new MeasurementUnitName(value);

        Assert.That(name.Value, Is.EqualTo(value));
    }
}