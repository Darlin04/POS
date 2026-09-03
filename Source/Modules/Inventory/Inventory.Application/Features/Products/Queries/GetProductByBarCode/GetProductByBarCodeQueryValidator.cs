using FluentValidation;

namespace Inventory.Application.Features.Products.Queries.GetProductByBarCode;

public class GetProductByBarCodeQueryValidator
    : AbstractValidator<GetProductByBarCodeQuery>
{
    public GetProductByBarCodeQueryValidator()
    {
        RuleFor(p => p.BarCode)
            .NotEmpty()
            .WithMessage("El código de barras es requerido")
            .Must(barcode => barcode.All(char.IsDigit))
            .WithMessage("El código de barras debe contener solo dígitos")
            .Must(barcode => new[] { 8, 12, 13, 14 }.Contains(barcode.Length))
            .WithMessage("La longitud del código de barras debe ser 8, 12, 13 o 14 dígitos");
    }
}