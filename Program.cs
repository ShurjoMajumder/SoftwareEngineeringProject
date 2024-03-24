namespace SoftwareEngineeringProject;

public static class Program
{
    /// <summary>
    /// Takes two input files (products, suppliers), outputs the inner join on the SupplierId.
    /// </summary>
    /// <param name="args">
    /// -[products|prods|prod|p] - specifies the path to the product file.
    /// -[suppliers|sups|sup|s] - specifies the path to the supplier file.
    /// -[output|out|o] - output path. overwrites pre-existing file. (defaults to ./out.txt)
    /// --[no-log|nolog|nlg|nl] - disables logging.
    /// </param>
    public static void Main(string[] args)
    {
        var arguments = new CmdArguments(args);

        var productRecords = CsvUtils.ReadCsv<ProductRecord>(
            arguments.ProductsPath, ", ",
            fields => new ProductRecord
            {
                Id = GenericConverter.Parse<int>(fields[0]),
                ProductName = fields[1].Trim(),
                Description = fields[2].Trim(),
                Price = fields[3].Trim(),
                Quantity = GenericConverter.Parse<int>(fields[4]),
                Status = GenericConverter.Parse<char>(fields[5]),
                SupplierId = GenericConverter.Parse<int>(fields[6])
            }
            ).ToList();
        
        var supplierRecords = CsvUtils.ReadCsv<SupplierRecord>(
            arguments.SuppliersPath,
            ", ",
            fields => new SupplierRecord
            {
                SupplierId = GenericConverter.Parse<int>(fields[0]),
                SupplierName = fields[1].Trim(),
                Address = fields[2].Trim(),
                Phone = fields[3].Trim(),
                Email = fields[4].Trim()
            }
            ).ToList();
        
        var inventory = JoinRecordsOnSupplierId(productRecords, supplierRecords);
        
        LogRecords(arguments.Logging, inventory);
        
        CsvUtils.WriteCsv(inventory, arguments.OutputPath);
    }
    
    /// <summary>
    /// Non-generic function that joins Product and Inventory records on "SupplierId".
    /// </summary>
    /// <param name="productRecords">IEnumerable containing product records.</param>
    /// <param name="supplierRecords">IEnumerable containing supplier records.</param>
    /// <returns>List of inventory records.</returns>
    public static List<InventoryRecord> JoinRecordsOnSupplierId(
        in IEnumerable<ProductRecord> productRecords,
        in IEnumerable<SupplierRecord> supplierRecords
        )
    {
        var inventory =
            from productRecord in productRecords
            join supplierRecord in supplierRecords on productRecord.SupplierId equals supplierRecord.SupplierId
            select new InventoryRecord
            {
                ProductId = productRecord.Id,
                ProductName = productRecord.ProductName,
                Quantity = productRecord.Quantity,
                Price = productRecord.Price,
                Status = productRecord.Status,
                SupplierName = supplierRecord.SupplierName
            };

        var inventoryRecords = inventory.ToList();
        inventoryRecords.Sort();
        
        return inventoryRecords;
    }
    
    /// <summary>
    /// Generic function that logs records.
    /// </summary>
    /// <param name="logging"></param>
    /// <param name="inventory"></param>
    public static void LogRecords<TRecord>(bool logging, in IEnumerable<TRecord> inventory)
    {
        if (!logging) { return; }

        Console.WriteLine("\nInventory Records:");
        foreach (var record in inventory)
        {
            Console.WriteLine(record);
        }
    }
}
