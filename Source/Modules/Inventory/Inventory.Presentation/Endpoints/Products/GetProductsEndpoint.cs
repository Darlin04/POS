using Inventory.Application.Features.Products;
using Inventory.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Presentation.Endpoints.Products;

public static class GetProductsEndpoint
{
    public static IEndpointRouteBuilder MapGetProductsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await sender.Send(new GetAllProductsQuery(), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .WithTags("Products")
        .Produces<List<ProductDto>>()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }
}