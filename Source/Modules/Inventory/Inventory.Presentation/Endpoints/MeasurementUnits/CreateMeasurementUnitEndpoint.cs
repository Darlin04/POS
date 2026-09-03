using Inventory.Application.Features.MeasurementUnits.Commands.CreateMeasurementUnit;
using Inventory.Application.Features.MeasurementUnits.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Presentation.Endpoints.MeasurementUnits;

public static class CreateMeasurementUnitEndpoint
{
    public static IEndpointRouteBuilder MapCreateMeasurementUnitEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/measurement-units", async (
                [FromBody] CreateMeasurementUnitCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);
            return Results.Created($"/measurement-units/{result.Id}", result);
        })
        .WithName("CreateMeasurementUnit")
        .WithTags("MeasurementUnits")
        .Produces<MeasurementUnitDto>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
        
        return app;
    }
}