using Blocks.Domain.Guards;
using Blocks.Domain.ValueObjects;

namespace Inventory.Domain.MeasurementUnits.ValueObjects;

public sealed record MeasurementUnitName : StringValueObject
{
    public MeasurementUnitName(string value) : base(value)
    {
        Guard.AgainstNullOrWhiteSpace(value, nameof(MeasurementUnitName));
        Guard.AgainstMaxLength(value, 20, nameof(MeasurementUnitName));
    }
}