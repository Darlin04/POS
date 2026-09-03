using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<List<ProductDto>>;