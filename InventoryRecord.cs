using CsvHelper.Configuration;

namespace SoftwareEngineeringProject;

public class InventoryRecord
{
    public required int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required int Quantity { get; set; }
    public required string Price { get; set; }
    public required char Status { get; set; }
    public required string SupplierName { get; set; }

    public override string ToString()
    {
        return $"{ProductId}, {ProductName}, {Quantity}, {Price}, {Status}, {SupplierName}";
    }
}

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
