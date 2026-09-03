using FluentValidation;

namespace Inventory.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.BarCode)
            .NotEmpty()
            .Must(v => v.Length is 8 or 12 or 13 or 14)
            .WithMessage("La longitud del código de barras debe ser 8, 12, 13 o 14 dígitos")
            .Must(v => v.All(char.IsDigit))
            .WithMessage("El código de barras solo puede contener números");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("La descripción no puede estar vacía");

        RuleFor(p => p.Cost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El costo no puede ser negativo")
            .Must(p => decimal.Round(p, 2) == p)
            .WithMessage("El costo debe tener máximo 2 decimales");
        
        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("El precio debe ser mayor que cero")
            .GreaterThan(p => p.Cost)
            .WithMessage("El precio debe ser mayor que el costo")
            .Must(p => decimal.Round(p, 2) == p)
            .WithMessage("El precio debe tener máximo 2 decimales");
        
        RuleFor(p => p.WholesaleQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("La cantidad al por mayor no puede ser negativa")
            .Must(p => decimal.Round(p, 2) == p)
            .WithMessage("La cantidad al por mayor debe tener máximo 2 decimales");
        
        RuleFor(p => p.WholesalePrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El precio al por mayor no puede ser negativo")
            .LessThanOrEqualTo(p => p.Price)
            .WithMessage("El precio al por mayor no puede ser mayor al precio regular")
            .Must(p => decimal.Round(p, 2) == p)
            .WithMessage("El precio al por mayor debe tener máximo 2 decimales");
        
        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El stock no puede ser negativo")
            .Must(p => decimal.Round(p, 2) == p)
            .WithMessage("El stock debe tener máximo 2 decimales");
        
        RuleFor(p => p.MinStock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El minStock no puede ser negativo")
            .Must(p => decimal.Round(p, 2) == p)
            .WithMessage("El minStock debe tener máximo 2 decimales");
    }
}