using CsvHelper.Configuration;

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

/// <summary>
/// Maps the items read from the file to the fields in <see cref="SupplierRecord"/>.
/// </summary>
public sealed class SupplierMap : ClassMap<SupplierRecord>
{
    /// <inheritdoc cref="SupplierMap"/>
    public SupplierMap()
    {
        Map(m => m.SupplierId).Index(0);
        Map(m => m.SupplierName).Index(1);
        Map(m => m.Address).Index(2);
        Map(m => m.Phone).Index(3);
        Map(m => m.Email).Index(4);
    }
}
