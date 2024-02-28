using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalesManager.Model
{
    public class Suppliers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuppliersID { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3, ErrorMessage = "Supplier name is required")]

        public string SupplierName { get; set; }

        public DateTime DateAdded { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Stockings> Stockings { get; set; }
    }

    public class EditSuppliersVm
    {
        [Key]
        [Required]
        public int SuppliersID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Supplier name is required")]
        public string SupplierName { get; set; }

        [StringLength(50)]
        [Required]
        public string Address { get; set; }
    }

    public class SupplierPayments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierPaymentsID { get; set; }

        [Required]
        public int SuppliersID { get; set; }

        public DateTime DatePaid { get; set; }

        [Range(1, double.MaxValue)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        public byte PaymentTypesID { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string Reference { get; set; }

        public virtual Suppliers Suppliers { get; set; }

        public virtual PaymentTypes PaymentTypes { get; set; }
    }

    public class Stockings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StockingsID { get; set; }

        [Required]
        public int ItemsID { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateBought { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Receipt { get; set; }

        [Required]
        public int SuppliersID { get; set; }

        [Range(0.1, double.MaxValue)]
        [Required]
        public double UnitCost { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string UserName { get; set; }

        [Timestamp, ConcurrencyCheck]
        public byte[] Concurrency { get; set; }

        public virtual Items Items { get; set; }

        public virtual Suppliers Suppliers { get; set; }
    }
}
