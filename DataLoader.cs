using System;
using System.IO;
using System.Collections.Generic;

namespace SalesDataAnalyzer
{
    public static class DataLoader
    {
        private static int numColumns = 8;
        public static List<SalesData> Load(string inputFilePath)
        {
            List<SalesData> salesData = new List<SalesData>();

            try
            {
                using (var sr = new StreamReader(inputFilePath))
                {
                    int lineNum = 0;
                    while(!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split(',');
                        lineNum += 1;
                        
                        if (lineNum == 1) continue;

                        if (line.Length != numColumns)
                        {
                            throw new Exception($"Row {lineNum} contains {line.Length} values. It should contain {numColumns}.");
                        }
                        int columnNum = 0;
                        try
                        {
                            columnNum++;
                            string invoiceNo = line[0].ToString();
                            columnNum++;
                            string stockCode = line[1].ToString();
                            columnNum++;
                            string description = line[2].ToString();
                            columnNum++;
                            int quantity = Int32.Parse(line[3]);
                            columnNum++;
                            string invoiceDate = line[4].ToString();
                            columnNum++;
                            float unitPrice = Single.Parse(line[5]);
                            columnNum++;
                            int customerID = Int32.Parse(line[6]);
                            columnNum++;
                            string country = line[7].ToString();

                            SalesData newLine = new SalesData(invoiceNo, stockCode, description, quantity, 
                                                              invoiceDate, unitPrice, customerID, country);
                            salesData.Add(newLine);

                        }
                        catch (Exception e)
                        {
                            throw new Exception($"File contains invalid data at row {lineNum}, column {columnNum}. {e.Message}");
                        }                    
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please check the filename and run the program again.");
            }

            return salesData;
        }
    }
}