using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManager.Model
{
    public class Payments
    {
        [Key]
        [StringLength(20, MinimumLength = 6)]
        public string Receipt { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        [Required]
        public decimal Cash { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue)]
        [DefaultValue(0)]
        [Required]
        public decimal MobileMoney { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3)]
        public string Customer { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10)]
        public string Telephone { get; set; }

        [DefaultValue(false)]
        public bool CanContact { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue)]
        public decimal Total { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        [DefaultValue("Outright Purchase")]
        [StringLength(30, MinimumLength = 5)]
        public string SalesType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpectedDate { get; set; }

        public DateTime DatePaid { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }
    }

    public class SalesLedgerVm
    {
        public string Receipt { get; set; }
        public decimal Cash { get; set; }
        public decimal MobileMoney { get; set; }
        public string Customer { get; set; }
        public decimal Total { get; set; }
        public string SalesType { get; set; }
        public DateTime DatePaid { get; set; }
        public decimal Cost { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string Telephone { get; set; }
        public int PricesID { get; set; }
        public short Quantity { get; set; }
    }

}
