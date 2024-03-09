using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace SoftwareEngineeringProject;

internal class Program
{
    public static void Main(string[] args)
    {
        List<ProductRecord> productRecords;
        List<SupplierRecord> supplierRecords;
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false };

        using (var productReader = new StreamReader(@"C:\dev\SoftwareEngineeringProject\assets\ProductFile.txt"))
        using (var productCsv = new CsvReader(productReader, config))
        {
            productCsv.Context.RegisterClassMap<ProductMap>();
            productRecords = productCsv.GetRecords<ProductRecord>().ToList();
        }

        using (var supplierReader = new StreamReader(@"C:\dev\SoftwareEngineeringProject\assets\SupplierFile.txt"))
        using (var supplierCsv = new CsvReader(supplierReader, config))
        {
            supplierCsv.Context.RegisterClassMap<SupplierMap>();
            supplierRecords = supplierCsv.GetRecords<SupplierRecord>().ToList();
        }

        productRecords.ForEach(record => record.Clean());
        supplierRecords.ForEach(record => record.Clean());

        Console.WriteLine("Product Records:");
        foreach (var record in productRecords)
        {
            Console.WriteLine(record);
        }

        Console.WriteLine("\nSupplier Records:");
        foreach (var record in supplierRecords)
        {
            Console.WriteLine(record);
        }

        var inventoryEnum = from productRecord in productRecords
            join supplierRecord in supplierRecords on productRecord.SupplierId equals supplierRecord.SupplierId
            select new InventoryRecord
            {
                ProductId = productRecord.Id,
                ProductName = productRecord.ProductName.Trim(),
                Quantity = productRecord.Quantity,
                Price = productRecord.Price.Trim(),
                Status = productRecord.Status,
                SupplierName = supplierRecord.SupplierName.Trim()
            };

        var inventory = inventoryEnum.ToList();
        
        Console.WriteLine("\nInventory Records:");
        foreach (var record in inventory)
        {
            Console.WriteLine(record);
        }
    }
}