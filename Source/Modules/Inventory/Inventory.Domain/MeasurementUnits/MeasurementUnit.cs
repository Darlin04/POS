using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Inventory.Domain.MeasurementUnits.ValueObjects;

namespace Inventory.Domain.MeasurementUnits;

public class MeasurementUnit : AggregateRoot
{
    public MeasurementUnitName MeasurementUnitName { get; private set; }
    public MeasurementUnitAbbreviation MeasurementUnitAbbreviation { get; private set; }
    
    private MeasurementUnit() { }

    public MeasurementUnit(MeasurementUnitName measurementUnitName, MeasurementUnitAbbreviation measurementUnitAbbreviation)
    {
        MeasurementUnitName = measurementUnitName;
        MeasurementUnitAbbreviation = measurementUnitAbbreviation;
        Id = Guid.CreateVersion7();
    }

    public void Update(MeasurementUnitName measurementUnitName, MeasurementUnitAbbreviation measurementUnitAbbreviation)
    {
        if (measurementUnitName == this.MeasurementUnitName &&
            measurementUnitAbbreviation == this.MeasurementUnitAbbreviation)
            return;
        
        MeasurementUnitName = measurementUnitName;
        MeasurementUnitAbbreviation = measurementUnitAbbreviation;
    }
}