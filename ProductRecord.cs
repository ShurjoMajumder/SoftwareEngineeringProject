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
