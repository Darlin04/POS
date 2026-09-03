using Inventory.Domain.Products;
using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductQueries _queries;

    public GetAllProductsQueryHandler(IProductQueries queries)
    {
        _queries = queries;
    }
    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}