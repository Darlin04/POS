using Blocks.Domain.Exceptions;
using Blocks.Domain.Guards;

namespace Blocks.Domain.ValueObjects;

public sealed record Itbis
{
    public decimal Percentage { get; }

    public Itbis(decimal percentage)
    {
        Guard.AgainstMoreThanTwoDecimals(percentage,"Percentage");
        Guard.AgainstNegativeDecimal(percentage, "Percentage");

        if (percentage > 0.18m)
        {
            throw new DomainException("El Itbis no puede ser mayor a 0.18");
        }
    }
    
    
};