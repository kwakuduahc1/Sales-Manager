using System;

namespace SalesManager.Model
{
    public class Ledger
    {
        public string Description { get; set; }

        public int Quantity { get; set; }

        public int ItemsID { get; set; }

        public string ItemName { get; set; }

        public int Balance { get; set; }

        public DateTime DateAdded { get; set; }
    }

    public class RecentSalesVm
    {
        public string Receipt { get; set; }

        public decimal Cost { get; set; }

        public string SalesType { get; set; }

        public string Customer { get; set; }

        public DateTime DatePaid{ get; set; }
    }

    public class StockHistoryVm
    {
        public string Receipt { get; set; }

        public DateTime DateBought { get; set; }

        public decimal Total { get; set; }
    }

}