using System;
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
        [StringLength(75, MinimumLength = 3)]
        public string Customer { get; set; }

        [Required]
        public int ItemsID { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10)]
        public string Telephone { get; set; }

        [Required]
        public short Quantity { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string UserName { get; set; }

        [DefaultValue(false)]
        public bool CanContact { get; set; }

        [StringLength(10, MinimumLength = 8)]
        public string Receipt { get; set; }

        [Column(TypeName ="money")]
        [Range(1, double.MaxValue)]
        public decimal Cost { get; set; }

        public DateTime DateAdded { get; set; }

        [Timestamp, ConcurrencyCheck]
        public byte[] Concurrency { get; set; }

        public virtual Items Items { get; set; }
    }
}
