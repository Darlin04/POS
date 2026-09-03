using Blocks.Domain.Abstractions;

namespace Inventory.Domain.Products;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<bool> MeasurementUnitExistsAsync(Guid measurementUnitId);

    Task<bool> BarCodeExistsAsync(string barCode);
}