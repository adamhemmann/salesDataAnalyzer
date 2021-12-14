using System;

namespace SalesDataAnalyzer
{
    public class SalesData
    {
        public string InvoiceNo { get; }
        public string StockCode { get; }
        public string Description { get; }
        public int Quantity { get; }
        public string InvoiceDate { get; }
        public float UnitPrice { get; }
        public int CustomerID { get; }
        public string Country { get; }
        public float LineTotal { get; }

        public SalesData(string invoiceNo, string stockCode, string description, int quantity,
                         string invoiceDate, float unitPrice, int customerID, string country)
                        {   
                        InvoiceNo = invoiceNo;
                        StockCode = stockCode;
                        Description = description;
                        Quantity = quantity;
                        InvoiceDate = invoiceDate;
                        UnitPrice = unitPrice;
                        CustomerID = customerID;
                        Country = country;
                        LineTotal = (quantity * unitPrice);
                        }

        /*override public string ToString()
        {
	        return String.Format("InvoiceNo: {0}, StockCode: {1}, Description: {2}, Quantity: {3}, InvoiceDate: {4}, UnitPrice: {5}, CustomerID: {6}, Country: {7}",
                                  InvoiceNo, StockCode, Description, Quantity, InvoiceDate, UnitPrice, CustomerID, Country);
        }*/
    }
}