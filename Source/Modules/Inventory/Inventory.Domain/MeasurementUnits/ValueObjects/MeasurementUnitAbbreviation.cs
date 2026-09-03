using Blocks.Domain.Guards;
using Blocks.Domain.ValueObjects;

namespace Inventory.Domain.MeasurementUnits.ValueObjects;

public sealed record MeasurementUnitAbbreviation : StringValueObject
{
    public MeasurementUnitAbbreviation(string value) : base(value)
    {
        Guard.AgainstNullOrWhiteSpace(value, nameof(MeasurementUnitName));
        Guard.AgainstMaxLength(value, 3, nameof(MeasurementUnitName));
    }
}