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
