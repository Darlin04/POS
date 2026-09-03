using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;

namespace Inventory.Tests.Domain.MeasurementUnits;

public class MeasurementUnitTests
{
    [Test]
    public void ShouldCreateMeasurementUnitWhenValuesAreValid()
    {
        const string nameValue = "Meter";
        const string abbreviationValue = "m";
        
        var name = new MeasurementUnitName(nameValue);
        var abbreviation = new MeasurementUnitAbbreviation(abbreviationValue);
        
        var measurementUnit = new MeasurementUnit(name, abbreviation);
        
        Assert.That(measurementUnit.MeasurementUnitName, Is.EqualTo(name));
        Assert.That(measurementUnit.MeasurementUnitAbbreviation, Is.EqualTo(abbreviation));
    }

    [Test]
    public void ShouldThrowWhenValuesAreInvalid()
    {
        Assert.Throws<ArgumentException>(() => new MeasurementUnit(new MeasurementUnitName(string.Empty), new MeasurementUnitAbbreviation(string.Empty)));
    }
}