using CsvHelper.Configuration;

namespace SoftwareEngineeringProject;

public class SupplierRecord
{
    public required int SupplierId { get; set; }
    public required string SupplierName { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    
    /// <summary>
    /// Removes leading/trailing spaces from string members.
    /// </summary>
    public void Clean()
    {
        SupplierName = SupplierName.Trim();
        Address = Address.Trim();
        Phone = Phone.Trim();
        Email = Email.Trim();
    }
    
    public override string ToString()
    {
        return $"{SupplierId}, {SupplierName}, {Address}, {Phone}, {Email}";
    }
}

public sealed class SupplierMap : ClassMap<SupplierRecord>
{
    public SupplierMap()
    {
        Map(m => m.SupplierId).Index(0);
        Map(m => m.SupplierName).Index(1);
        Map(m => m.Address).Index(2);
        Map(m => m.Phone).Index(3);
        Map(m => m.Email).Index(4);
    }
}
