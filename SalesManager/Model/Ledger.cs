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
}