using Blocks.EntityFramework;
using Inventory.Domain.MeasurementUnits;
using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Queries.GetMeasurementUnitById;

public class GetMeasurementUnitByIdQueryHandler : IRequestHandler<GetMeasurementUnitByIdQuery, MeasurementUnitDto>
{
    private readonly IMeasurementUnitRepository _measurementUnitRepository;

    public GetMeasurementUnitByIdQueryHandler(IMeasurementUnitRepository measurementUnitRepository)
    {
        _measurementUnitRepository = measurementUnitRepository;
    }
    public async Task<MeasurementUnitDto> Handle(GetMeasurementUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var measurementUnit = await _measurementUnitRepository.FindByIdOrThrowAsync(request.Id,"unidad de medida", cancellationToken);

        return new MeasurementUnitDto
        {
            Id = measurementUnit.Id,
            Name = measurementUnit.MeasurementUnitName,
            Abbreviation = measurementUnit.MeasurementUnitAbbreviation
        };
    }
}