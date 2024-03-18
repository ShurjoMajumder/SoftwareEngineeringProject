using System.Globalization;
using CsvHelper.Configuration;

namespace SoftwareEngineeringProject
{
    public class Testing
    {
        public void testing(){
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ", "
            };            
            Console.WriteLine("\n---------------------------------------------------------------");
            Console.WriteLine("TEST CASES:");
            Console.WriteLine("--------------------------------------------------------------");
    
            testCase1(config);
           // testCase2(config);
           // testCase3(config);
           // testCase4(config);
           // testCase5(config);
        }
        private void testCase1 (CsvConfiguration config){
// Test Case 1: Ensure output sorted by product ID and write in out.txt file
            var productRecordsTest1 = CsvUtils.ReadCsv<ProductRecord, ProductMap>( $"ProductFileTest1.txt", config);
            var supplierRecordsTest1 = CsvUtils.ReadCsv<SupplierRecord, SupplierMap>($"SupplierFileTest1.txt", config);
            Console.WriteLine("Test Case 1: Ensure output sorted by product ID\n");
            string expected1 = @"Expected Result:

Inventory Records:
1234, TV, 5, $699.7, B, Acer
2591, Camera, 10, $499.9, B, Acme Corporation";
            Console.WriteLine(expected1);
            Console.WriteLine("\nActual Results:");
            var actual1 = Program.JoinRecordsOnSupplierId(productRecordsTest1, supplierRecordsTest1);
            Program.LogRecords(true, actual1);
            CsvUtils.WriteCsv<InventoryRecord, InventoryMap>(actual1, $"out.txt", config);
        }
        private void testCase2(CsvConfiguration config){
            // Test Case 2: Test not matching product and supplier
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Test Case 2: Ensure not matching product and supplier records results in empty inventory records\n");

            var productRecordsTest2 = CsvUtils.ReadCsv<ProductRecord, ProductMap>( $"ProductFileTest2.txt", config);
            var supplierRecordsTest2 = CsvUtils.ReadCsv<SupplierRecord, SupplierMap>($"SupplierFileTest2.txt", config);
            string expected2 = @"Expected Result:

Inventory Records:";
            Console.WriteLine(expected2);
            Console.WriteLine("\nActual Results:");
            var actual2 = Program.JoinRecordsOnSupplierId(productRecordsTest2, supplierRecordsTest2);
            Program.LogRecords(true, actual2);
        }

        private void testCase3(CsvConfiguration config){
            // Test Case 3: Missing Column Inputs -> PROGRAM SHOULD CRASH
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Test Case 3: Missing Column Inputs\n");
            var productRecordsTest3 = CsvUtils.ReadCsv<ProductRecord, ProductMap>( $"ProductFileTest3.txt", config);
            var supplierRecordsTest3 = CsvUtils.ReadCsv<SupplierRecord, SupplierMap>($"SupplierFileTest3.txt", config);
            Console.WriteLine("\nActual Results:");
            var actual3 = Program.JoinRecordsOnSupplierId(productRecordsTest3, supplierRecordsTest3);
            Program.LogRecords(true, actual3);
        }

        private void testCase4(CsvConfiguration config){
            // Test Case 4: Incorrect data type inputs -> PROGRAM SHOULD CRASH
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Test Case 4: Incorrect data type inputs\n");
            var productRecordsTest4 = CsvUtils.ReadCsv<ProductRecord, ProductMap>( $"ProductFileTest4.txt", config);
            var supplierRecordsTest4 = CsvUtils.ReadCsv<SupplierRecord, SupplierMap>($"SupplierFileTest4.txt", config);
            Console.WriteLine("\nActual Results:");
            var actual4 = Program.JoinRecordsOnSupplierId(productRecordsTest4, supplierRecordsTest4);
            Program.LogRecords(true, actual4);
        }

        private void testCase5(CsvConfiguration config){
            // Test Case 5: Path does not exist -> PROGRAM SHOULD CRASH
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Test Case 5: Incorrect data type inputs\n");
            var productRecordsTest5 = CsvUtils.ReadCsv<ProductRecord, ProductMap>( $"ProductFileTest5.txt", config);
            var supplierRecordsTest5 = CsvUtils.ReadCsv<SupplierRecord, SupplierMap>($"SupplierFileTest5.txt", config);
            Console.WriteLine("\nActual Results:");
            var actual5 = Program.JoinRecordsOnSupplierId(productRecordsTest5, supplierRecordsTest5);
            Program.LogRecords(true, actual5);
        }
    }
}