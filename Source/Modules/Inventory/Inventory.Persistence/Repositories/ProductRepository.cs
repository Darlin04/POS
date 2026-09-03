using Inventory.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Persistence.Repositories;

public class ProductRepository(InventoryDbContext dbContext)
    : InventoryRepository<Product>(dbContext), IProductRepository
{
    public async Task<bool> MeasurementUnitExistsAsync(Guid measurementUnitId)
        => await Context.MeasurementUnits.AnyAsync(mu => mu.Id == measurementUnitId);

    public async Task<bool> BarCodeExistsAsync(string barCode)
        => await Query().AnyAsync(p => p.BarCode.Value == barCode);
}