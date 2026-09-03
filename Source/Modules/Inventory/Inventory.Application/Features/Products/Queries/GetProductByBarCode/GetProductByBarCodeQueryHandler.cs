using Blocks.Application.Exceptions;
using MediatR;

namespace Inventory.Application.Features.Products.Queries.GetProductByBarCode;

public class GetProductByBarCodeQueryHandler : IRequestHandler<GetProductByBarCodeQuery, ProductDto>
{
    private readonly IProductQueries _queries;

    public GetProductByBarCodeQueryHandler(IProductQueries queries)
    {
        _queries = queries;
    }
    
    public async Task<ProductDto> Handle(GetProductByBarCodeQuery request, CancellationToken cancellationToken)
    {
        var productDto = await _queries.GetByBarCodeAsync(request.BarCode, cancellationToken);
        return productDto ?? throw new NotFoundException($"No existe el producto con el código de barras {request.BarCode}");
    }
}