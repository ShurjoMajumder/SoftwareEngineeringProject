using System.Collections;
using Microsoft.VisualBasic.FileIO;

namespace SoftwareEngineeringProject;

/// <summary>
/// Contains generic utility functions for reading from and writing to CSV files.
/// </summary>
public static class CsvUtils
{
    /// <summary>
    /// Reads product CSV file.
    /// </summary>
    /// <param name="path">Path to file.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<ProductRecord> ReadProductCsv(in string path)
    {
        using var parser = new TextFieldParser(path);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(", ");

        var records = new List<ProductRecord>();
        
        while (!parser.EndOfData)
        {
            var fields = parser.ReadFields();
            if (fields == null) throw new ArgumentException("Invalid CSV data.");
            
            var record = new ProductRecord
            {
                Id = GenericConverter.Parse<int>(fields[0]),
                ProductName = fields[1],
                Description = fields[2],
                Price = fields[3],
                Quantity = GenericConverter.Parse<int>(fields[4]),
                Status = GenericConverter.Parse<char>(fields[5]),
                SupplierId = GenericConverter.Parse<int>(fields[6])
            };
            
            records.Add(record);
        }
        return records;
    }

    /// <summary>
    /// Reads supplier CSV file.
    /// </summary>
    /// <param name="path">Path to file.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<SupplierRecord> ReadSupplierCsv(in string path)
    {
        using var parser = new TextFieldParser(path);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(", ");

        var records = new List<SupplierRecord>();
        
        while (!parser.EndOfData)
        {
            var fields = parser.ReadFields();
            if (fields == null) throw new ArgumentException("Invalid CSV data.");
            
            var record = new SupplierRecord
            {
                SupplierId = GenericConverter.Parse<int>(fields[0]),
                SupplierName = fields[1],
                Address = fields[2],
                Phone = fields[3],
                Email = fields[4]
            };
            
            records.Add(record);
        }
        return records;
    }

    /// <summary>
    /// Writes records from an `IEnumerable` to a file at `path`.
    /// </summary>
    /// <param name="records">Records to write.</param>
    /// <param name="path">Path to output file.</param>
    /// <typeparam name="TRecord"></typeparam>
    public static void WriteCsv<TRecord>(in IEnumerable records, string path)
    {
        using var writer = new StreamWriter(path);

        foreach (var record in records)
        {
            writer.WriteLine(record);
        }
    }
}
