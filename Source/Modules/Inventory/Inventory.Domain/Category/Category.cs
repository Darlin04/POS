using Inventory.Domain.Products;
using Blocks.Domain.Abstractions;
using Blocks.Domain.Guards;
using Blocks.Domain.ValueObjects;

namespace Inventory.Domain.Category;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public Itbis Itbis {get; private set;}
    public bool IsActive {get; private set;}

    private readonly List<Product> _products = new List<Product>();
    
    private Category (){}

    public Category(string name, Itbis itbis, bool isactive)
    {
        Guard.AgainstNullOrWhiteSpace(name, nameof(name));
        Guard.AgainstMaxLength(name, 50,nameof(name));

        Name = name;
        Itbis = itbis;
        IsActive = isactive;
    }
}