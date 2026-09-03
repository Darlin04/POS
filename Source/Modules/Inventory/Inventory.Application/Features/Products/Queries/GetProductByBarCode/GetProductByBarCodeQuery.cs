using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetProductByBarCode;

public record GetProductByBarCodeQuery(string BarCode) : IRequest<ProductDto>;