using MediatR;

namespace Inventory.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string BarCode,
    string Description,
    decimal Cost,
    decimal Price,
    decimal WholesaleQuantity,
    decimal WholesalePrice,
    decimal Stock,
    decimal MinStock,
    Guid MeasurementUnitId
) : IRequest<ProductDto>;