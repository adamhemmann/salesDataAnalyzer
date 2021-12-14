using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SalesDataAnalyzer
{
    class Queries
    {
        public static string Query1(List<SalesData> salesData)
        {
            //1. List all the items sold to customers in Australia (stockCode and Description).
            string output = "1. All items sold to Australian customers (Stock Code and Description):\n" +
                            "-----------------------------------------------------------------------\n";

            var allAustralianSales = from item in salesData 
                                     where item.Country == "Australia" 
                                     select new { StockCode = item.StockCode,
                                                  Description = item.Description };         

            if (allAustralianSales.Count() > 0)
            {
                output += String.Format("{0, -15} {1}\n", "STOCK CODE", "ITEM DESCRIPTION");

                foreach (var i in allAustralianSales)
                {
                    output += String.Format("{0, -15} {1}\n", i.StockCode, i.Description);
                }
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query2(List<SalesData> salesData)
        {
            //2. How many individual sales were there? To determine this you have to count the unique invoice numbers. You should group by invoice number?
            string output = "\n2. Number of unique invoices:" +
                            "\n-----------------------------\n";
                            
            var numberOfUniqueInvoices = from item in salesData 
                                         orderby item.InvoiceNo ascending 
                                         select item;

            if (numberOfUniqueInvoices.Count() > 0)
            {
                var sList = new ArrayList();

                foreach (SalesData i in numberOfUniqueInvoices)
                {
                    if (!sList.Contains(i.InvoiceNo))
                    {
                        sList.Add(i.InvoiceNo);
                    } else {
                        continue;
                    }
                }
                output += String.Format("{0:N0}", sList.Count);
            }
            return output;
        }

        public static string Query3(List<SalesData> salesData)
        {
            //3. What is the sales total for invoice number 536365? Sales total for an invoice will be the sum of (Quantity * UnitPrice) for all products sold in the invoice.
            string output = "\n\n3. Sales total for invoice number 536365:" +
                            "\n-----------------------------------------\n";
            float tt = 0f;

            var totalSalesForInvoice536365 = from item in salesData 
                                             where item.InvoiceNo == "536365" 
                                             select item.LineTotal;

            if (totalSalesForInvoice536365.Count() > 0)
            {
                foreach (float i in totalSalesForInvoice536365)
                {
                    tt += i;
                }
                output += String.Format("{0:C}", tt);
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query4(List<SalesData> salesData)
        {
            //4. List the total sales by country?
            string output = "";
            output += "\n\n4. Total sales by country:" +
                      "\n--------------------------\n";

            var totalSalesByCountry = from item in salesData 
                                      orderby item.Country 
                                      select new { Country = item.Country, 
                                                   LineTotal = item.LineTotal };

            var salesDict = new Dictionary<string, float>();

            if (totalSalesByCountry.Count() > 0)
            {
                foreach (var i in totalSalesByCountry)
                {
                    if (!salesDict.ContainsKey(i.Country))
                    {
                        salesDict.Add(i.Country, i.LineTotal);
                    } else {
                        salesDict[i.Country] += i.LineTotal;
                    }
                }
                output += String.Format("{0,-20} {1}\n", "COUNTRY", "TOTAL SALES");

                foreach (KeyValuePair<string, float> x in salesDict)
                {
                    output += String.Format("{0, -20} {1:C}\n", x.Key, x.Value);
                }
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query5(List<SalesData> salesData)
        {
            //5. Which customer has spent the most money during the period?
            string output = "\n5. Customer with the highest total sales:" +
                            "\n-----------------------------------------\n";

            var customerWithTheHighestTotalSales = from item in salesData 
                                                   orderby item.CustomerID 
                                                   select new { CustomerID = item.CustomerID, 
                                                                LineTotal = item.LineTotal };

            var salesDict2 = new Dictionary<int, float>();

            if (customerWithTheHighestTotalSales.Count() > 0)
            {
                foreach (var i in customerWithTheHighestTotalSales)
                {
                    if (!salesDict2.ContainsKey(i.CustomerID))
                    {
                        salesDict2.Add(i.CustomerID, i.LineTotal);
                    } else {
                        salesDict2[i.CustomerID] += i.LineTotal;
                    }
                }

                var items = (from pair in salesDict2 
                             orderby pair.Value descending 
                             select pair).First();

                output += String.Format("{0, -15} {1:C}\n", "CUSTOMER ID", "TOTAL SALES") +
                          String.Format("{0, -15} {1:C}", items.Key, items.Value);
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query6(List<SalesData> salesData)
        {
            //6. What are the total sales to customer 15311?
            string output = "\n\n6. Total sales for customer # 15311:" +
                            "\n------------------------------------\n";
            int t = 0;

            var totalSalesForCustomer15311 = from item in salesData 
                                             where item.CustomerID == 15311 
                                             select item.LineTotal;

            if (totalSalesForCustomer15311.Count() > 0)
            {
                foreach (int i in totalSalesForCustomer15311)
                {
                    t += i;
                }
                output += String.Format("{0:C}", t);
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query7(List<SalesData> salesData)
        {
            //7. How many units of “HAND WARMER UNION JACK” were sold in the dataset?
            string output = "\n\n7. Units of \"HAND WARMER UNION JACK\" sold:" +
                            "\n------------------------------------------\n";
            int t = 0;

            var handWarmerUnionJackTotalUnits = from item in salesData
                                                where item.Description == "HAND WARMER UNION JACK" 
                                                select item.Quantity;                                                             

            if (handWarmerUnionJackTotalUnits.Count() > 0)
            {
                foreach (var i in handWarmerUnionJackTotalUnits)
                {
                    t += i;
                }
                output += String.Format("{0:N0}", t);
                //Console.WriteLine($"\n{t}");
            } else {
                output += "This data is not available";
            }
            return output;
        }

        public static string Query8(List<SalesData> salesData)
        {
            //8. What was the total value of the “HAND WARMER UNION JACK” sales in the dataset?
            string output = "\n\n8. Total value of \"HAND WARMER UNION JACK\" sales:" +
                            "\n-------------------------------------------------\n";
            float tt = 0f;

            var totalValueOfHandWarmerUnionJackSales = from item in salesData
                                                       where item.Description == "HAND WARMER UNION JACK"
                                                       select item.LineTotal;

            if (totalValueOfHandWarmerUnionJackSales.Count() > 0)
            {
                foreach (var i in totalValueOfHandWarmerUnionJackSales)
                {
                    tt += i;
                }
                output += String.Format("{0:C}", tt);
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query9(List<SalesData> salesData)
        {
            //9. Which product has the highest UnitPrice (stockCode and Description).?
            string output = "\n\n9. Product with the highest UnitPrice:" +
                            "\n--------------------------------------\n";

            var productWithTheHighestUnitPrice = (from item in salesData 
                                                  orderby item.UnitPrice descending 
                                                  select new { StockCode = item.StockCode,
                                                               Description = item.Description,
                                                               UnitPrice = item.UnitPrice })
                                                               .First();

            if (productWithTheHighestUnitPrice != null)
            {
                output += String.Format("{0, -10} {1, 20} {2, 15}\n", "STOCK CODE", "ITEM DESCRIPTION", "UNIT PRICE") +
                          String.Format("{0, -10} {1, 20} {2, 15:C}", productWithTheHighestUnitPrice.StockCode, 
                                                                      productWithTheHighestUnitPrice.Description, 
                                                                      productWithTheHighestUnitPrice.UnitPrice);
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query10(List<SalesData> salesData)
        {
            //10. What is the total sales for the entire dataset?
            string output = "\n\n10. Total sales for entire dataset:" +
                            "\n-----------------------------------\n";
            float tt = 0f;

            var salesForEntireDataset = from item in salesData 
                                        select item.LineTotal;

            if (salesForEntireDataset.Count() > 0)
            {
                foreach (var i in salesForEntireDataset)
                {
                    tt += i;
                }
                output += String.Format("{0:C}", tt);
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query11(List<SalesData> salesData)
        {
            //11. Which invoice number had the highest sales?
            string output = "\n\n11. Invoice with the highest sales:" +
                            "\n-----------------------------------\n";

            var invoiceWithHighestSales = from item in salesData 
                                          orderby item.InvoiceNo 
                                          select new { item.InvoiceNo,
                                                       item.LineTotal };

            var salesDict3 = new Dictionary<string, float>();

            if (invoiceWithHighestSales.Count() > 0)
            {
                foreach (var i in invoiceWithHighestSales)
                {
                    if (!salesDict3.ContainsKey(i.InvoiceNo))
                    {
                        salesDict3.Add(i.InvoiceNo, i.LineTotal);
                    } else {
                        salesDict3[i.InvoiceNo] += i.LineTotal;
                    }
                }

                var items = (from pair in salesDict3 
                             orderby pair.Value descending 
                             select pair)
                             .First();

                output += String.Format("{0, -12} {1}\n", "INVOICE #", "TOTAL SALES") +
                          String.Format("{0, -12} {1:C}", items.Key, items.Value);
            } else {
                output += "This data is not available.";
            }
            return output;
        }

        public static string Query12(List<SalesData> salesData)
        {
            //12. Which product sold the most units?
            string output = "\n\n12. Product with most units sold:" +
                            "\n---------------------------------\n";

            var productWithMostUnitsSold = from item in salesData 
                                           orderby item.Description 
                                           select new { item.Description,
                                                        item.Quantity };

            var salesDict4 = new Dictionary<string, int>();

            if (productWithMostUnitsSold.Count() > 0)
            {
                foreach (var i in productWithMostUnitsSold)
                {
                    if (!salesDict4.ContainsKey(i.Description))
                    {
                        salesDict4.Add(i.Description, i.Quantity);
                    } else {
                        salesDict4[i.Description] += i.Quantity;
                    }
                }

            var items = (from pair in salesDict4 
                            orderby pair.Value descending 
                            select pair)
                            .First();
                            
            output += String.Format("{0, -40} {1}\n", "PRODUCT DESCRIPTION", "TOTAL QUANTITY SOLD") +
                      String.Format("{0, -40} {1:N0}", items.Key, items.Value);
            } else {
            output += "This data is not available.";
            }
        return output;
        }
    }
}
