using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace SoftwareEngineeringProject;

/// <summary>
/// Holds Product records.
/// </summary>
public class ProductRecord
{
    public required int Id { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public required string Price { get; set; }
    public required int Quantity { get; set; }
    public required char Status { get; set; }
    public required int SupplierId { get; set; }

    /// <summary>
    /// Removes leading/trailing spaces from string members.
    /// </summary>
    public void Clean()
    {
        ProductName = ProductName.Trim();
        Description = Description.Trim();
        Price = Price.Trim();
    }
    
    public override string ToString()
    {
        return $"{Id}, {ProductName}, {Description}, {Price}, {Quantity}, {Status}, {SupplierId}";
    }
}

public sealed class ProductMap : ClassMap<ProductRecord>
{
    /// <summary>
    /// Maps the items read from the file to the fields in ProductRecord.
    /// </summary>
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
