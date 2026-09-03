namespace Inventory.Application.Features.MeasurementUnits.Queries;

public sealed record MeasurementUnitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
}