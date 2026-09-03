using Blocks.Domain.Enums;
using Blocks.Domain.Guards;

namespace Blocks.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public Currency Currency { get; } = Currency.DOP;
    
    private Money() {}

    public Money(decimal amount, Currency currency = Currency.DOP)
    {
        Guard.AgainstNegativeDecimal(amount, "monto");
        Guard.AgainstMoreThanTwoDecimals(amount, "monto");

        Amount = amount;
        Currency = currency;
    }
}