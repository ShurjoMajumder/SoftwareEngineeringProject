using CsvHelper.Configuration;

namespace SoftwareEngineeringProject;

/// <summary>
/// Holds inventory records.
/// </summary>
public record InventoryRecord : IComparable<InventoryRecord>
{
    public required int ProductId { get; init; }
    public required string ProductName { get; init; }
    public required int Quantity { get; init; }
    public required string Price { get; init; }
    public required char Status { get; init; }
    public required string SupplierName { get; init; }

    public override string ToString()
    {
        return $"{ProductId}, {ProductName}, {Quantity}, {Price}, {Status}, {SupplierName}";
    }

    public int CompareTo(InventoryRecord? other)
    {
        if (other == null)
        {
            throw new ArgumentException("Other is null.");
        }
        
        return ProductId - other.ProductId;
    }
}

/// <summary>
/// Maps the items read from a CSV file to the fields in <see cref="InventoryRecord"/>.
/// </summary>
public sealed class InventoryMap : ClassMap<InventoryRecord>
{
    public InventoryMap()
    {
        Map(m => m.ProductId).Index(0);
        Map(m => m.ProductName).Index(1);
        Map(m => m.Quantity).Index(2);
        Map(m => m.Price).Index(3);
        Map(m => m.Status).Index(4);
        Map(m => m.SupplierName).Index(5);
    }
}
