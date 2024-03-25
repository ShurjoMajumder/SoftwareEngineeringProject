using Microsoft.VisualBasic.FileIO;

namespace SoftwareEngineeringProject;

/// <summary>
/// Contains generic utility functions for reading from and writing to CSV files.
/// </summary>
public static class CsvUtils
{
    /// <summary>
    /// Generic function that reads a CSV file.
    /// </summary>
    /// <param name="path">Path to CSV.</param>
    /// <param name="delim">Delimiter.</param>
    /// <param name="mapper">Function that maps the fields to an object type.</param>
    /// <typeparam name="TRecord">Object type</typeparam>
    /// <returns>List of objects</returns>
    /// <exception cref="Exception">Invalid data format.</exception>
    public static IEnumerable<TRecord> ReadCsv<TRecord>(in string path, in string delim, Func<string[], TRecord> mapper)
    {
        using var parser = new TextFieldParser(path);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(delim);

        var records = new List<TRecord>();

        while (!parser.EndOfData)
        {
            var fields = parser.ReadFields();
            if (fields == null) throw new Exception("Invalid CSV data");
            
            records.Add(mapper(fields));
        }
        
        return records;
    }

    /// <summary>
    /// Writes records from an `IEnumerable` to a file at `path`.
    /// </summary>
    /// <param name="records">Records to write.</param>
    /// <param name="path">Path to output file.</param>
    /// <typeparam name="TRecord">Record type.</typeparam>
    public static void WriteCsv<TRecord>(in IEnumerable<TRecord> records, string path)
    {
        using var writer = new StreamWriter(path);

        foreach (var record in records)
        {
            writer.WriteLine(record);
        }
    }
}
