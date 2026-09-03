namespace Inventory.Application.Features.Products;

public record ProductDto
{
    public Guid Id { get; init; }
    public string BarCode { get; init; }
    public string Description { get; init; }
    public string Currency { get; init; }
    public decimal Cost { get; init; }
    public decimal Price { get; init; }
    public decimal WholesaleQuantity { get; init; }
    public decimal WholesalePrice { get; init; }
    public decimal Stock { get; init; }
    public decimal MinStock { get; init; }
    public string MeasurementUnit { get; set; }
}