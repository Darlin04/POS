using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Queries.GetMeasurementUnitById;

public record GetMeasurementUnitByIdQuery(Guid Id) : IRequest<MeasurementUnitDto>;