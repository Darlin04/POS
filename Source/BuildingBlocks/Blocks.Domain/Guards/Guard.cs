using System.Text.RegularExpressions;
using Blocks.Domain.Exceptions;

namespace Blocks.Domain.Guards;

public static partial class Guard
{
    public static void AgainstNull(object? value, string propertyName)
    {
        if (value is null)
            throw new ArgumentException($"{propertyName} no puede estar vacío ni ser espacios en blanco", propertyName);
    }
    
    public static void AgainstNullOrWhiteSpace(string? value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{propertyName} no puede estar vacío ni ser espacios en blanco", propertyName);
    }

    public static void AgainstMaxLength(string? value, int maxLength, string propertyName)
    {
        AgainstNullOrWhiteSpace(value, propertyName);
        if (value!.Length > maxLength)
            throw new ArgumentException($"{propertyName} no puede tener más de {maxLength} caracteres", propertyName);
    }
    
    public static void AgainstInvalidEmail(string value)
    {
        if (!EmailRegex().IsMatch(value))
            throw new ArgumentException($"El correo es inválido");
    }

    public static void AgainstNegativeDecimal(decimal value, string propertyName)
    {
        if (value < 0)
            throw new ArgumentException($"{propertyName} no puede ser negativo", propertyName);
    }

    public static void AgainstMoreThanTwoDecimals(decimal value, string propertyName)
    {
        if (value * 100 != decimal.Truncate(value * 100))
            throw new DomainException($"{propertyName} no puede tener más de dos decimales");
    }
}

public static partial class Guard
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}