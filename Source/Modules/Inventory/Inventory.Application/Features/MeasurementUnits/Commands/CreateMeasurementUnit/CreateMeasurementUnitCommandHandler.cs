using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Inventory.Application.Features.MeasurementUnits.Queries;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;
using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Commands.CreateMeasurementUnit;

public class CreateMeasurementUnitCommandHandler : IRequestHandler<CreateMeasurementUnitCommand, MeasurementUnitDto>
{
    private readonly IMeasurementUnitRepository _measurementUnitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMeasurementUnitCommandHandler(IMeasurementUnitRepository measurementUnitRepository, IUnitOfWork unitOfWork)
    {
        _measurementUnitRepository = measurementUnitRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<MeasurementUnitDto> Handle(CreateMeasurementUnitCommand command, CancellationToken cancellationToken)
    {
        if (await _measurementUnitRepository.ExistsByNameAsync(command.Name, null, cancellationToken))
            throw new DomainException("Ya existe una unidad de medida con ese nombre");
        
        if (await _measurementUnitRepository.ExistsByAbbreviationAsync(command.Abbreviation, null, cancellationToken))
            throw new DomainException("Ya existe una unidad de medida con esa abreviatura");

        var measurementUnit = new MeasurementUnit(new MeasurementUnitName(command.Name), new MeasurementUnitAbbreviation(command.Abbreviation));

        await _measurementUnitRepository.AddAsync(measurementUnit, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new MeasurementUnitDto
        {
            Id = measurementUnit.Id,
            Name = measurementUnit.MeasurementUnitName,
            Abbreviation = measurementUnit.MeasurementUnitAbbreviation
        };
    }
}