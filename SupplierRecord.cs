namespace SoftwareEngineeringProject;

/// <summary>
/// Holds supplier records.
/// </summary>
public record SupplierRecord
{
    public required int SupplierId { get; init; }
    public required string? SupplierName { get; init; }
    public required string? Address { get; init; }
    public required string? Phone { get; init; }
    public required string? Email { get; init; }
    
    public override string ToString()
    {
        return $"{SupplierId}, {SupplierName}, {Address}, {Phone}, {Email}";
    }
}
