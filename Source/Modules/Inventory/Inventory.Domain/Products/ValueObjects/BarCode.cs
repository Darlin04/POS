using Blocks.Domain.Exceptions;
using Blocks.Domain.Guards;
using Blocks.Domain.ValueObjects;

namespace Inventory.Domain.Products.ValueObjects;

public record BarCode : StringValueObject
{
    private static readonly int[] AllowedLengths = [8, 12, 13, 14];
    public BarCode(string value) : base(value)
    {
        Guard.AgainstNullOrWhiteSpace(value, "código de barras");

        if (!value.All(char.IsDigit))
            throw new DomainException("El código de barras debe contener solo dígitos");

        if (!AllowedLengths.Contains(value.Length))
            throw new DomainException("La longitud del código de barras debe ser 8, 12, 13 o 14 dígitos");
    }
}