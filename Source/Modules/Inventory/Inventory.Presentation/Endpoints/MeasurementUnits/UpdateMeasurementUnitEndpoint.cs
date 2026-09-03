using Inventory.Application.Features.MeasurementUnits.Commands.UpdateMeasurementUnit;
using Inventory.Application.Features.MeasurementUnits.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Presentation.Endpoints.MeasurementUnits;

public static class UpdateMeasurementUnitEndpoint
{
    public static IEndpointRouteBuilder MapUpdateMeasurementUnitEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/measurement-units/{id:guid}", async (
            [FromRoute] Guid id,
            [FromBody] UpdateMeasurementUnitCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command with { Id = id }, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("UpdateMeasurementUnit")
        .WithTags("MeasurementUnits")
        .Produces<MeasurementUnitDto>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
        
        return app;
    }
}