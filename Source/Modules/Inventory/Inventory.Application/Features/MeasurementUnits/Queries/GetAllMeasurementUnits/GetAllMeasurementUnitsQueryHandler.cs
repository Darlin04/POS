using Inventory.Domain.MeasurementUnits;
using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Queries.GetAllMeasurementUnits;

public class GetAllMeasurementUnitsQueryHandler : IRequestHandler<GetAllMeasurementUnitsQuery, List<MeasurementUnitDto>>
{
    private readonly IMeasurementUnitRepository _measurementUnitRepository;

    public GetAllMeasurementUnitsQueryHandler(IMeasurementUnitRepository measurementUnitRepository)
    {
        _measurementUnitRepository = measurementUnitRepository;
    }
    public async Task<List<MeasurementUnitDto>> Handle(GetAllMeasurementUnitsQuery request, CancellationToken cancellationToken)
    {
        var measurementUnits = await _measurementUnitRepository.GetAllAsync(cancellationToken);

        return measurementUnits.Select(measurementUnit =>
            new MeasurementUnitDto
            { 
                Id = measurementUnit.Id,
                Name = measurementUnit.MeasurementUnitName,
                Abbreviation = measurementUnit.MeasurementUnitAbbreviation
            }
        ).ToList();
    }
}