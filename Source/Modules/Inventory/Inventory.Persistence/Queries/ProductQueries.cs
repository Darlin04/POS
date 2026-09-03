using Inventory.Application.Features.Products;
using Inventory.Application.Features.Products.Queries;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Persistence.Queries;

public class ProductQueries(InventoryDbContext dbContext) : IProductQueries
{
    public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await (
            from p in dbContext.Products.AsNoTracking()
            join mu in dbContext.MeasurementUnits
                on p.MeasurementUnitId equals mu.Id
            select new ProductDto
            {
                Id = p.Id,
                BarCode = p.BarCode.Value,
                Description =  p.Description,
                Currency = p.Cost.Currency.ToString(),
                Cost = p.Cost.Amount,
                Price = p.Price.Amount,
                WholesaleQuantity = p.WholesaleQuantity.Value,
                WholesalePrice = p.WholesalePrice.Amount,
                Stock = p.Stock.Value,
                MinStock = p.MinStock.Value,
                MeasurementUnit = mu.MeasurementUnitName.Value
            }
        ).ToListAsync(cancellationToken);
    }

    public async Task<ProductDto?> GetByBarCodeAsync(string barCode, CancellationToken cancellationToken = default)
    {
        return await (
            from p in dbContext.Products.AsNoTracking()
            join mu in dbContext.MeasurementUnits
                on p.MeasurementUnitId equals mu.Id
            where p.BarCode.Value == barCode
            select new ProductDto
            {
                Id = p.Id,
                BarCode = p.BarCode.Value,
                Description =  p.Description,
                Currency = p.Cost.Currency.ToString(),
                Cost = p.Cost.Amount,
                Price = p.Price.Amount,
                WholesaleQuantity = p.WholesaleQuantity.Value,
                WholesalePrice = p.WholesalePrice.Amount,
                Stock = p.Stock.Value,
                MinStock = p.MinStock.Value,
                MeasurementUnit = mu.MeasurementUnitName.Value
            }
        ).FirstOrDefaultAsync(cancellationToken);
    }
}