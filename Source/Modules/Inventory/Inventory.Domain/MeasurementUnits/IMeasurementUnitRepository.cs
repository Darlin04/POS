using Blocks.Domain.Abstractions;

namespace Inventory.Domain.MeasurementUnits;

public interface IMeasurementUnitRepository : IGenericRepository<MeasurementUnit>
{
    Task<bool> ExistsByNameAsync(string name, Guid? excludeId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByAbbreviationAsync(string abbreviation, Guid? excludeId, CancellationToken cancellationToken = default);
    Task<List<MeasurementUnit>> GetAllAsync(CancellationToken cancellationToken = default);
    void Remove(MeasurementUnit measurementUnit);
}