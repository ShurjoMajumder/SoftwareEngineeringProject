using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace SoftwareEngineeringProject;

internal static class Program
{
    /// <summary>
    /// Takes two input files (products, suppliers), outputs the inner join on the SupplierId.
    /// </summary>
    /// <param name="args">
    /// -[products|prods|prod|p] - specifies the path to the product file.
    /// -[suppliers|sups|sup|s] - specifies the path to the supplier file.
    /// -[output|out|o] - output path. overwrites pre-existing file. (defaults to ./out.txt)
    /// --[no-log|nlg|nl] - disables logging.
    /// </param>
    public static void Main(string[] args)
    {
        var arguments = new CmdArguments(args);
        
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Delimiter = ", "
        };
        
        var productRecords = ReadCsv<ProductRecord, ProductMap>(arguments.ProductsPath, config);
        var supplierRecords = ReadCsv<SupplierRecord, SupplierMap>(arguments.SuppliersPath, config);
        
        var inventory = JoinRecords(productRecords, supplierRecords);
        
        LogRecords(arguments.Logging, inventory);
        
        WriteCsv<InventoryRecord, InventoryMap>(inventory, arguments.OutputPath, config);
    }

    /// <summary>
    /// Generic function that reads a CSV file.
    /// </summary>
    /// <param name="path">Path to the CSV file.</param>
    /// <param name="config">CsvHelper configuration.</param>
    /// <typeparam name="TRecord">Record type.</typeparam>
    /// <typeparam name="TMapper">Class map that maps to TRecord.</typeparam>
    /// <returns>List of TRecord objects.</returns>
    private static List<TRecord> ReadCsv<TRecord, TMapper>(string path, IReaderConfiguration config)
        where TMapper : ClassMap<TRecord>
    {
        // input validation in the form of "if its wrong, the program crashes".
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<TMapper>();
        var records = csv.GetRecords<TRecord>().ToList();
        
        return records;
    }

    /// <summary>
    /// Non-generic function that joins Product and Inventory records on "SupplierId".
    /// </summary>
    /// <param name="productRecords">IEnumerable containing product records.</param>
    /// <param name="supplierRecords">IEnumerable containing supplier records.</param>
    /// <returns>List of inventory records.</returns>
    private static List<InventoryRecord> JoinRecords(
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

        return inventory.ToList();
    }

    /// <summary>
    /// Generic function that logs records.
    /// </summary>
    /// <param name="logging"></param>
    /// <param name="inventory"></param>
    private static void LogRecords<TRecord>(bool logging, IEnumerable<TRecord> inventory)
    {
        if (!logging) { return; }
        
        Console.WriteLine("\nInventory Records:");
        foreach (var record in inventory)
        {
            Console.WriteLine(record);
        }
    }

    /// <summary>
    /// Generic function that writes records to the given output path. Overwrites the file.
    /// </summary>
    /// <param name="records">Records to write.</param>
    /// <param name="outPath">Path to output file.</param>
    /// <param name="config">CsvHelper configuration.</param>
    /// <typeparam name="TRecord">Record type.</typeparam>
    /// <typeparam name="TMapper">Class map that maps to TRecord.</typeparam>
    private static void WriteCsv<TRecord, TMapper>(
        IEnumerable<TRecord> records,
        string outPath,
        IWriterConfiguration config
        )
    where TMapper : ClassMap<TRecord>
    {
        // input validation in the form of "if its wrong, the program crashes".
        // the default output file is valid, so any exception here is always the user's fault.
        using var writer = new StreamWriter(outPath);
        using var csv = new CsvWriter(writer, config);
        
        csv.Context.RegisterClassMap<TMapper>();
        csv.WriteRecords(records);
    }
}
