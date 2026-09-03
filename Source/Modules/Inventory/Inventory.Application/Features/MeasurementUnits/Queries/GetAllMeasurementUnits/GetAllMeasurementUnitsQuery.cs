using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Queries.GetAllMeasurementUnits;

public record GetAllMeasurementUnitsQuery : IRequest<List<MeasurementUnitDto>>;