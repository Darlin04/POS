using FluentValidation;

namespace Inventory.Application.Features.MeasurementUnits.Commands.DeleteMeasurementUnit;

public class DeleteMeasurementUnitCommandValidator
    : AbstractValidator<DeleteMeasurementUnitCommand>
{
    public DeleteMeasurementUnitCommandValidator()
    {
        RuleFor(mu => mu.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("El id es obligatorio");
    }
}