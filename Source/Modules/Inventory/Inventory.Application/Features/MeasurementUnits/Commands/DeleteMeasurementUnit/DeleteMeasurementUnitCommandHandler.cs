using Blocks.Domain.Abstractions;
using Blocks.EntityFramework;
using Inventory.Domain.MeasurementUnits;
using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Commands.DeleteMeasurementUnit;

public class DeleteMeasurementUnitCommandHandler : IRequestHandler<DeleteMeasurementUnitCommand>
{
    private readonly IMeasurementUnitRepository _measurementUnitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMeasurementUnitCommandHandler(IMeasurementUnitRepository measurementUnitRepository, IUnitOfWork unitOfWork)
    {
        _measurementUnitRepository = measurementUnitRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteMeasurementUnitCommand command, CancellationToken cancellationToken)
    {
        // todo - verify the measurement unit doesn't have products
        
        var measurementUnit = await _measurementUnitRepository.FindByIdOrThrowAsync(command.Id, "unidad de medida", cancellationToken);

        _measurementUnitRepository.Remove(measurementUnit);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}