namespace Inventory.Application.Features.Products.Queries;

public interface IProductQueries
{
    Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByBarCodeAsync(string barCode, CancellationToken cancellationToken = default);
}