namespace SoftwareEngineeringProject;

/// <summary>
/// Holds supplier records.
/// </summary>
public record SupplierRecord
{
    public int SupplierId { get; init; }
    public string? SupplierName { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    
    public override string ToString()
    {
        return $"{SupplierId}, {SupplierName}, {Address}, {Phone}, {Email}";
    }
}
