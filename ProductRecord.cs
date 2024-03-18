namespace SoftwareEngineeringProject;

/// <summary>
/// Holds product records.
/// </summary>
public record ProductRecord
{
    public required int Id { get; init; }
    public required string? ProductName { get; init; }
    public required string? Description { get; init; }
    public required string? Price { get; init; }
    public required int Quantity { get; init; }
    public required char Status { get; init; }
    public required int SupplierId { get; init; }
    
    public override string ToString()
    {
        return $"{Id}, {ProductName}, {Description}, {Price}, {Quantity}, {Status}, {SupplierId}";
    }
}
