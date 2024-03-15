using CsvHelper.Configuration;

namespace SoftwareEngineeringProject;

/// <summary>
/// Holds product records.
/// </summary>
public record ProductRecord
{
    public int Id { get; init; }
    public string? ProductName { get; init; }
    public string? Description { get; init; }
    public string? Price { get; init; }
    public int Quantity { get; init; }
    public char Status { get; init; }
    public int SupplierId { get; init; }
    
    public override string ToString()
    {
        return $"{Id}, {ProductName}, {Description}, {Price}, {Quantity}, {Status}, {SupplierId}";
    }
}

/// <summary>
/// Maps the items read from a CSV file to the fields in <see cref="ProductMap"/>.
/// </summary>
public sealed class ProductMap : ClassMap<ProductRecord>
{
    /// <inheritdoc cref="ProductMap"/>
    public ProductMap()
    {
        Map(m => m.Id).Index(0);
        Map(m => m.ProductName).Index(1);
        Map(m => m.Description).Index(2);
        Map(m => m.Price).Index(3);
        Map(m => m.Quantity).Index(4);
        Map(m => m.Status).Index(5);
        Map(m => m.SupplierId).Index(6);
    }
}
