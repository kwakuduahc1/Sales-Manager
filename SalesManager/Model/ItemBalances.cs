using System;

namespace SalesManager.Model
{
    public class ItemBalances
    {
        public int ItemsID { get; set; }

        public string Group { get; set; }

        public string Item { get; set; }

        public int Quantity { get; set; }

        public DateTime DateIssued { get; set; }
    }
}