using CsvHelper;
using CsvHelper.Configuration;

namespace SoftwareEngineeringProject;

/// <summary>
/// Contains generic utility functions for reading from and writing to CSV files.
/// </summary>
internal static class CsvUtils
{
    /// <summary>
    /// Generic function that reads a CSV file.
    /// </summary>
    /// <param name="path">Path to the CSV file.</param>
    /// <param name="config">CsvHelper configuration.</param>
    /// <typeparam name="TRecord">Record type.</typeparam>
    /// <typeparam name="TClassMap">Class map that maps to TRecord.</typeparam>
    /// <returns>List of TRecord objects.</returns>
    public static List<TRecord> ReadCsv<TRecord, TClassMap>(string path, IReaderConfiguration config)
        where TClassMap : ClassMap<TRecord>
    {
        // input validation in the form of "if its wrong, the program crashes".
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<TClassMap>();
        var records = csv.GetRecords<TRecord>().ToList();
        
        return records;
    }
    
    /// <summary>
    /// Generic function that writes records to the given output path. Overwrites the file.
    /// </summary>
    /// <param name="records">Records to write.</param>
    /// <param name="outPath">Path to output file.</param>
    /// <param name="config">CsvHelper configuration.</param>
    /// <typeparam name="TRecord">Record type.</typeparam>
    /// <typeparam name="TClassMap">Class map that maps to TRecord.</typeparam>
    public static void WriteCsv<TRecord, TClassMap>(
        IEnumerable<TRecord> records,
        string outPath,
        IWriterConfiguration config
        )
        where TClassMap : ClassMap<TRecord>
    {
        // input validation in the form of "if its wrong, the program crashes".
        // the default output file is valid, so any exception here is always the user's fault.
        using var writer = new StreamWriter(outPath);
        using var csv = new CsvWriter(writer, config);
        
        csv.Context.RegisterClassMap<TClassMap>();
        csv.WriteRecords(records);
    }
}

/// <summary>
/// Maps the items read from a CSV file to the fields in <see cref="ProductMap"/>.
/// </summary>
public sealed class ProductMap : ClassMap<ProductRecord>
{
    /// <inheritdoc cref="ProductMap"/>
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

/// <summary>
/// Maps the items read from a CSV file to the fields in <see cref="SupplierRecord"/>.
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

/// <summary>
/// Maps the items read from a CSV file to the fields in <see cref="InventoryRecord"/>.
/// </summary>
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
