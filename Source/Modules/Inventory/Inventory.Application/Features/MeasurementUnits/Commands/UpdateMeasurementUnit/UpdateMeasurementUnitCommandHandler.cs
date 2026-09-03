using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Blocks.EntityFramework;
using Inventory.Application.Features.MeasurementUnits.Queries;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;
using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Commands.UpdateMeasurementUnit;

public class UpdateMeasurementUnitCommandHandler : IRequestHandler<UpdateMeasurementUnitCommand, MeasurementUnitDto>
{
    private readonly IMeasurementUnitRepository _measurementUnitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMeasurementUnitCommandHandler(IMeasurementUnitRepository measurementUnitRepository, IUnitOfWork unitOfWork)
    {
        _measurementUnitRepository = measurementUnitRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<MeasurementUnitDto> Handle(UpdateMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        var measurementUnit =
            await _measurementUnitRepository.FindByIdOrThrowAsync(request.Id, "unidad de medida", cancellationToken);
        
        if (await _measurementUnitRepository.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
            throw new DomainException("Ya existe una unidad de medida con ese nombre");
        
        if (await _measurementUnitRepository.ExistsByAbbreviationAsync(request.Abbreviation, request.Id, cancellationToken))
            throw new DomainException("Ya existe una unidad de medida con esa abreviatura");
        
        measurementUnit.Update(new MeasurementUnitName(request.Name), new MeasurementUnitAbbreviation(request.Abbreviation));

        await _unitOfWork.CommitAsync(cancellationToken);

        return new MeasurementUnitDto
        {
            Id = measurementUnit.Id,
            Name = measurementUnit.MeasurementUnitName,
            Abbreviation = measurementUnit.MeasurementUnitAbbreviation
        };
    }
}