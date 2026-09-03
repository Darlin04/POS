using Inventory.Application.Features.Products;
using Inventory.Application.Features.Products.Queries.GetProductByBarCode;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Presentation.Endpoints.Products;

public static class GetProductByBarCodeEndpoint
{
    public static IEndpointRouteBuilder MapGetProductByBarCodeEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{barCode}", async (
            [FromRoute] string barCode,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var query = new GetProductByBarCodeQuery(barCode);
            var result = await sender.Send(query, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetProductByBarCode")
        .WithTags("Products")
        .Produces<ProductDto>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }
}