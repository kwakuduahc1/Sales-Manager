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
        public int SalesID { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        public string Receipt { get; set; }

        public int PricesID { get; set; }

        public decimal Price { get; set; }

        public string Unit { get; set; }

        public int ItemsID { get; set; }

        public string ItemName { get; set; }

        public DateTime DateAdded { get; set; }
    }
}