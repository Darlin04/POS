using Inventory.Application.Features.Products;
using Inventory.Application.Features.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Presentation.Endpoints.Products;

public static class CreateProductEndpoint
{
    public static IEndpointRouteBuilder MapCreateProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (
            [FromBody] CreateProductCommand command,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await sender.Send(command, cancellationToken);
            return Results.Created($"products/{result.Id}", result);
        })
        .WithName("CreateProduct")
        .WithTags("Products")
        .Produces<ProductDto>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }
}