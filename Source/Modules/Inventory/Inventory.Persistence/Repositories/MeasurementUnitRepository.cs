using Inventory.Domain.MeasurementUnits;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Persistence.Repositories;

public class MeasurementUnitRepository(InventoryDbContext dbContext)
    : InventoryRepository<MeasurementUnit>(dbContext), IMeasurementUnitRepository
{
    public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId, CancellationToken cancellationToken)
    {
        return await Query().AnyAsync(mu => mu.MeasurementUnitName.Value == name && 
                                      (!excludeId.HasValue || mu.Id != excludeId.Value),
                                       cancellationToken);
    }

    public async Task<bool> ExistsByAbbreviationAsync(string abbreviation, Guid? excludeId, CancellationToken cancellationToken = default)
    {
        return await Query().AnyAsync(mu => mu.MeasurementUnitAbbreviation.Value == abbreviation &&
                                      (!excludeId.HasValue || mu.Id != excludeId.Value),
                                       cancellationToken);
    }

    public async Task<List<MeasurementUnit>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Query().ToListAsync(cancellationToken);
    }

    public void Remove(MeasurementUnit measurementUnit)
    {
        Context.Remove(measurementUnit);
    }
}