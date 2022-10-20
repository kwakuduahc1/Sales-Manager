using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManager.Model
{
    public class Sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SalesID { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Required]
        public int ItemsID { get; set; }

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string UserName { get; set; }

        [StringLength(20, MinimumLength = 6)]
        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(Payments))]
        public string Receipt { get; set; }

        public DateTime DateAdded { get; set; }

        [Timestamp, ConcurrencyCheck]
        public byte[] Concurrency { get; set; }

        public virtual Items Items { get; set; }

        public virtual Payments Payments { get; set; }
    }

    public class SalesVm
    {
        [Required]
        public int ItemsID { get; set; }

        [Required]
        public short Quantity { get; set; }

        public string ItemName { get; set; }
        public decimal Cost { get; set; }
    }

    public class ReceiptVm
    {
        [Required]
        [StringLength(75, MinimumLength = 3)]
        public string Customer { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string SalesType { get; set; }

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

        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10)]
        public string Telephone { get; set; }

        [DefaultValue(false)]
        public bool CanContact { get; set; }

        public List<SalesVm> Sales { get; set; }

        public string Receipt { get; set; }
        public DateTime DatePaid { get; set; }
    }
}
