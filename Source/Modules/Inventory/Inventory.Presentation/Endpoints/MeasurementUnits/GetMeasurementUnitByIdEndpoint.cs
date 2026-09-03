using Inventory.Application.Features.MeasurementUnits.Queries;
using Inventory.Application.Features.MeasurementUnits.Queries.GetMeasurementUnitById;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Presentation.Endpoints.MeasurementUnits;

public static class GetMeasurementUnitByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetMeasurementUnitByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/measurement-units/{id:guid}", async (
            [FromRoute] Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMeasurementUnitByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetMeasurementUnitById")
        .WithTags("MeasurementUnits")
        .Produces<MeasurementUnitDto>()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
        
        return app;
    }
}