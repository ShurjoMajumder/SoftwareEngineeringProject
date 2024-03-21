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

        var productRecords = CsvUtils.ReadProductCsv(arguments.ProductsPath).ToList();
        var supplierRecords = CsvUtils.ReadSupplierCsv(arguments.SuppliersPath).ToList();
        
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
        IEnumerable<ProductRecord> productRecords,
        IEnumerable<SupplierRecord> supplierRecords
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
    public static void LogRecords<TRecord>(bool logging, IEnumerable<TRecord> inventory)
    {
        if (!logging) { return; }

        Console.WriteLine("\nInventory Records:");
        foreach (var record in inventory)
        {
            Console.WriteLine(record);
        }
    }
}
