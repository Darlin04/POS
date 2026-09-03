using Inventory.Application.Features.MeasurementUnits.Commands.DeleteMeasurementUnit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Presentation.Endpoints.MeasurementUnits;

public static class DeleteMeasurementUnitEndpoint
{
    public static IEndpointRouteBuilder MapDeleteMeasurementUnitEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/measurement-units/{id:guid}", async (
            [FromRoute] Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new DeleteMeasurementUnitCommand(id);
            await sender.Send(query, cancellationToken);
            return Results.NoContent();
        })
        .WithName("DeleteMeasurementUnit")
        .WithTags("MeasurementUnits")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }
}