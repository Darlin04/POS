using FluentValidation;

namespace Inventory.Application.Features.MeasurementUnits.Commands.UpdateMeasurementUnit;

public class UpdateMeasurementUnitCommandValidator
    : AbstractValidator<UpdateMeasurementUnitCommand>
{
    public UpdateMeasurementUnitCommandValidator()
    {
        RuleFor(mu => mu.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("El id es obligatorio");
        
        RuleFor(mu => mu.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("El nombre es obligatorio")
            .MaximumLength(20)
            .WithMessage("El nombre no puede exceder los 20 caracteres");

        RuleFor(mu => mu.Abbreviation)
            .NotNull()
            .NotEmpty()
            .WithMessage("La abreviatura es obligatoria")
            .MaximumLength(3)
            .WithMessage("La abreviatura no puede exceder los 3 caracteres");
    }
}