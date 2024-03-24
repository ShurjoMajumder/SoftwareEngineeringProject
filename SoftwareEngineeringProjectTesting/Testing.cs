using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using SoftwareEngineeringProject;

namespace SoftwareEngineeringProjectTesting;

public static class Testing
{
    /// <summary>
    /// Reads product CSV file.
    /// </summary>
    /// <param name="path">Path to file.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static IEnumerable<ProductRecord> ReadProductCsv(in string path)
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
    private static IEnumerable<SupplierRecord> ReadSupplierCsv(in string path)
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
    
    public static void Main()
    {
        Console.WriteLine("\n--------------------------------------------------------------");
        Console.WriteLine("TEST CASES:");
        Console.WriteLine("--------------------------------------------------------------");
        
        TestCase1();
        
        TestCase2();
        
        try
        {
            TestCase3();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            TestCase4();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            TestCase5();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
    
    private static void TestCase1 ()
    {
// Test Case 1: Ensure output sorted by product ID and write in out.txt file
        var productRecordsTest1 = ReadProductCsv( "ProductFileTest1.txt");
        var supplierRecordsTest1 = ReadSupplierCsv("SupplierFileTest1.txt");
        Console.WriteLine("Test Case 1: Ensure output sorted by product ID\n");
        const string expected1 = """
                                 Expected Result:

                                 Inventory Records:
                                 1234, TV, 5, $699.7, B, Acer
                                 2591, Camera, 10, $499.9, B, Acme Corporation
                                 """;
        Console.WriteLine(expected1);
        Console.WriteLine("\nActual Results:");
        var actual1 = Program.JoinRecordsOnSupplierId(productRecordsTest1, supplierRecordsTest1);
        Program.LogRecords(true, actual1);
        CsvUtils.WriteCsv(actual1, "out.txt");
    }
    
    private static void TestCase2()
    {
        // Test Case 2: Test not matching product and supplier
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Test Case 2: Ensure not matching product and supplier records results in empty inventory records\n");

        var productRecordsTest2 = ReadProductCsv( "ProductFileTest2.txt");
        var supplierRecordsTest2 = ReadSupplierCsv("SupplierFileTest2.txt");
        const string expected2 = """
                                 Expected Result:

                                 Inventory Records:
                                 """;
        Console.WriteLine(expected2);
        Console.WriteLine("\nActual Results:");
        var actual2 = Program.JoinRecordsOnSupplierId(productRecordsTest2, supplierRecordsTest2);
        Program.LogRecords(true, actual2);
    }

    private static void TestCase3()
    {
        // Test Case 3: Missing Column Inputs -> PROGRAM SHOULD CRASH
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Test Case 3: Missing Column Inputs\n");
        var productRecordsTest3 = ReadProductCsv( "ProductFileTest3.txt");
        var supplierRecordsTest3 = ReadSupplierCsv("SupplierFileTest3.txt");
        Console.WriteLine("\nActual Results:");
        var actual3 = Program.JoinRecordsOnSupplierId(productRecordsTest3, supplierRecordsTest3);
        Program.LogRecords(true, actual3);
    }

    private static void TestCase4()
    {
        // Test Case 4: Incorrect data type inputs -> PROGRAM SHOULD CRASH
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Test Case 4: Incorrect data type inputs\n");
        var productRecordsTest4 = ReadProductCsv( "ProductFileTest4.txt");
        var supplierRecordsTest4 = ReadSupplierCsv("SupplierFileTest4.txt");
        Console.WriteLine("\nActual Results:");
        var actual4 = Program.JoinRecordsOnSupplierId(productRecordsTest4, supplierRecordsTest4);
        Program.LogRecords(true, actual4);
    }

    private static void TestCase5()
    {
        // Test Case 5: Path does not exist -> PROGRAM SHOULD CRASH
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Test Case 5: Missing input file\n");
        var productRecordsTest5 = ReadProductCsv( "ProductFileTest5.txt");
        var supplierRecordsTest5 = ReadSupplierCsv("SupplierFileTest5.txt");
        Console.WriteLine("\nActual Results:");
        var actual5 = Program.JoinRecordsOnSupplierId(productRecordsTest5, supplierRecordsTest5);
        Program.LogRecords(true, actual5);
    }
}
