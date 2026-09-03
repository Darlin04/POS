using Blocks.Domain.Guards;

namespace Inventory.Domain.Products.ValueObjects;

public record Quantity
{
    public decimal Value { get; }

    public Quantity(decimal value)
    {
        Guard.AgainstNegativeDecimal(value, "cantidad");
        Guard.AgainstMoreThanTwoDecimals(value, "Cantidad");

        Value = value;
    }
    
    public sealed override string ToString() => $"{Value}";
}