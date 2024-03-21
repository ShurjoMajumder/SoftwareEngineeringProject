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
                ProductName = fields[1].Trim(),
                Description = fields[2].Trim(),
                Price = fields[3].Trim(),
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
                SupplierName = fields[1].Trim(),
                Address = fields[2].Trim(),
                Phone = fields[3].Trim(),
                Email = fields[4].Trim()
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
    public static void WriteCsv<TRecord>(in IEnumerable<TRecord> records, string path)
    {
        using var writer = new StreamWriter(path);

        foreach (var record in records)
        {
            writer.WriteLine(record);
        }
    }
}
